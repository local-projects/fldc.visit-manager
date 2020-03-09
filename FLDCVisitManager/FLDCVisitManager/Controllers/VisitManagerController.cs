using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FLDCVisitManagerBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FLDCVisitManager.Controllers
{
    [ApiController]
    [Route("")]
    public class VisitManagerController : ControllerBase
    {

        private readonly ILogger<VisitManagerController> _logger;

        public VisitManagerController(ILogger<VisitManagerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public void GetCollectionPoints()
        {

        }

        [Route("hello")]
        public void GetCollectionPointTrigger(CPRequestParams req)
        {

        }
    }
}
