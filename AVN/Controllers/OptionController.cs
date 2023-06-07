using AVN.Automapper;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AVN.Controllers
{
    public class OptionController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public OptionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> AcademicYearList()
        {
            var list = await unitOfWork.AcademicYearRepository.GetAllAsync();
            var mappedList = mapper.Map<AcademicYear, AcademicYearVM>(list).ToList();
            return PartialView(mappedList);
        }

        public async Task<ActionResult> MovementTypeList()
        {
            var list = await unitOfWork.MovementTypeRepository.GetAllAsync();
            var mappedList = mapper.Map<MovementType, MovementTypeVM>(list).ToList();
            return PartialView(mappedList);
        }

        public async Task<ActionResult> OrderTypeList()
        {
            var list = await unitOfWork.OrderTypeRepository.GetAllAsync();
            var mappedList = mapper.Map<OrderType, OrderTypeVM>(list).ToList();
            return PartialView(mappedList);
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
            if (ModelState.IsValid)
            {
                var mappedEntity = mapper.Map<AcademicYearVM, AcademicYear>(entityVM);
                mappedEntity.Name = mappedEntity.YearFrom.ToString() + "-" + mappedEntity.YearTo.ToString();
                await unitOfWork.AcademicYearRepository.CreateAsync(mappedEntity);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(entityVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMovementType(MovementTypeVM entityVM)
        {
            if (ModelState.IsValid)
            {
                var mappedEntity = mapper.Map<MovementTypeVM, MovementType>(entityVM);
                await unitOfWork.MovementTypeRepository.CreateAsync(mappedEntity);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(entityVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrderType(OrderTypeVM entityVM)
        {
            if (ModelState.IsValid)
            {
                var mappedEntity = mapper.Map<OrderTypeVM, OrderType>(entityVM);
                await unitOfWork.OrderTypeRepository.CreateAsync(mappedEntity);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(entityVM);
        }

        public async Task<IActionResult> EditAcademicYear(int id)
        {
            var entity = await unitOfWork.AcademicYearRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            var mappedEntity = mapper.Map<AcademicYear, AcademicYearVM>(entity);
            return View(mappedEntity);
        }

        public async Task<IActionResult> EditMovementType(int id)
        {
            var entity = await unitOfWork.MovementTypeRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            var mappedEntity = mapper.Map<MovementType, MovementTypeVM>(entity);
            return View(mappedEntity);
        }

        public async Task<IActionResult> EditOrderType(int id)
        {
            var entity = await unitOfWork.OrderTypeRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            var mappedEntity = mapper.Map<OrderType, OrderTypeVM>(entity);
            return View(mappedEntity);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAcademicYear(int id, AcademicYearVM entityVM)
        {
            if (id != entityVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var mappedEntity = mapper.Map<AcademicYearVM, AcademicYear>(entityVM);
                mappedEntity.Name = mappedEntity.YearFrom.ToString() + "-" + mappedEntity.YearTo.ToString();
                await unitOfWork.AcademicYearRepository.UpdateAsync(mappedEntity);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(entityVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMovementType(int id, MovementTypeVM entityVM)
        {
            if (id != entityVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var mappedEntity = mapper.Map<MovementTypeVM, MovementType>(entityVM);
                await unitOfWork.MovementTypeRepository.UpdateAsync(mappedEntity);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(entityVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditOrderType(int id, OrderTypeVM entityVM)
        {
            if (id != entityVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var mappedEntity = mapper.Map<OrderTypeVM, OrderType>(entityVM);
                await unitOfWork.OrderTypeRepository.UpdateAsync(mappedEntity);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(entityVM);
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
            var entity = await unitOfWork.AcademicYearRepository.GetByIdAsync(id);
            await unitOfWork.AcademicYearRepository.DeleteAsync(entity);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteMovementTypeConfirmed(int id)
        {
            var entity = await unitOfWork.MovementTypeRepository.GetByIdAsync(id);
            await unitOfWork.MovementTypeRepository.DeleteAsync(entity);
            await unitOfWork.SaveChangesAsync();
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
            var entityList = (await unitOfWork.AcademicYearRepository.GetAllAsync()).OrderByDescending(x => x.Id);
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
    }
}
