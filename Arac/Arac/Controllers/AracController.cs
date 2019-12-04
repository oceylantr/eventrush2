using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Arac.Services;
using Arac.EventSourcing;

namespace Arac.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AracController : ControllerBase
    {
        private readonly AracService _aracService;
        // private readonly ICommandBus _commandBus;
        private readonly IEventBus _eventBus;
        private readonly SubscriptionManager _subscriptionManager;

        public AracController(AracService aracService, SubscriptionManager subscriptionManager, /*ICommandBus commandBus,*/ IEventBus eventBus)
        {
            _aracService = aracService;
            // _commandBus = commandBus;
            _eventBus = eventBus;
            _subscriptionManager = subscriptionManager;
        }

        // [HttpGet]
        // public ActionResult<List<Arac>> Get() =>
        //     _aracService.Get();

        [HttpGet]
        public ActionResult<List<Arac>> Get(){
            _subscriptionManager.subscribeServicesFor(_eventBus, "Arac");
            return _aracService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetArac")]
        public ActionResult<Arac> Get(string id)
        {
            // if (id.Equals("subsciption_start")){
            //     _subscriptionManager.subscribeServicesFor(_eventBus, "Arac");
            // return NoContent();
            // }

            var Arac = _aracService.Get(id);

            if (Arac == null)
            {
                return NotFound();
            }

            return Arac;
        }

        // HttpGet("/startsubscriptions", Name = "startsubscriptions")
        // public ActionResult<Arac> StartSubscriptions()
        // {
        //     _subscriptionManager.subscribeServicesFor(_eventBus, "Arac");
        //     return NoContent();
        // }


    }
}
