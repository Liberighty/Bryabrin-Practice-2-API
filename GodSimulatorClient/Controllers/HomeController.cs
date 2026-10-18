using GodSimulatorClient.Data;
using GodSimulatorClient.Models;
using GodSimulatorClient.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Diagnostics;
using System.Text.Json;
using WorldUpdate = GodSimulatorClient.Data.Update;

namespace GodSimulatorClient.Controllers
{
    public class HomeController : Controller
    {
        #region логгер и сервисы.
        private readonly ILogger<HomeController> _logger;

        private readonly UpdatesClientService _updatesClientService;
        public HomeController(ILogger<HomeController> logger, UpdatesClientService updatesClientService)
        {
            _logger = logger;
            _updatesClientService = updatesClientService;
        }
        #endregion

        #region Index. Первая страница, привествие и экспозиция.

        public IActionResult Index()                             //Приветствие//
        {
            return View();
        }

        #endregion

        #region Action

        [HttpGet]                                                     //основная страница обновлений//
        public async Task<IActionResult> Action()
        {
            var updateData = await _updatesClientService.GetUpdatesAll();

            return View(updateData);
        }
        #endregion

        #region Поиск, добавь сервис
        [HttpGet]                                                           //поиск//
        public async Task<IActionResult> Search(string version, string description)
        {

            var responseData = await _updatesClientService.SearchUpdates(version, description);
            
            return View("Action", responseData);
        }
        #endregion      


        #region ADD.
        [HttpPost]
        public async Task<IActionResult> Add(UpdateClientAdd request)
        {
            await _updatesClientService.AddUpdates(request);
            return RedirectToAction(nameof(Action));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {

            var divisions = await _updatesClientService.GetDivisionsAll();
            return View(divisions);
        }
        #endregion


        #region  REMOVE.
        [HttpGet]                                                              //Удаление записей//
        public async Task<IActionResult> Remove(int Id)
        {
            
            await _updatesClientService.RemoveUpdates(Id);

            return RedirectToAction(nameof(Action));
        }
        #endregion

        #region EDIT, добавь сервис
        //редактирование//
        [HttpGet]                                                 //открытие окна редактируемого обновления//
        public async Task<IActionResult> Edit(int Id)
        {
            var responseData = await _updatesClientService.GetEditUpdates(Id);

            return View(responseData);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateClientEdit request)
        {

            await _updatesClientService.PostEditUpdates(request);
            
            return RedirectToAction(nameof(Action));
        }

        #endregion

        #region Очень (!!!) важные мнения

        [HttpGet]
        public IActionResult InternetInsanity()
        {
            var post = MessagesData.GetRandom();
            return Json(new { post });
        }
        #endregion

        #region Privacy и Error.
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}
