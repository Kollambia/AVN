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
            return View();
        }

        [HttpPost]
        public IActionResult Index(OrderVM orderInfo)
        {
            if (ModelState.IsValid)
            {
                var mappedOrder = mapper.Map<OrderVM, Order>(orderInfo);
                var transferImportStudentIds = StudentController.TransferImportStudents
                    .Where(x => x.Transfered == true).Select(x => x.Id).ToList();

                var studentOrderService = new OrderService(context);
                OperationResult result = studentOrderService.CreateStudentOrder(mappedOrder, transferImportStudentIds);
                //to to alert
                if (result.Success)
                {
                    StudentController.TransferImportStudents.Clear();
                    StudentController.TransferExportStudents.Clear();
                }
                return View();
            }
            return View(orderInfo);
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
            await unitOfWork.OrderRepository.DeleteByIdAsync(id);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
