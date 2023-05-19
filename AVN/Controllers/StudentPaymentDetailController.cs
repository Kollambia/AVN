using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using Microsoft.AspNetCore.Mvc;

public class StudentPaymentDetailsController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public StudentPaymentDetailsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _unitOfWork.StudentPaymentDetailRepository.GetAllAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Payment,PaymentDate,Number,PaymentType,SpecialPurpose,StudentPaymentId")] StudentPaymentDetail studentPaymentDetail)
    {
        if (ModelState.IsValid)
        {
            await _unitOfWork.StudentPaymentDetailRepository.CreateAsync(studentPaymentDetail);
            return RedirectToAction(nameof(Index));
        }
        return View(studentPaymentDetail);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var studentPaymentDetail = await _unitOfWork.StudentPaymentDetailRepository.GetByIdAsync(id.Value);
        if (studentPaymentDetail == null)
        {
            return NotFound();
        }
        return View(studentPaymentDetail);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Payment,PaymentDate,Number,PaymentType,SpecialPurpose,StudentPaymentId")] StudentPaymentDetail studentPaymentDetail)
    {
        if (id != studentPaymentDetail.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _unitOfWork.StudentPaymentDetailRepository.UpdateAsync(studentPaymentDetail);
            return RedirectToAction(nameof(Index));
        }
        return View(studentPaymentDetail);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var studentPaymentDetail = await _unitOfWork.StudentPaymentDetailRepository.GetByIdAsync(id.Value);
        if (studentPaymentDetail == null)
        {
            return NotFound();
        }

        return View(studentPaymentDetail);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var studentPaymentDetail = await _unitOfWork.StudentPaymentDetailRepository.GetByIdAsync(id);
        await _unitOfWork.StudentPaymentDetailRepository.DeleteAsync(studentPaymentDetail);
        return RedirectToAction(nameof(Index));
    }
}
