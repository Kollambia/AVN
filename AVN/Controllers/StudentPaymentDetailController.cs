using AVN.Automapper;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using Microsoft.AspNetCore.Mvc;

public class StudentPaymentDetailsController : Controller
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public StudentPaymentDetailsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        return View(await unitOfWork.StudentPaymentDetailRepository.GetAllAsync());
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
            await unitOfWork.StudentPaymentDetailRepository.CreateAsync(studentPaymentDetail);
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

        var studentPaymentDetail = await unitOfWork.StudentPaymentDetailRepository.GetByIdAsync(id.Value);
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
            await unitOfWork.StudentPaymentDetailRepository.UpdateAsync(studentPaymentDetail);
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

        var studentPaymentDetail = await unitOfWork.StudentPaymentDetailRepository.GetByIdAsync(id.Value);
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
        var studentPaymentDetail = await unitOfWork.StudentPaymentDetailRepository.GetByIdAsync(id);
        await unitOfWork.StudentPaymentDetailRepository.DeleteAsync(studentPaymentDetail);
        return RedirectToAction(nameof(Index));
    }

    public async Task<ActionResult> RefreshStudentPaymentDetails(int id)
    {
        var paymentDetails = (await unitOfWork.StudentPaymentDetailRepository.GetAllAsync()).Where(x => x.StudentPaymentId == id).ToList();
        var mappedpaymentDetails = mapper.Map<StudentPaymentDetail, StudentPaymentDetailVM>(paymentDetails).ToList();
        return PartialView("PartialViews/_PaymentDetails", mappedpaymentDetails);
    }

}
