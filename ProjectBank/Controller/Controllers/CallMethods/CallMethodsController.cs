﻿using Microsoft.AspNetCore.Mvc;
using ProjectBank.Controller.Services;
using ProjectBank.Controller.Services.MathodsServise;
using ProjectBank.Data;
using ProjectBank.Entities;

namespace ProjectBank.Controller.Controllers.CallMethods
{
    [Route("api/MakeTransactions")]
    [ApiController]
    public class CallMethodsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMethodsSevice methodsService;

        public CallMethodsController(DataContext context, IMethodsSevice methodsSevice)
        {
            _context = context;
            this.methodsService = methodsSevice;
        }
        [HttpGet("MakeTransaction/cardID={cardID}/sum={sum}")]
        public async Task<ActionResult<Account>> MakeTransaction(Guid cardID, double sum)
        {
            await methodsService.MakeTransaction(cardID, sum);
            return Ok();
        }

        [HttpPut("makeTransaction/senderCardID={senderCardID}/receiverCardID={receiverCardID}/sum={sum}")]
        public async Task<ActionResult<Account>> MakeTransaction(Guid senderCardID, Guid receiverCardID, double sum)
        {
            await methodsService.MakeTransaction(senderCardID, receiverCardID, sum);
            return Ok();
        }
    }
}