using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Odeme.Services;
using Odeme.EventSourcing;

namespace Odeme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OdemeController : ControllerBase
    {
        private readonly OdemeService _odemeService;
        // private readonly ICommandBus _commandBus;
        private readonly IEventBus _eventBus;
        private readonly SubscriptionManager _subscriptionManager;

        public OdemeController(OdemeService odemeService, SubscriptionManager subscriptionManager, /*ICommandBus commandBus,*/ IEventBus eventBus)
        {
            _odemeService = odemeService;
            // _commandBus = commandBus;
            _eventBus = eventBus;
            _subscriptionManager = subscriptionManager;
        }

        // [HttpGet]
        // public ActionResult<List<Odeme>> Get() =>
        //     _odemeService.Get();

        [HttpGet]
        public ActionResult<List<Odeme>> Get(){
            _subscriptionManager.subscribeServicesFor(_eventBus, "Odeme");
            return _odemeService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetOdeme")]
        public ActionResult<Odeme> Get(string id)
        {
            // if (id.Equals("subsciption_start")){
            //     _subscriptionManager.subscribeServicesFor(_eventBus, "Odeme");
            // return NoContent();
            // }

            var Odeme = _odemeService.Get(id);

            if (Odeme == null)
            {
                return NotFound();
            }

            return Odeme;
        }

        // HttpGet("/startsubscriptions", Name = "startsubscriptions")
        // public ActionResult<Odeme> StartSubscriptions()
        // {
        //     _subscriptionManager.subscribeServicesFor(_eventBus, "Odeme");
        //     return NoContent();
        // }


    }
}
