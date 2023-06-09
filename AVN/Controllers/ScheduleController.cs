﻿using AVN.Automapper;
using AVN.Data;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AVN.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly AppDbContext context;
        public ScheduleController(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext context)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Schedule/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(context.Groups, "Id", "GroupName");
            ViewData["SubjectId"] = new SelectList(context.Subjects, "Id", "Title");
            ViewData["EmployeeId"] = new SelectList(context.Employees, "Id", "FullName");
            
            var model = new List<ScheduleVM>
            {
                new ScheduleVM
                {
                    Schedules = new List<Schedule> { new Schedule(), new Schedule() },

                    SubjectSelectList = unitOfWork.SubjectRepository.GetAll().Select(i => new SelectListItem
                    {
                        Text = i.Title,
                        Value = i.Id.ToString()
                    }),

                    EmployeeSelectList = unitOfWork.EmployeeRepository.GetAll().Select(i => new SelectListItem()
                    {
                        Text = i.FullName,
                        Value = i.Id.ToString()
                    }),

                    GroupSelectList = unitOfWork.GroupRepository.GetAll().Select(i => new SelectListItem()
                    {
                        Text = i.GroupName,
                        Value = i.Id.ToString()
                    })
                },


            };

            for (var i = 0; i < model.Count; i++)
            {
                ViewData[$"[{i}].SubjectId"] = ViewData["SubjectId"];
                ViewData[$"[{i}].GroupId"] = ViewData["GroupId"];
                ViewData[$"[{i}].EmployeeId"] = ViewData["EmployeeId"];
            }
            ViewData["AcademicYears"] = new SelectList(context.AcademicYears, "Id", "Name");


            return View(model);
        }

        // POST: Schedules/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<ScheduleVM> schedules)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    var mappedSubjects = mapper.Map<ScheduleVM, Schedule>(schedules);

                    foreach (var schedule in mappedSubjects)
                    {
                        new ScheduleVM
                        {
                            SubjectSelectList = unitOfWork.SubjectRepository.GetAll().Select(i => new SelectListItem
                            {
                                Text = i.Title,
                                Value = i.Id.ToString()
                            }),

                            EmployeeSelectList = unitOfWork.EmployeeRepository.GetAll().Select(i => new SelectListItem()
                            {
                                Text = i.FullName,
                                Value = i.Id.ToString()
                            }),

                            GroupSelectList = unitOfWork.GroupRepository.GetAll().Select(i => new SelectListItem()
                            {
                                Text = i.GroupName,
                                Value = i.Id.ToString()
                            }),

                           
                        };
                       
                        // Add schedule to database
                        context.Add(schedule);
                    }

                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                    return RedirectToAction("Index", "Schedule");
                }
                
            }
            ViewData["GroupId"] = new SelectList(context.Groups, "Id", "GroupName");
            ViewData["SubjectId"] = new SelectList(context.Subjects, "Id", "Title");
            ViewData["EmployeeId"] = new SelectList(context.Employees, "Id", "Name");
            ViewData["AcademicYears"] = new SelectList(context.AcademicYears, "Id", "Name");

            return View(schedules);
        }

        // GET: Schedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await context.Schedules.FindAsync(id);
            var mappedSchedult = mapper.Map<Schedule, ScheduleVM>(schedule);
            if (mappedSchedult == null)
            {
                return NotFound();
            }

            ViewData["GroupId"] = new SelectList(context.Groups, "Id", "GroupName", mappedSchedult.GroupId);
            ViewData["SubjectId"] = new SelectList(context.Subjects, "Id", "Title", mappedSchedult.SubjectId);
            ViewData["EmployeeId"] = new SelectList(context.Employees, "Id", "FullName", mappedSchedult.EmployeeId);
            ViewData["AcademicYears"] = new SelectList(context.AcademicYears, "Id", "Name", mappedSchedult.AcademicYearId);

            return View(mappedSchedult);
        }

        // POST: Schedules/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ScheduleVM schedule)
        {
            if (id != schedule.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    var mappedSchedult = mapper.Map<ScheduleVM, Schedule>(schedule);
                    await unitOfWork.ScheduleRepository.UpdateAsync(mappedSchedult);
                    await unitOfWork.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["GroupId"] = new SelectList(context.Groups, "Id", "GroupName", schedule.GroupId);
            ViewData["SubjectId"] = new SelectList(context.Subjects, "Id", "Title", schedule.SubjectId);
            ViewData["EmployeeId"] = new SelectList(context.Employees, "Id", "Name", schedule.EmployeeId);
            ViewData["AcademicYears"] = new SelectList(context.AcademicYears, "Id", "Name", schedule.AcademicYearId);

            return View(schedule);
        }

        // GET: Schedule/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var schedule = await unitOfWork.ScheduleRepository.GetByIdAsync(id);

            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Subject/Delete/5

        public IActionResult DeleteConfirmed(string groupId)
        {
            // Найти все записи в расписании для данной группы
            var schedulesForGroup = context.Schedules.Where(s => s.Group.Id == groupId).ToList();

            if (!schedulesForGroup.Any())
            {
                return NotFound();
            }

            // Удалить все найденные записи
            context.Schedules.RemoveRange(schedulesForGroup);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        public async Task<ActionResult> ScheduleList(int facultyId, int departmentId, int directionId, string groupId, int groupType, int academYearId)
        {
            var schedule = await unitOfWork.ScheduleRepository.GetAllAsync();
            
            if (!string.IsNullOrEmpty(groupId))
            {
                schedule = schedule.Where(x => x.GroupId == groupId);
            }
            else if (directionId > 0)
            {
                schedule.Where(x => x.Group.DirectionId == directionId);
            }
            else if (departmentId > 0)
            {
                schedule.Where(x => x.Group.Direction.DepartmentId == departmentId);
            }
            else if (facultyId > 0)
            {
                schedule = schedule.Where(x => x.Group.Direction.Department.FacultyId == facultyId);
            }
            else if (groupType > 0)
            {
                schedule = schedule.Where(x => (int)x.Group.GroupType == groupType);
            }

            else
            {
                return PartialView(new List<ScheduleVM>());
            }

            if (groupType > 0)
            {
                
            }

            if (academYearId > 0)
            {
                schedule = schedule.Where(x => x.AcademicYearId == academYearId);
            }

            var mappedSchedule = schedule.Select(s => mapper.Map<Schedule, ScheduleVM>(s)).ToList();
            return PartialView(mappedSchedule ?? new List<ScheduleVM>());
        }

    }
}