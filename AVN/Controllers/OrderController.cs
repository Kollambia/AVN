using AVN.Business;
using AVN.Data;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AVN.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly AppDbContext context;

        public OrderController(IUnitOfWork unitOfWork, AppDbContext context)
        {
            this.unitOfWork = unitOfWork;
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
            //return PartialView("PartialViews/_StudentFilters", new StudentsFilterVM());
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
                studentOrderService.AddOrder(order);

                if (studentOrderService.AddOrder(order))
                {
                    // возвращаться сообщение
                }

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
