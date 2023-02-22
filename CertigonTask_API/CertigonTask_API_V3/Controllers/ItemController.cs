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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CertigonTask_API_V3.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class ItemController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ItemController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            /*if (!HttpContext.GetLoginInfo().isLogiran)
                return Forbid();*/

            return Ok(_dbContext.Item.FirstOrDefault(i => i.ID == id)); ;
        }

        [HttpPost("{id}")]
        public ActionResult Update(int id, [FromBody] ItemUpdateVM x)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("Not logged in!");

            Item item;

            if (id == 0)
            {
                item = new Item
                {
                    created_time = DateTime.Now
                };
                _dbContext.Add(item);
            }
            else
            {
                item = _dbContext.Item.FirstOrDefault(i => i.ID == id);
                if (item == null)
                    return BadRequest("Unknown ID");
            }

            item.Name = x.name.RemoveTags();
            item.Description = x.description.RemoveTags();
            item.Price = x.price;
            item.Category = x.category;
            

            _dbContext.SaveChanges();
            return Get(item.ID);
        }

        [HttpPost("{id}")]
        public ActionResult Delete(int id)
        {
            if (!HttpContext.GetLoginInfo().isPermisijaManager)
                return BadRequest("You are not manager!");

            Item item = _dbContext.Item.Find(id);

            if (item == null || id == 1)
                return BadRequest("Incorrect ID");

            _dbContext.Remove(item);
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public ActionResult GetAll(string category)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("You are not logged in!");

            var data = _dbContext.Item
                .Where(x => category == null || x.Category.StartsWith(category))
                .OrderByDescending(i => i.ID)
                .AsQueryable();


            return Ok( data.Take(100).ToList());
        }


        [HttpGet]
        public ActionResult GetAll2()
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("You are not logged in!");

            var data = _dbContext.Item.ToList().AsQueryable();
            return Ok(data.Take(100).ToList());
        }


    }
}
