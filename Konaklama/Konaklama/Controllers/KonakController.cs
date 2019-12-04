using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Konak.Services;
using Konak.EventSourcing;

namespace Konak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KonakController : ControllerBase
    {
        private readonly KonakService _konakService;
        // private readonly ICommandBus _commandBus;
        private readonly IEventBus _eventBus;
        private readonly SubscriptionManager _subscriptionManager;

        public KonakController(KonakService konakService, SubscriptionManager subscriptionManager, /*ICommandBus commandBus,*/ IEventBus eventBus)
        {
            _konakService = konakService;
            // _commandBus = commandBus;
            _eventBus = eventBus;
            _subscriptionManager = subscriptionManager;
        }

        // [HttpGet]
        // public ActionResult<List<Konak>> Get() =>
        //     _konakService.Get();

        [HttpGet]
        public ActionResult<List<Konak>> Get(){
            _subscriptionManager.subscribeServicesFor(_eventBus, "Konak");
            return _konakService.Get();
        }

        [HttpGet("{id:length(24)}", Name = "GetKonak")]
        public ActionResult<Konak> Get(string id)
        {
            // if (id.Equals("subsciption_start")){
            //     _subscriptionManager.subscribeServicesFor(_eventBus, "Konak");
            // return NoContent();
            // }

            var konak = _konakService.Get(id);

            if (konak == null)
            {
                return NotFound();
            }

            return konak;
        }

        // HttpGet("/startsubscriptions", Name = "startsubscriptions")
        // public ActionResult<Konak> StartSubscriptions()
        // {
        //     _subscriptionManager.subscribeServicesFor(_eventBus, "Konak");
        //     return NoContent();
        // }


    }
}
