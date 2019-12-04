using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ucus.Services;
using Ucus.EventSourcing;

namespace Ucus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UcusController : ControllerBase
    {
        private readonly UcusService _ucusService;
        private readonly ICommandBus _commandBus;
        private readonly IEventBus _eventBus;

        public UcusController(UcusService ucusService, ICommandBus commandBus, IEventBus eventBus)
        {
            _ucusService = ucusService;
            _commandBus = commandBus;
            _eventBus = eventBus;
        }

        [HttpGet]
        public ActionResult<List<Ucus>> Get() =>
            _ucusService.Get();

        [HttpGet("{id:length(24)}", Name = "GetUcus")]
        public ActionResult<Ucus> Get(string id)
        {
            var ucus = _ucusService.Get(id);

            if (ucus == null)
            {
                return NotFound();
            }

            return ucus;
        }

        [HttpPost]
        public ActionResult<Ucus> Create(Ucus ucus)
        {
            //FIXME _ucusService.Create(ucus);
            _commandBus.Process(new UcusAyarlaCommand(ucus, _ucusService, _eventBus));

            return NoContent();
            //FIXME return CreatedAtRoute("GetUcus", new { id = ucus.Id.ToString() }, ucus);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Ucus ucusIn)
        {
            var ucus = _ucusService.Get(id);

            if (ucus == null)
            {
                return NotFound();
            }

            _ucusService.Update(id, ucusIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var ucus = _ucusService.Get(id);

            if (ucus == null)
            {
                return NotFound();
            }

            _ucusService.Remove(ucus.Id);

            return NoContent();
        }


    }
}
