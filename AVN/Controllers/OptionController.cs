using AVN.Automapper;
using AVN.Business;
using AVN.Data;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AVN.Controllers
{
    public class OptionController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly AppDbContext context;
        public OptionController(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext context)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.context = context;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> AcademicYearList()
        {
            var list = await unitOfWork.AcademicYearRepository.GetAllAsync();
            var mappedList = mapper.Map<AcademicYear, AcademicYearVM>(list).ToList();
            return PartialView(mappedList ?? new List<AcademicYearVM>());
        }

        public async Task<ActionResult> MovementTypeList()
        {
            var list = await unitOfWork.MovementTypeRepository.GetAllAsync();
            var mappedList = mapper.Map<MovementType, MovementTypeVM>(list).ToList();
            return PartialView(mappedList ?? new List<MovementTypeVM>());
        }

        public async Task<ActionResult> OrderTypeList()
        {
            var list = await unitOfWork.OrderTypeRepository.GetAllAsync();
            var mappedList = mapper.Map<OrderType, OrderTypeVM>(list).ToList();
            return PartialView(mappedList ?? new List<OrderTypeVM>());
        }

        public IActionResult CreateAcademicYear()
        {
            return View();
        }

        public IActionResult CreateMovementType()
        {
            return View();
        }

        public IActionResult CreateOrderType()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAcademicYear(AcademicYearVM entityVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var mappedEntity = mapper.Map<AcademicYearVM, AcademicYear>(entityVM);
                    var isYearRangeExistInDB = unitOfWork.AcademicYearRepository.GetAll().Any(x => x.YearFrom == mappedEntity.YearFrom);
                    if (isYearRangeExistInDB)
                    {
                        TempData["error"] = "Текущий учебный год уже существует";
                        return View(entityVM);
                    }

                    mappedEntity.Name = mappedEntity.YearFrom.ToString() + "-" + mappedEntity.YearTo.ToString();
                    await unitOfWork.AcademicYearRepository.CreateAsync(mappedEntity);
                    await unitOfWork.SaveChangesAsync();

                    TempData["success"] = "Запись успешно добавлена";
                    return RedirectToAction(nameof(Index));
                }
                
                return View(entityVM);
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMovementType(MovementTypeVM entityVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var mappedEntity = mapper.Map<MovementTypeVM, MovementType>(entityVM);
                    await unitOfWork.MovementTypeRepository.CreateAsync(mappedEntity);
                    await unitOfWork.SaveChangesAsync();

                    TempData["success"] = "Запись успешно добавлена";
                    return RedirectToAction(nameof(Index));
                }

                return View(entityVM);
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrderType(OrderTypeVM entityVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var mappedEntity = mapper.Map<OrderTypeVM, OrderType>(entityVM);
                    await unitOfWork.OrderTypeRepository.CreateAsync(mappedEntity);
                    await unitOfWork.SaveChangesAsync();

                    TempData["success"] = "Запись успешно добавлена";
                    return RedirectToAction(nameof(Index));
                }

                return View(entityVM);
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> EditAcademicYear(int id)
        {
            var entity = await unitOfWork.AcademicYearRepository.GetByIdAsync(id);
            if (entity == null)
            {
                TempData["error"] = "Не удалось найти текущую запись";
                return RedirectToAction(nameof(Index));
            }
            var mappedEntity = mapper.Map<AcademicYear, AcademicYearVM>(entity);
            return View(mappedEntity);
        }

        public async Task<IActionResult> EditMovementType(int id)
        {
            var entity = await unitOfWork.MovementTypeRepository.GetByIdAsync(id);
            if (entity == null)
            {
                TempData["error"] = "Не удалось найти текущую запись";
                return RedirectToAction(nameof(Index));
            }
            var mappedEntity = mapper.Map<MovementType, MovementTypeVM>(entity);
            return View(mappedEntity);
        }

        public async Task<IActionResult> EditOrderType(int id)
        {
            var entity = await unitOfWork.OrderTypeRepository.GetByIdAsync(id);
            if (entity == null)
            {
                TempData["error"] = "Не удалось найти текущую запись";
                return RedirectToAction(nameof(Index));
            }
            var mappedEntity = mapper.Map<OrderType, OrderTypeVM>(entity);
            return View(mappedEntity);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAcademicYear(int id, AcademicYearVM entityVM)
        {
            var currentEntity = unitOfWork.AcademicYearRepository.GetById(id);
            if (currentEntity == null)
            {
                TempData["error"] = "Не удалось найти текущую запись";
                return View(entityVM);
            }
            try
            {
                if (ModelState.IsValid)
                {
                    currentEntity.YearFrom = entityVM.YearFrom ?? 0;
                    currentEntity.YearTo = entityVM.YearTo ?? 0;
                    currentEntity.Name = entityVM.YearFrom.ToString() + "-" + entityVM.YearTo.ToString();

                    var isYearRangeExistInDB = unitOfWork.AcademicYearRepository.GetAll()
                        .Any(x => x.YearFrom == currentEntity.YearFrom && x.Id != currentEntity.Id);

                    if (isYearRangeExistInDB)
                    {
                        TempData["error"] = "Текущий учебный год уже существует";
                        return View(entityVM);
                    }

                    await unitOfWork.SaveChangesAsync();

                    TempData["success"] = "Запись успешно изменена";
                    return RedirectToAction(nameof(Index));
                }
                return View(entityVM);
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMovementType(int id, MovementTypeVM entityVM)
        {
            if (id != entityVM.Id)
            {
                TempData["error"] = "Не удалось найти текущую запись";
                return View(entityVM);
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var mappedEntity = mapper.Map<MovementTypeVM, MovementType>(entityVM);
                    await unitOfWork.MovementTypeRepository.UpdateAsync(mappedEntity);
                    await unitOfWork.SaveChangesAsync();

                    TempData["success"] = "Запись успешно изменена";
                    return RedirectToAction(nameof(Index));
                }
                return View(entityVM);
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction(nameof(Index));
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditOrderType(int id, OrderTypeVM entityVM)
        {
            if (id != entityVM.Id)
            {
                TempData["error"] = "Не удалось найти текущую запись";
                return View(entityVM);
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var mappedEntity = mapper.Map<OrderTypeVM, OrderType>(entityVM);
                    await unitOfWork.OrderTypeRepository.UpdateAsync(mappedEntity);
                    await unitOfWork.SaveChangesAsync();

                    TempData["success"] = "Запись успешно изменена";
                    return RedirectToAction(nameof(Index));
                }
                return View(entityVM);
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> DeleteAcademicYear(int id)
        {
            var entity = await unitOfWork.AcademicYearRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        public async Task<IActionResult> DeleteMovementType(int id)
        {
            var entity = await unitOfWork.MovementTypeRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        public async Task<IActionResult> DeleteOrderType(int id)
        {
            var entity = await unitOfWork.OrderTypeRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        public async Task<IActionResult> DeleteAcademicYearConfirmed(int id)
        {
            var entity = await context.AcademicYears
                .Include(o => o.Groups)
                .Include(s => s.StudentMovements)
                .Include(s => s.StudentPayments)
                .Include(o => o.Orders)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (entity == null)
            {
                TempData["error"] = "Не удалось найти учебный год. Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Option");
            }

            if (entity.Groups != null || entity.StudentMovements != null || entity.StudentPayments != null || entity.Orders != null)
            {
                await unitOfWork.GroupRepository.DeleteRangeAsync(entity.Groups);
                await unitOfWork.StudentMovementRepository.DeleteRangeAsync(entity.StudentMovements);
                await unitOfWork.StudentPaymentRepository.DeleteRangeAsync(entity.StudentPayments);
                await unitOfWork.OrderRepository.DeleteRangeAsync(entity.Orders);
            }
            await unitOfWork.AcademicYearRepository.DeleteAsync(entity);
            await unitOfWork.SaveChangesAsync();

            TempData["success"] = "Запись успешно удалена";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteMovementTypeConfirmed(int id)
        {
            var entity = await context.MovementTypes
                .Include(o => o.OrderTypes)
                .Include(s => s.StudentMovements)
                .Include(o => o.Orders)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (entity == null)
            {
                TempData["error"] = "Не удалось найти тип перемещения. Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Option");
            }

            if (entity.OrderTypes != null || entity.StudentMovements != null || entity.Orders != null)
            {
                await unitOfWork.OrderTypeRepository.DeleteRangeAsync(entity.OrderTypes);
                await unitOfWork.StudentMovementRepository.DeleteRangeAsync(entity.StudentMovements);
                await unitOfWork.OrderRepository.DeleteRangeAsync(entity.Orders);
            }
            await unitOfWork.MovementTypeRepository.DeleteAsync(entity);
            await unitOfWork.SaveChangesAsync();

            TempData["success"] = "Запись успешно удалена";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteOrderTypeConfirmed(int id)
        {
            var entity = await unitOfWork.OrderTypeRepository.GetByIdAsync(id);
            await unitOfWork.OrderTypeRepository.DeleteAsync(entity);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<List<SelectListItem>> GetAcademicYears()
        {
            var entityList = (await unitOfWork.AcademicYearRepository.GetAllAsync()).OrderByDescending(x => x.YearTo);
            return entityList.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name }).ToList();
        }

        public async Task<List<SelectListItem>> GetMovementTypes()
        {
            var entityList = await unitOfWork.MovementTypeRepository.GetAllAsync();
            return entityList.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name }).ToList();
        }

        public async Task<List<SelectListItem>> GetOrderTypes()
        {
            var entityList = await unitOfWork.OrderTypeRepository.GetAllAsync();
            return entityList.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name }).ToList();
        }

        public async Task<List<SelectListItem>> GetOrderTypeByMovement(int movementTypeId)
        {
            if (movementTypeId == 0)
                return new List<SelectListItem>();
            var entityList = (await unitOfWork.OrderTypeRepository.GetAllAsync()).Where(x => x.MovementTypeId == movementTypeId);
            return entityList.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name }).ToList();
        }
    }
}
