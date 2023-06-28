using AutoMapper;
using AVN.Business;
using AVN.Data;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AVN.Controllers;
using AVN.Common.Customs;
using Microsoft.EntityFrameworkCore;
using AVN.Common.Enums;

namespace AVN.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly AppDbContext context;
        private readonly UserManager<AppUser> userManager;

        public OrderController(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext context, UserManager<AppUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.context = context;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var studentOrderService = new OrderService(context);
            OrderVM orderInfo = new OrderVM
            {
                Number = studentOrderService.GenerateRandomNumber(8),
                Date = DateTime.Now
            };
            return View(orderInfo);
        }

        [HttpPost]
        public IActionResult Index(OrderVM orderInfo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<string> studentIds = new List<string>();
                    var mappedOrder = mapper.Map<OrderVM, Order>(orderInfo);
                    var moveType = unitOfWork.MovementTypeRepository.GetById(mappedOrder.MovementTypeId);
                    if (moveType == null)
                    {
                        TempData["error"] = $"Тип перемещения не найден.";
                    }
                    else if (moveType.MoveType == MoveType.NextCourseTransfer)
                    {
                        studentIds = StudentController.TransferExportStudents.Select(x => x.Id).ToList();

                    }
                    else
                    {
                        studentIds = StudentController.TransferImportStudents
                                               .Where(x => x.Transfered == true).Select(x => x.Id).ToList();
                    }
                   
                    var studentOrderService = new OrderService(context);
                    OperationResult result = studentOrderService.CreateStudentOrder(mappedOrder, studentIds);
                    if (result.Success)
                    {
                        TempData["success"] = "Приказ успешно сформирован";
                        StudentController.TransferImportStudents.Clear();
                        StudentController.TransferExportStudents.Clear();
                        return View();
                    }
                    else
                    {
                        TempData["error"] = result.Message;
                    }
                }
                return View(orderInfo);
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Student");
            }
            
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Order order)
        {
            if (ModelState.IsValid)
            {
                await unitOfWork.OrderRepository.CreateAsync(order);
                await unitOfWork.SaveChangesAsync();

                var studentOrderService = new OrderService(context);
                //studentOrderService.AddOrder(order);

                //if (studentOrderService.AddOrder(order))
                //{
                //    // возвращаться сообщение
                //}

                return RedirectToAction(nameof(Index));

            }

            return View(order);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var order = await unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await unitOfWork.OrderRepository.UpdateAsync(order);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var order = await unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await unitOfWork.OrderRepository.DeleteByIdAsync(id);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Order");
            }
    
        }
    }
}
