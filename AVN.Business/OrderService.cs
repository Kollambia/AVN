using AVN.Common;
using AVN.Common.Customs;
using AVN.Common.Enums;
using AVN.Common.PdfGenerator;
using AVN.Data;
using AVN.Model.Entities;
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

    public OperationResult CreateStudentOrder(Order order, List<string> studentIds)
    {
        var result = new OperationResult();

        var students = GetStudentByIds(studentIds);
        if (students == null)
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

        var group = _dbContext.Groups.FirstOrDefault(x => x.Id == order.GroupId);
        if (group == null)
        {
            result.Success = false;
            result.Message = "Не удалось получить данные о группе.";
            return result;
        }

        var academicYear = _dbContext.AcademicYears.FirstOrDefault(x => x.Id == order.AcademicYearId);
        if (academicYear == null)
        {
            result.Success = false;
            result.Message = "Не удалось получить данные об учебном году.";
            return result;
        }

        if (movementType.MoveType.Equals(MoveType.NextCourseTransfer))
        {
            var currentCourse = group.Course;
            var currentTrainingPeriod = group.TrainingPeriod;

            if (CanTranslateInCurrentAcademicYear(group.Id, academicYear.Id))
            {
                result.Success = false;
                result.Message = $"Нельзя переводить на следующий курс в текущем учебном году.";
                return result;
            }

            if (!CanSetNextCourse(currentTrainingPeriod, currentCourse))
            {
                result.Success = false;
                result.Message = $"Срок обучения: {currentTrainingPeriod.GetDisplayName()}. Текущий курс группы {currentCourse}.";
                return result;
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
                    AcademicYearId = academicYear.Id,
                    GroupId = group.Id
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
                    AcademicYearId = academicYear.Id,
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
                    decimal contract = CalculateContract(student.Id);
                    _dbContext.StudentPayments.Add(new StudentPayment
                    {
                        StudentId = student.Id,
                        Contract = contract,
                        Payed = 0,
                        Debt = contract,
                        RecruitmentYear = order.Date.Year,
                        AcademicYearId = academicYear.Id,
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

        _dbContext.SaveChanges();

        result.Success = true;
        result.Message = "Приказ успешно создан.";
        return result;

    }

    private bool CanTranslateInCurrentAcademicYear(string groupId, int currentAcademicYearId)
    {
        var nextCourseTransferMoveType = _dbContext.MovementTypes.Single(x => x.MoveType.Equals(MoveType.NextCourseTransfer));

        return _dbContext.Orders.Where(x => x.GroupId == groupId &&
                                        x.MovementTypeId == nextCourseTransferMoveType.Id)
                                        .Any(x => x.AcademicYearId == currentAcademicYearId);
    }

    private bool CanSetNextCourse(TrainingPeriod trainingPeriod, Course currentCourse)
    {
        var courseMapping = new Dictionary<TrainingPeriod, Course>
        {
            { TrainingPeriod.OneYear, Course.Second },
            { TrainingPeriod.TwoYear, Course.Third },
            { TrainingPeriod.ThreeYear, Course.Fourth },
            { TrainingPeriod.FourYear, Course.Fifth },
            { TrainingPeriod.FifthYear, Course.Sixth },
            { TrainingPeriod.SixYear, Course.Sixth }
        };

        // Check if the current course is the last course in the mapping
        if (currentCourse != courseMapping.Last().Value)
            return true;   

        // Return false if there is no next course
        return false;
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
        decimal contractValue = subjects.Sum(s => int.Parse(s.CreditHours)) * direction.CreditCost;
        return contractValue;
    }

    public Student SetStudentStatusAndGradeBookNumber(Student student)
    {
        int count = _dbContext.Students.Count();
        int studentsCount = count % 1000;
        string lastThreeNumbers = studentsCount.ToString("D3");

        student.GradeBookNumber = $"{DateTime.Now.Year}{lastThreeNumbers}";
        //student.Status = StudentStatus.Enlisted;

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

    public byte[]? CreateStudentPaymentDetail(StudentPayment payment)
    {
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
            PaymentAccountNumber = GeneratePaymentAccountNumber(),
            PaymentAmount = (int)payment.Contract,
            PaymentPurpose = "Оплата за обучение"
        };

        try
        {
            _dbContext.StudentPaymentDetails.Add(new StudentPaymentDetail
            {
                StudentPaymentId = payment.Id,
                SpecialPurpose = model.PaymentPurpose,
                Number = model.PaymentAccountNumber,
                Payment = 0,
                PaymentDate = DateTime.Now,
                PaymentType = PaymentType.Cash
            });
            _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            return null;
        }

        var invoiceGenerator = new InvoiceGenerator();
        var pdfInvoice = invoiceGenerator.GenerateStudentPaymentPdf(model);
        return File.ReadAllBytes(pdfInvoice);
    }
    private static string GeneratePaymentAccountNumber()
    {
        Random random = new Random();
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < 10; i++)
        {
            int randomNumber = random.Next(0, 10);
            stringBuilder.Append(randomNumber);
        }

        return stringBuilder.ToString();
    }

}