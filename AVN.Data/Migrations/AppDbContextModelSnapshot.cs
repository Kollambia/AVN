﻿// <auto-generated />
using System;
using AVN.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AVN.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AVN.Model.Entities.AcademicYear", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearFrom")
                        .HasColumnType("int");

                    b.Property<int>("YearTo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AcademicYears");
                });

            modelBuilder.Entity("AVN.Model.Entities.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("AcademicDegree")
                        .HasColumnType("int");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Citizenship")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<int?>("DirectionId")
                        .HasColumnType("int");

                    b.Property<int?>("EducationalLine")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int?>("FacultyId")
                        .HasColumnType("int");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("GradeBookNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("StudingForm")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("AVN.Model.Entities.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DepartmentShortName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FacultyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("AVN.Model.Entities.Direction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AcademicDegree")
                        .HasColumnType("int");

                    b.Property<decimal>("CreditCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DirectionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DirectionNumber")
                        .HasColumnType("int");

                    b.Property<string>("DirectionShortName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrainingPeriod")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Directions");
                });

            modelBuilder.Entity("AVN.Model.Entities.Employee", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasMaxLength(100)
                        .HasColumnType("datetime2");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<string>("SName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("AVN.Model.Entities.Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DeanName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FacultyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FacultyShortName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("AVN.Model.Entities.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AcademicDegree")
                        .HasColumnType("int");

                    b.Property<int?>("AcademicYearId")
                        .HasColumnType("int");

                    b.Property<int>("Course")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DirectionId")
                        .HasColumnType("int");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GroupType")
                        .HasColumnType("int");

                    b.Property<int>("StudingForm")
                        .HasColumnType("int");

                    b.Property<int>("TrainingPeriod")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AcademicYearId");

                    b.HasIndex("DirectionId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("AVN.Model.Entities.MovementType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MoveType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MovementTypes");
                });

            modelBuilder.Entity("AVN.Model.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AcademicYearId")
                        .HasColumnType("int");

                    b.Property<int>("Course")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("MovementTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderTypeId")
                        .HasColumnType("int");

                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AcademicYearId");

                    b.HasIndex("GroupId");

                    b.HasIndex("MovementTypeId");

                    b.HasIndex("OrderTypeId");

                    b.HasIndex("StudentId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("AVN.Model.Entities.OrderType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("MovementTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MovementTypeId");

                    b.ToTable("OrderTypes");
                });

            modelBuilder.Entity("AVN.Model.Entities.Student", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Citizenship")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<int>("EducationalLine")
                        .HasColumnType("int");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("GradeBookNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<bool>("IsHasDebt")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RecruitmentYear")
                        .HasColumnType("int");

                    b.Property<string>("SName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("AVN.Model.Entities.StudentMovement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AcademicYearId")
                        .HasColumnType("int");

                    b.Property<DateTime>("MovementDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MovementTypeId")
                        .HasColumnType("int");

                    b.Property<int>("NewGroupId")
                        .HasColumnType("int");

                    b.Property<int>("OldGroupId")
                        .HasColumnType("int");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("StudentId1")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AcademicYearId");

                    b.HasIndex("MovementTypeId");

                    b.HasIndex("StudentId1");

                    b.ToTable("StudentMovements");
                });

            modelBuilder.Entity("AVN.Model.Entities.StudentPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AcademicYearId")
                        .HasColumnType("int");

                    b.Property<decimal>("Contract")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Course")
                        .HasColumnType("int");

                    b.Property<decimal>("Debt")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<decimal>("Payed")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RecruitmentYear")
                        .HasColumnType("int");

                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AcademicYearId");

                    b.HasIndex("GroupId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentPayments");
                });

            modelBuilder.Entity("AVN.Model.Entities.StudentPaymentDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Payment")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentType")
                        .HasColumnType("int");

                    b.Property<string>("SpecialPurpose")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StudentPaymentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentPaymentId");

                    b.ToTable("StudentPaymentDetails");
                });

            modelBuilder.Entity("AVN.Model.Entities.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Course")
                        .HasColumnType("int");

                    b.Property<string>("CreditHours")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("AVN.Model.Entities.SubjectEmployee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("EmployeeCreditHours")
                        .HasColumnType("int");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("EmployeeId1")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId1");

                    b.HasIndex("SubjectId");

                    b.ToTable("SubjectEmployees");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("AVN.Model.Entities.AppUser", b =>
                {
                    b.HasOne("AVN.Model.Entities.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("AVN.Model.Entities.Department", b =>
                {
                    b.HasOne("AVN.Model.Entities.Faculty", "Faculty")
                        .WithMany("Departments")
                        .HasForeignKey("FacultyId");

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("AVN.Model.Entities.Direction", b =>
                {
                    b.HasOne("AVN.Model.Entities.Department", "Department")
                        .WithMany("Directions")
                        .HasForeignKey("DepartmentId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("AVN.Model.Entities.Employee", b =>
                {
                    b.HasOne("AVN.Model.Entities.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("AVN.Model.Entities.Group", b =>
                {
                    b.HasOne("AVN.Model.Entities.AcademicYear", "AcademicYear")
                        .WithMany("Groups")
                        .HasForeignKey("AcademicYearId");

                    b.HasOne("AVN.Model.Entities.Direction", "Direction")
                        .WithMany("Groups")
                        .HasForeignKey("DirectionId");

                    b.Navigation("AcademicYear");

                    b.Navigation("Direction");
                });

            modelBuilder.Entity("AVN.Model.Entities.Order", b =>
                {
                    b.HasOne("AVN.Model.Entities.AcademicYear", "AcademicYear")
                        .WithMany("Orders")
                        .HasForeignKey("AcademicYearId");

                    b.HasOne("AVN.Model.Entities.Group", "Group")
                        .WithMany("Orders")
                        .HasForeignKey("GroupId");

                    b.HasOne("AVN.Model.Entities.MovementType", "MovementType")
                        .WithMany("Orders")
                        .HasForeignKey("MovementTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AVN.Model.Entities.OrderType", "OrderType")
                        .WithMany("Orders")
                        .HasForeignKey("OrderTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AVN.Model.Entities.Student", "Student")
                        .WithMany("Orders")
                        .HasForeignKey("StudentId");

                    b.Navigation("AcademicYear");

                    b.Navigation("Group");

                    b.Navigation("MovementType");

                    b.Navigation("OrderType");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("AVN.Model.Entities.OrderType", b =>
                {
                    b.HasOne("AVN.Model.Entities.MovementType", "MovementType")
                        .WithMany("OrderTypes")
                        .HasForeignKey("MovementTypeId");

                    b.Navigation("MovementType");
                });

            modelBuilder.Entity("AVN.Model.Entities.Student", b =>
                {
                    b.HasOne("AVN.Model.Entities.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("AVN.Model.Entities.StudentMovement", b =>
                {
                    b.HasOne("AVN.Model.Entities.AcademicYear", "AcademicYear")
                        .WithMany("StudentMovements")
                        .HasForeignKey("AcademicYearId");

                    b.HasOne("AVN.Model.Entities.MovementType", "MovementType")
                        .WithMany("StudentMovements")
                        .HasForeignKey("MovementTypeId");

                    b.HasOne("AVN.Model.Entities.Student", "Student")
                        .WithMany("StudentMovements")
                        .HasForeignKey("StudentId1");

                    b.Navigation("AcademicYear");

                    b.Navigation("MovementType");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("AVN.Model.Entities.StudentPayment", b =>
                {
                    b.HasOne("AVN.Model.Entities.AcademicYear", "AcademicYear")
                        .WithMany("StudentPayments")
                        .HasForeignKey("AcademicYearId");

                    b.HasOne("AVN.Model.Entities.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AVN.Model.Entities.Student", "Student")
                        .WithMany("StudentPayments")
                        .HasForeignKey("StudentId");

                    b.Navigation("AcademicYear");

                    b.Navigation("Group");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("AVN.Model.Entities.StudentPaymentDetail", b =>
                {
                    b.HasOne("AVN.Model.Entities.StudentPayment", "StudentPayment")
                        .WithMany("PaymentDetails")
                        .HasForeignKey("StudentPaymentId");

                    b.Navigation("StudentPayment");
                });

            modelBuilder.Entity("AVN.Model.Entities.Subject", b =>
                {
                    b.HasOne("AVN.Model.Entities.Department", "Department")
                        .WithMany("Subjects")
                        .HasForeignKey("DepartmentId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("AVN.Model.Entities.SubjectEmployee", b =>
                {
                    b.HasOne("AVN.Model.Entities.Employee", "Employee")
                        .WithMany("SubjectEmployees")
                        .HasForeignKey("EmployeeId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AVN.Model.Entities.Subject", "Subject")
                        .WithMany("SubjectEmployees")
                        .HasForeignKey("SubjectId");

                    b.Navigation("Employee");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("AVN.Model.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("AVN.Model.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AVN.Model.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("AVN.Model.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AVN.Model.Entities.AcademicYear", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("Orders");

                    b.Navigation("StudentMovements");

                    b.Navigation("StudentPayments");
                });

            modelBuilder.Entity("AVN.Model.Entities.Department", b =>
                {
                    b.Navigation("Directions");

                    b.Navigation("Employees");

                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("AVN.Model.Entities.Direction", b =>
                {
                    b.Navigation("Groups");
                });

            modelBuilder.Entity("AVN.Model.Entities.Employee", b =>
                {
                    b.Navigation("SubjectEmployees");
                });

            modelBuilder.Entity("AVN.Model.Entities.Faculty", b =>
                {
                    b.Navigation("Departments");
                });

            modelBuilder.Entity("AVN.Model.Entities.Group", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("AVN.Model.Entities.MovementType", b =>
                {
                    b.Navigation("OrderTypes");

                    b.Navigation("Orders");

                    b.Navigation("StudentMovements");
                });

            modelBuilder.Entity("AVN.Model.Entities.OrderType", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("AVN.Model.Entities.Student", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("StudentMovements");

                    b.Navigation("StudentPayments");
                });

            modelBuilder.Entity("AVN.Model.Entities.StudentPayment", b =>
                {
                    b.Navigation("PaymentDetails");
                });

            modelBuilder.Entity("AVN.Model.Entities.Subject", b =>
                {
                    b.Navigation("SubjectEmployees");
                });
#pragma warning restore 612, 618
        }
    }
}
