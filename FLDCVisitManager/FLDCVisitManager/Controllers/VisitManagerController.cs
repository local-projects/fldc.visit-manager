using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FLDCVisitManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
    }
}
