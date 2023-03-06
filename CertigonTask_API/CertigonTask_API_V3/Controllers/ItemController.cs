using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CertigonTask_API_V3.Data;
using CertigonTask_API_V3.Entities;
using CertigonTask_API_V3.Helpers;
using CertigonTask_API_V3.Helpers.AuthenticationAuthorization;
using CertigonTask_API_V3.Models.Accounts;
using CertigonTask_API_V3.Models.Items;
using CertigonTask_API_V3.Services.ItemService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CertigonTask_API_V3.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            this._itemService = itemService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> Get(int id)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return Forbid();

            /*return Ok(_dbContext.Item.FirstOrDefault(i => i.Id == id)); ;*/
            var result = await _itemService.GetSingleItem(id);
            if(result is null)
                return NotFound("Item is not found");
            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<List<Item>>> Update(int id, [FromBody] ItemUpdateVM request)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("Not logged in!");

           var result = await _itemService.UpdateItem(id, request);
            if (result is null)
                return NotFound("Item not found!");

            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!HttpContext.GetLoginInfo().isPermissionManager)
                return BadRequest("You are not manager!");

            var result = await _itemService.DeleteItem(id);
            if (result is null)
                return NotFound("Item not found!");

            return Ok(id);
        }

        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetAll()
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("You are not logged in!");

            return await _itemService.GetAllItems();
        }


    }
}
