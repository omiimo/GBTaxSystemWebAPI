using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GBTaxSystemWebAPI.Models;
using GBTaxSystemWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GBTaxSystemWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController : ControllerBase
    {
        IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }
        // POST api/<PurchaseController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PurchaseDataModel purchaseData)
        {
            return Ok(await _purchaseService.CalculatePurchaseInfo(purchaseData));
        }
    }
}
