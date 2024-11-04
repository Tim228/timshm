using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebPlan.Domain.Entity;
using WebPlan.Domain.ViewModels;
using WebPlan.Service.Implementations;
using WebPlan.Service.Interfaces;

namespace WebPlan.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataRecordService _dataRecordService;

        public HomeController(IDataRecordService dataRecordService)
        {
            _dataRecordService = dataRecordService;
        }

        public IActionResult Index() => View();

        [HttpPost]
        [Route("Home/CheckDate")]
        [Route("CheckDate")]
        public IActionResult CheckDate(DateViewModel dateViewModel)
        {
            DataRecordService.emailSelect = HttpContext?.User?.Identity?.Name;
            DataRecordService.dateSelect = dateViewModel.Calendar;
            if (ModelState.IsValid)
            {
                return RedirectToAction("Record");
            }
            return View("Index");
        }

        public async Task<IActionResult> Record()
        {
            var records = _dataRecordService.RecordService();
           
            return View(records);
        }

        [HttpPost]
        public async Task<IActionResult> Check(DataRecord dataRecord)
        {
            await _dataRecordService.Create(dataRecord);

            if (User.IsInRole("admin"))
                return RedirectToAction("List");
            return RedirectToAction("Profile");
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> List() //Вывод всех клиентов по записи (доступно только админам)
        {
            var response = _dataRecordService.ListService();
            return View(await response);
        }

        [Authorize(Roles = "admin, user")]
        [Route("Profile")]
        public IActionResult Profile()
        {
            IQueryable<DataRecord> recordIQuer = _dataRecordService.Profile();

            var records = recordIQuer.Where(p => p.Email == HttpContext.User.Identity.Name);

            return View(records);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {           
            await _dataRecordService.DeleteService(id);
            if(User.IsInRole("admin"))
                return RedirectToAction("List");
            return RedirectToAction("Profile");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id!=null)
            {
                DataRecord dataRecord = await _dataRecordService.EditService(id);
                if (dataRecord!=null)
                    return View(dataRecord);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DataRecord dataRecord)
        {
            await _dataRecordService.EditSaveService(dataRecord);
            return RedirectToAction("List");
        }
    }
}
