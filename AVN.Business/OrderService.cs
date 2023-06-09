﻿using AVN.Common;
using AVN.Common.Customs;
using AVN.Common.Enums;
using AVN.Common.PdfGenerator;
using AVN.Data;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using iText.Commons.Actions.Contexts;
using iText.Layout.Borders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using System.Text;
using System.Text.RegularExpressions;

namespace AVN.Business;

public class OrderService
{
    private readonly AppDbContext _dbContext;

    public OrderService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Student> GetStudentsByFullName(string fullName)
    {
        try
        {
            string[] nameParts = fullName.Split(' ');
            var validNameParts = nameParts.Where(part => !string.IsNullOrEmpty(part) && part.Length >= 3);
            var students = _dbContext.Students.ToList().Where(s =>
                validNameParts.Any(part =>
                    s.SName.Contains(part, StringComparison.OrdinalIgnoreCase) ||
                    s.Name.Contains(part, StringComparison.OrdinalIgnoreCase) ||
                    s.PName.Contains(part, StringComparison.OrdinalIgnoreCase)
                )
            ).ToList();

            return students;
        }
        catch (Exception ex)
        {
            throw new Exception();
        }

    }
    public List<Employee> GetEmployeesByFullName(string fullName)
    {
        try
        {
            string[] nameParts = fullName.Split(' ');
            var validNameParts = nameParts.Where(part => !string.IsNullOrEmpty(part) && part.Length >= 3);
            var employees = _dbContext.Employees.ToList().Where(s =>
                validNameParts.Any(part =>
                    s.SName.Contains(part, StringComparison.OrdinalIgnoreCase) ||
                    s.Name.Contains(part, StringComparison.OrdinalIgnoreCase) ||
                    s.PName.Contains(part, StringComparison.OrdinalIgnoreCase)
                )
            ).ToList();

            return employees;
        }
        catch (Exception ex)
        {
            throw new Exception();
        }

    }
    public OperationResult CreateStudentOrder(Order order, List<string> studentIds)
    {
        var result = new OperationResult();

        var students = GetStudentByIds(studentIds);
        if (students == null || students.Count <= 0)
        {
            result.Success = false;
            result.Message = "Не удалось получить данные о студентах.";
            return result;
        }

        var movementType = _dbContext.MovementTypes.FirstOrDefault(x => x.Id == order.MovementTypeId);
        if (movementType == null)
        {
            result.Success = false;
            result.Message = "Не удалось получить данные о типе перемещений.";
            return result;
        }

        var groupId = movementType.MoveType != MoveType.NextCourseTransfer ? order.GroupId : students.FirstOrDefault().GroupId;

        var group = _dbContext.Groups.FirstOrDefault(x => x.Id == groupId);
        if (group == null)
        {
            result.Success = false;
            result.Message = "Не удалось получить данные о группе.";
            return result;
        }

        var newAcademicYear = _dbContext.AcademicYears.FirstOrDefault(x => x.Id == order.AcademicYearId);
        var oldAcademicYear = _dbContext.AcademicYears.FirstOrDefault(x => x.Id == group.AcademicYearId);
        if (newAcademicYear == null || oldAcademicYear == null)
        {
            result.Success = false;
            result.Message = "Не удалось получить данные об учебном году.";
            return result;
        }

        if (movementType.MoveType.Equals(MoveType.NextCourseTransfer))
        {
            var currentCourse = group.Course;
            var currentTrainingPeriod = group.TrainingPeriod;

            if (newAcademicYear.YearTo <= oldAcademicYear.YearTo)
            {
                result.Success = false;
                result.Message = $"Нельзя переводить на следующий курс в текущем учебном году.";
                return result;
            }

            if (!CanSetNextCourse(currentTrainingPeriod, currentCourse))
            {
                result.Success = false;
                result.Message = $"Срок обучения: {currentTrainingPeriod.GetDisplayName()}. Текущий курс у группы: {currentCourse.GetCourseInWriting()}.";
                return result;
            }
            else
            {
                group.AcademicYearId = newAcademicYear.Id;
            }

            var changedCourse = SetNextCourseForGroup(currentCourse);
            if (currentCourse == changedCourse)
            {
                result.Success = false;
                result.Message = "Невозможно перевести студента на следующий курс.";
                return result;
            }
            else
            {
                group.Course = changedCourse;
            }

            try
            {
                _dbContext.Update(group);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Не удалось изменить данные группы: {ex.Message} ";
                return result;
            }

        }

        // Создаем приказ для каждого студента
        try
        {
            foreach (var student in students)
            {
                var newStudentOrder = new Order
                {
                    StudentId = student.Id,
                    Date = order.Date,
                    Number = order.Number,
                    Note = order?.Note ?? "",
                    Course = group.Course,
                    OrderTypeId = order.OrderTypeId,
                    MovementTypeId = movementType.Id,
                    AcademicYearId = newAcademicYear.Id,
                    GroupId = group.Id,
                };
                _dbContext.Orders.Add(newStudentOrder);
            }
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = $"Произошла ошибка при создании приказа: {ex.Message} ";
            return result;
        }

        // Создаем историю перемещений
        try
        {
            foreach (var student in students)
            {
                var newStudentMovement = new StudentMovement
                {
                    OldGroupId = student.GroupId,
                    NewGroupId = group.Id,
                    MovementDate = order.Date,
                    OrderNumber = order.Number,
                    MovementTypeId = movementType.Id,
                    AcademicYearId = newAcademicYear.Id,
                    StudentId = student.Id
                };
                _dbContext.StudentMovements.Add(newStudentMovement);
            }
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = $"Не удалось создать историю перемещений студентов: {ex.Message} ";
            return result;
        }

        // Меняем каждому студенту группу и статус
        try
        {
            foreach (var student in students)
            {
                student.GroupId = group.Id;
                student.Status = GetStudentStatusByGroupId(group.Id);
                _dbContext.Update(student);
            }
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = $"Не удалось изменить статус и группу у студентов: {ex.Message} ";
            return result;
        }

        // Создаем контракт для студента который только поступил и для тех которые перевелись на следующий курс
        try
        {
            if (movementType.MoveType == MoveType.Enlisted || movementType.MoveType == MoveType.NextCourseTransfer)
            {
                foreach (var student in students)
                {
                    if (student.EducationalLine == EducationalLine.Budget)
                        continue;
                    //Теперь у студента долг хы)
                    student.IsHasDebt = true;
                    _dbContext.Update(student);

                    decimal contract = CalculateContract(student.Id);
                    _dbContext.StudentPayments.Add(new StudentPayment
                    {
                        StudentId = student.Id,
                        Contract = contract,
                        Payed = 0,
                        Debt = contract,
                        RecruitmentYear = order.Date.Year,
                        AcademicYearId = newAcademicYear.Id,
                        Course = group.Course,
                        GroupId = student.GroupId
                    });
                }
            }
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = $"Ошибка при создании контракта для студентов: {ex.Message} ";
            return result;
        }

        // Создаем ведомость за текущий учебный год для студента который только поступил и для тех которые перевелись на следующий курс
        try
        {
            if (movementType.MoveType == MoveType.Enlisted || movementType.MoveType == MoveType.NextCourseTransfer)
            {
                foreach (var student in students)
                {
                    var studentSubjects = _dbContext.Subjects.Where(subject => subject.DepartmentId == 
                                        group.Direction.DepartmentId && subject.Course == group.Course);
                    foreach (var studentSubject in studentSubjects)
                    {
                        _dbContext.GradeBooks.Add(new GradeBook
                        {
                            Grade = Grades.DontPass,
                            Points = 0,
                            Date = null,
                            AcademicYearId = newAcademicYear.Id,
                            GroupId = group.Id,
                            SubjectId = studentSubject.Id,
                            StudentId = student.Id
                        });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = $"Ошибка при создании ведомости для студентов: {ex.Message} ";
            return result;
        }

        _dbContext.SaveChanges();

        result.Success = true;
        result.Message = "Приказ успешно создан.";
        return result;

    }

    private bool CanSetNextCourse(TrainingPeriod currentTrainingPeriod, Course currentCourse)
    {
        //var courseMapping = new Dictionary<TrainingPeriod, Course>
        //{
        //    { TrainingPeriod.OneYear, Course.First },
        //    { TrainingPeriod.TwoYear, Course.Second },
        //    { TrainingPeriod.ThreeYear, Course.Third },
        //    { TrainingPeriod.FourYear, Course.Fourth },
        //    { TrainingPeriod.FifthYear, Course.Fifth },
        //    { TrainingPeriod.SixYear, Course.Sixth }
        //};

        //// Check if the current course is the last course in the mapping
        //if (currentCourse != courseMapping.Last().Value)
        //    return true;   

        //// Return false if there is no next course
        //return false;

        if (currentCourse >= (Course)currentTrainingPeriod)
            return false;

        return true;
    }

    private Course SetNextCourseForGroup(Course currentCourse)
    {
        Course[] allCourses = (Course[])Enum.GetValues(typeof(Course));

        int currentIndex = Array.IndexOf(allCourses, currentCourse);
        int nextIndex = (currentIndex + 1) % allCourses.Length;

        if (nextIndex != 0) // Если следующий индекс не равен нулю, значит есть следующее значение
            return allCourses[nextIndex]; //возвращаем след курс
        else
            return currentCourse;
    }

    private List<Student> GetStudentByIds(List<string> studentIds)
    {
        var students = new List<Student>();
        foreach (var studentId in studentIds)
        {
            var student = _dbContext.Students.Single(x => x.Id == studentId);
            students.Add(student);
        }
        return students;
    }

    private decimal CalculateContract(string studentId)
    {
        var student = _dbContext.Students.Include(s => s.Group)
            .ThenInclude(g => g.Direction)
            .First(s => s.Id == studentId);

        var course = student.Group.Course;
        var direction = student.Group.Direction;
        var subjects = _dbContext.Subjects.Where(s => s.DepartmentId == direction.DepartmentId && s.Course == course).ToList();
        decimal contractValue = subjects.Sum(s => s.CreditCounts) * direction.CreditCost;
        return contractValue;
    }

    public Student SetStudentStatusAndGradeBookNumber()
    {
        Student student = new Student();
        int count = _dbContext.Students.Count();
        int studentsCount = count % 1000;
        string lastThreeNumbers = studentsCount.ToString("D5");

        int lastTwoNumbers = DateTime.Now.Year % 100;
        student.GradeBookNumber = $"{lastTwoNumbers.ToString("D2")}{lastThreeNumbers}";
        student.Status = StudentStatus.Enrollee;
        student.RecruitmentYear = DateTime.Now.Year;

        return student;
    }

    public StudentStatus GetStudentStatusByGroupId(string groupId)
    {
        GroupType groupType = _dbContext.Groups.SingleOrDefault(x => x.Id == groupId).GroupType;
        StudentStatus studentStatus = groupType switch
        {
            GroupType.Students => StudentStatus.Active,
            GroupType.Enrolled => StudentStatus.Enrollee,
            GroupType.Archived => StudentStatus.Transfer,
            GroupType.Graduated => StudentStatus.Graduated,
            GroupType.Expelled => StudentStatus.Expelled,
            GroupType.AcademicLeaved => StudentStatus.LeaveOfAbsence,
            _ => StudentStatus.Inactive
        };
        return studentStatus;
    }

    public OperationResult CreateStudentPaymentDetail(StudentPayment payment)
    {
        var result = new OperationResult();

        var model = new PaymentInvoice
        {
            Faculty = payment.Group.Direction.Department.Faculty.FacultyName,
            Department = payment.Group.Direction.Department.DepartmentName ?? "Нет данных",
            Direction = payment.Group.Direction.DirectionName ?? "Нет данных",
            Group = payment.Group.GroupName ?? "Нет данных",
            Course = payment.Group?.Course.GetCourseInWriting() ?? "Нет данных",
            FullName = $"{payment.Student.SName} {payment.Student.Name} {payment.Student.PName}",
            EducationForm = payment.Group.StudingForm.GetDisplayName(),
            AcademicDegree = payment.Group.AcademicDegree.GetDisplayName(),
            PaymentAccountNumber = GenerateRandomNumber(10),
            PaymentAmount = (int)payment.Contract,
            PaymentPurpose = "Оплата за обучение"
        };

        try
        {
            if (payment.Debt <= 0)
            {
                result.Success = false;
                result.Message = $"Текущий контракт полностью оплачен!";
                return result;
            }
            _dbContext.StudentPaymentDetails.Add(new StudentPaymentDetail
            {
                StudentPaymentId = payment.Id,
                SpecialPurpose = model.PaymentPurpose,
                Number = model.PaymentAccountNumber
            });
            _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = $"Ошибка при создании счета: {ex.Message} ";
            return result;
        }

        var reportGenerator = new ReportGenerator();
        var pdfInvoice = reportGenerator.GenerateStudentPaymentPdf(model);

        result.Success = true;
        result.Message = "Счет успешно создан!";
        result.Data = pdfInvoice;
        return result;
    }



    public string GenerateRandomNumber(int count)
    {
        Random random = new Random();
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < count; i++)
        {
            int randomNumber = random.Next(0, 10);
            stringBuilder.Append(randomNumber);
        }

        return stringBuilder.ToString();
    }

    public OperationResult StudentPaymentProcess(StudentPaymentDetail paymentDetail)
    {
        var result = new OperationResult();
        var contract = _dbContext.StudentPayments.FirstOrDefault(x => x.Id == paymentDetail.StudentPaymentId);
        if (contract == null)
        {
            result.Success = false;
            result.Message = $"Не удалось получить данные о контракте студента";
            return result;
        }

        var student = _dbContext.Students.FirstOrDefault(x => x.Id == contract.StudentId);
        if (student == null)
        {
            result.Success = false;
            result.Message = $"Не удалось получить данные о студенте";
            return result;
        }

        if (contract.Debt >= paymentDetail.Payment)
        {
            contract.Debt -= paymentDetail.Payment ?? 0;
            contract.Payed += paymentDetail.Payment ?? 0;
        }
        else if (contract.Debt < paymentDetail.Payment)
        {
            var overpayment = paymentDetail.Payment - contract.Debt;
            paymentDetail.SpecialPurpose = $"Оплата за обучение (Переплата {overpayment})";
            contract.Debt = 0;
            contract.Payed += paymentDetail.Payment ?? 0;
        }

        if (contract.Debt == 0)
        {
            student.IsHasDebt = false;

            var inactiveAccounts = _dbContext.StudentPaymentDetails.Where(x => x.StudentPaymentId == contract.Id && x.Payment == null && x.PaymentDate == null && x.Id != paymentDetail.Id).ToList();
            if (inactiveAccounts.Count > 0)
            {
                try
                {
                    _dbContext.RemoveRange(inactiveAccounts);
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.Message = $"Произошла ошибка при удалении неактивных счетов: {ex.Message} ";
                    return result;
                }
            }

        }

        try
        {
            _dbContext.Update(student);
            _dbContext.Update(contract);
            _dbContext.Update(paymentDetail);
            _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = $"Произошла ошибка при оплате: {ex.Message} ";
            return result;
        }

        result.Success = true;
        result.Message = "Оплата успешно проведена!";
        return result;
    }

}