using AVN.Common.Enums;
using AVN.Data;
using AVN.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace AVN.Business;

public class OrderService
{
    private readonly AppDbContext _dbContext;

    public OrderService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool AddOrder(Order order)
    {
        // Валидация на зачисление студента
        if (order.OrderType == OrderType.Enrollment && _dbContext.Order.Any(o => o.StudentId == order.StudentId && o.OrderType == OrderType.Enrollment))
        {
            return false;
        }

        // Валидация на ежегодный перевод студента
        if (order.OrderType == OrderType.Translation
            && _dbContext.Order.Any(o => o.StudentId == order.StudentId && o.OrderType == OrderType.Translation && o.Date.Year == DateTime.Now.Year)
            && _dbContext.StudentPayments.Any(sp => sp.StudentId == order.StudentId && sp.AcademicYear == DateTime.Now.Year && sp.Debt > 0))
        {
            return false;
        }

        _dbContext.Order.Add(order);

        if (order.OrderType == OrderType.Enrollment || order.OrderType == OrderType.Translation)
        {
            decimal contract = CalculateContract(order.StudentId);
            _dbContext.StudentPayments.Add(new StudentPayment
            {
                StudentId = order.StudentId,
                Contract = contract,
                Payed = 0,
                Debt = contract,
                AcademicYear = order.Date.Year
            });
        }

        _dbContext.SaveChanges();
        return true;
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
}