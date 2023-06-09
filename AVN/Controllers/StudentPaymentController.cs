﻿using AVN.Automapper;
using AVN.Business;
using AVN.Common.Enums;
using AVN.Data;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using Microsoft.AspNetCore.Mvc;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace AVN.Web.Controllers
{
    public class StudentPaymentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly AppDbContext context;
        public StudentPaymentController(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext context)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.context = context;
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


        [HttpGet]
        public async Task<ActionResult> Payment(string orderNumber)
        {
            if (string.IsNullOrEmpty(orderNumber))
                return View();

            var paymentDetail = (await unitOfWork.StudentPaymentDetailRepository.FindByConditionAsync(x => x.Number == orderNumber)).FirstOrDefault();
            if (paymentDetail == null)
                return View();

            var paymentDetailVM = mapper.Map<StudentPaymentDetail, StudentPaymentDetailVM>(paymentDetail);
            return View(paymentDetailVM);
        }


        [HttpPost]
        public ActionResult Payment(StudentPaymentDetailVM paymentDetailVM)
        {
            var studentOrderService = new OrderService(context);
            if (ModelState.IsValid)
            {
                var mappings = mapper.Map<StudentPaymentDetailVM, StudentPaymentDetail>(paymentDetailVM);
                var result = studentOrderService.StudentPaymentProcess(mappings);
                if (result.Success)
                    return View();
                else
                    return View(paymentDetailVM);
            }
            return View(paymentDetailVM);
        }

        public async Task<ActionResult> StudentPaymentDetailList(string orderNumber, string studentName)
        {
            List<StudentPaymentDetail> studentPaymentDetailsAll = new List<StudentPaymentDetail>();

            if (!string.IsNullOrEmpty(orderNumber))
            {
                var paymentDetail = (await unitOfWork.StudentPaymentDetailRepository.GetAllAsync()).FirstOrDefault(x => x.Number == orderNumber);
                if (paymentDetail == null)
                    return PartialView(new List<StudentPaymentDetailVM>());

                var studentPaymentDetails = (await unitOfWork.StudentPaymentDetailRepository.GetAllAsync()).Where(x => x.StudentPaymentId == paymentDetail.StudentPaymentId);
                studentPaymentDetailsAll.AddRange(studentPaymentDetails);
            }
            else if (!string.IsNullOrEmpty(studentName))
            {
                var studentOrderService = new OrderService(context);
                var students = studentOrderService.GetStudentsByFullName(studentName);

                foreach (var student in students)
                {
                    if (!student.StudentPayments.Any())
                        continue;

                    var studentPaymentDetails = student.StudentPayments
                        .SelectMany(sp => sp.PaymentDetails)
                        .ToList();

                    studentPaymentDetailsAll.AddRange(studentPaymentDetails);
                }

            }
            else
            {
                return PartialView(new List<StudentPaymentDetailVM>());
            }

            var mappings = mapper.Map<StudentPaymentDetail, StudentPaymentDetailVM>(studentPaymentDetailsAll);
            return PartialView(mappings ?? new List<StudentPaymentDetailVM>());
        }

        public async Task<ActionResult> StudentPaymentDetails(string orderNumber)
        {
            if (string.IsNullOrEmpty(orderNumber))
                return RedirectToAction("Payment");
            var paymentDetail = (await unitOfWork.StudentPaymentDetailRepository.GetAllAsync()).FirstOrDefault(x => x.Number == orderNumber);
            return RedirectToAction("Payment", new { paymentDetailId = paymentDetail?.Id });
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
