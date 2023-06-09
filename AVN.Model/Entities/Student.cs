﻿using AVN.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace AVN.Model.Entities;

public class Student : BaseEntity<Student, string>
{
    public string SName { get; set; }
    public string Name { get; set; }
    public string PName { get; set; }
    public StudentStatus Status { get; set; }
    public DateTime DateOfBirth { get; set; }
    public EducationalLine EducationalLine { get; set; }
    public string GradeBookNumber { get; set; }
    public Gender Gender { get; set; }
    public Citizenship Citizenship { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public int RecruitmentYear{ get; set; }
    public bool IsHasDebt { get; set; }

    public int Score { get; set; }

    public string GroupId { get; set; }
    public virtual Group? Group { get; set; }

    public virtual ICollection<StudentPayment>? StudentPayments { get; set; }
    public virtual ICollection<Order>? Orders { get; set; }
    public virtual ICollection<StudentMovement>? StudentMovements { get; set; }
    public virtual ICollection<GradeBook>? GradeBook { get; set; }

    public Student()
    {
        StudentPayments = new List<StudentPayment>();
        Orders = new List<Order>();
        StudentMovements = new List<StudentMovement>();
        GradeBook = new List<GradeBook>();
    }

    [NotMapped]
    public string FullName
    {
        get
        {
            return this.GetFullName();
        }
    }

    public string GetFullName()
    {
        return SName + " " + Name + " " + PName;
    }

}