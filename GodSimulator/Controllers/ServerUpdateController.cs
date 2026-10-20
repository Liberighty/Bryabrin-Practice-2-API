using GodSimulator.Data;
using GodSimulator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldUpdate = GodSimulator.Data.Update;

namespace GodSimulator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServerUpdateController : ControllerBase
    {
        private readonly ILogger<ServerUpdateController> _logger;
        private readonly MyApplicationContext _context;

        public ServerUpdateController(ILogger<ServerUpdateController> logger, MyApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetWorldUpdate")]
        public List<WorldUpdate> Get()
        {
            _logger.LogInformation("Получаю список обновлений...");
            return _context.Updates.Include(x => x.Division).OrderByDescending(x => x.Id).ToList();
        }

        [HttpGet("search")]
        public List<WorldUpdate> Get([FromQuery] string? descriptionFinder, [FromQuery] string? versionFinder)
        {
            return _context.Updates
                .Where(x => (descriptionFinder == null || x.Description.Contains(descriptionFinder)))
                .Where(x => (versionFinder == null || x.Version.Contains(versionFinder)))
                .Include(x => x.Division)
                .OrderByDescending(x => x.Id)
                .ToList();
        }

        [HttpPut(Name = "PutWorldUpdate")]
        public WorldUpdate Put(UpdatesAdd requestUpdate)
        {
            _logger.LogInformation("Добавить обновление...");

            var newUpdate = new WorldUpdate
            {
                Description = requestUpdate.Description,
                Version = requestUpdate.Version,
                DivisionId = requestUpdate.DivisionId
            };
            _context.Updates.Add(newUpdate);
            _context.SaveChanges();

            return newUpdate;
        }

        [HttpDelete(Name = "DeleteWorldUpdate")]
        public IActionResult Delete(int updateId)
        {
            _logger.LogInformation("Удалить обновление...");

            var deleteUpdate = _context.Updates.FirstOrDefault(x => x.Id == updateId);
            if (deleteUpdate != null)
            {
                _context.Remove(deleteUpdate);
                _context.SaveChanges();

                return Ok();
            }

            return NotFound();
        }

        [HttpPatch(Name = "PatchWorldUpdate")]
        public IActionResult Patch(UpdateEdit redactedUpdate)
        {
            _logger.LogInformation("Редактирование данных в базе...");

            var proceedRedaction = _context.Updates.FirstOrDefault(x => x.Id == redactedUpdate.Id);
            if (proceedRedaction != null)
            {
                proceedRedaction.Version = redactedUpdate.Version;
                proceedRedaction.Description = redactedUpdate.Description;

                _context.Update(proceedRedaction);
                _context.SaveChanges();

                return Ok();
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult<WorldUpdate> Get(int id)
        {
            var reqId = _context.Updates.FirstOrDefault(x => x.Id == id);
            if (reqId == null)
                return NotFound();
            return reqId;
        }
    }
}