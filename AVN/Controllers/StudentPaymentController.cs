using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AVN.Web.Controllers
{
    public class StudentPaymentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentPaymentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var payments = await _unitOfWork.StudentPaymentRepository.GetAllAsync();
            return View(payments);
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
                await _unitOfWork.StudentPaymentRepository.CreateAsync(payment);
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var payment = await _unitOfWork.StudentPaymentRepository.GetByIdAsync(id);
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
                await _unitOfWork.StudentPaymentRepository.UpdateAsync(payment);
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var payment = await _unitOfWork.StudentPaymentRepository.GetByIdAsync(id);
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
            var payment = await _unitOfWork.StudentPaymentRepository.GetByIdAsync(id);
            await _unitOfWork.StudentPaymentRepository.DeleteAsync(payment);
            return RedirectToAction(nameof(Index));
        }
    }
}
