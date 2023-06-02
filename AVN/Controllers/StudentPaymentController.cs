using AVN.Automapper;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using Microsoft.AspNetCore.Mvc;

namespace AVN.Web.Controllers
{
    public class StudentPaymentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public StudentPaymentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(string id)
        {
            var payments = (await unitOfWork.StudentPaymentRepository.GetAllAsync()).ToList().Where(x=> x.StudentId == id);
            var mappedPayments= mapper.Map<StudentPayment, StudentPaymentVM>(payments).ToList();
            if(mappedPayments == null)
                return View(new List<StudentPaymentVM>());
            return View(mappedPayments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentPayment payment)
        {
            if (ModelState.IsValid)
            {
                await unitOfWork.StudentPaymentRepository.CreateAsync(payment);
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var payment = await unitOfWork.StudentPaymentRepository.GetByIdAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentPayment payment)
        {
            if (id != payment.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await unitOfWork.StudentPaymentRepository.UpdateAsync(payment);
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var payment = await unitOfWork.StudentPaymentRepository.GetByIdAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await unitOfWork.StudentPaymentRepository.GetByIdAsync(id);
            await unitOfWork.StudentPaymentRepository.DeleteAsync(payment);
            return RedirectToAction(nameof(Index));
        }
    }
}
