using GodSimulatorClient.Data;
using GodSimulatorClient.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GodSimulatorClient.Controllers
{
    public class DivisionController : Controller
    {
        #region логгер и сервисы.
        private readonly ILogger<DivisionController> _logger;
        private readonly UpdatesClientService _updatesClientService;

        public DivisionController(ILogger<DivisionController> logger, UpdatesClientService updatesClientService)
        {
            _logger = logger;
            _updatesClientService = updatesClientService;
        }
        #endregion

        #region Action
        [HttpGet]
        public async Task<IActionResult> Action()
        {
            var updateData = await _updatesClientService.GetDivisionsAll();
            return View(updateData);
        }
        #endregion

        #region ADD Division
        [HttpPost]
        public async Task<IActionResult> AddDivisions(Division request)
        {
            await _updatesClientService.AddDivisions(request);
            return RedirectToAction(nameof(Action));
        }
        #endregion

        #region REMOVE
        [HttpGet]
        public async Task<IActionResult> RemoveDivision(int Id)
        {
            await _updatesClientService.RemoveDivision(Id);
            return RedirectToAction(nameof(Action));
        }
        #endregion

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}