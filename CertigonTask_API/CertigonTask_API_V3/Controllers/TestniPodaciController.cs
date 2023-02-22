using System;
using System.Collections.Generic;
using System.Linq;
using CertigonTask_API_V3.Data;
using CertigonTask_API_V3.Entities;
using CertigonTask_API_V3.Helpers;
using CertigonTask_API_V3.Helpers.AuthenticationAuthorization;
using CertigonTask_API_V3.Models.Accounts;
using CertigonTask_API_V3.Models.Items;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIT_Api_Examples.Modul1_TestniPodaci.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestniPodaciController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public TestniPodaciController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public ActionResult Count()
        {
            Dictionary<string, int> data = new Dictionary<string, int>();
            data.Add("KorisnickiNalog", _dbContext.KorisnickiNalog.Count());
            data.Add("Items", _dbContext.Item.Count());

            return Ok(data);
        }

        [HttpPost]
        public ActionResult Generate()
        {
            var items = new List<Item>();
            var users = new List<KorisnickiNalog>();

            items.Add(new Item { Name = "Logitech G900", Description = "Najbolja mehanicka tastatura", Category = "Tastature", Price = 432.35m });
            items.Add(new Item { Name = "Logitech G733", Description = "Bezicki gaming headset", Category = "Slusalice", Price = 253.20m });
            items.Add(new Item { Name = "Logitech G402", Description = "Gaming wired mis", Category = "Misevi", Price = 150.264m });
            items.Add(new Item { Name = "Logitech MX Master 3S", Description = "Office wired mis", Category = "Misevi", Price = 190.235m });
            items.Add(new Item { Name = "Logitech G PRO X superlight", Description = "Gaming wireless mis", Category = "Misevi", Price = 150.25m });
            items.Add(new Item { Name = "Logitech G305 Lightspeed", Description = "Gaming wireless mis", Category = "Misevi", Price = 150.75m });
            items.Add(new Item { Name = "Logitech G715", Description = "Gaming wireless tastatura", Category = "Tastature", Price = 305.75m });
            items.Add(new Item { Name = "Logitech MX Keys", Description = "Office wireless tastatura", Category = "Tastature", Price = 175.75m });
            items.Add(new Item { Name = "Logitech Signature K650", Description = "Office wireless tastatura", Category = "Tastature", Price = 125.35m });
            items.Add(new Item { Name = "Razer Huntsman V2 Analog", Description = "Gaming wired tastatura", Category = "Tastature", Price = 345.35m });
            items.Add(new Item { Name = "Razer Pro Type Ultra", Description = "Gaming wireless tastatura", Category = "Tastature", Price = 545.75m });
            items.Add(new Item { Name = "SteelSeries Acrtis 7", Description = "Gaming wireless slusalice", Category = "Slusalice", Price = 399.99m });
            items.Add(new Item { Name = "SteelSeries Acrtis Nova 3", Description = "Gaming wired slusalice", Category = "Slusalice", Price = 219.99m });
            items.Add(new Item { Name = "SteelSeries Acrtis 9 Wireless", Description = "Gaming wireless slusalice", Category = "Slusalice", Price = 439.99m });
            items.Add(new Item { Name = "Alienware AW3423DW", Description = "Gaming monitor 34,QD OLED", Category = "Monitori", Price = 1765.29m });
            items.Add(new Item { Name = "LG 27GN950-B", Description = "Gaming monitor 27,Nano IPS", Category = "Monitori", Price = 1294.23m });
            items.Add(new Item { Name = "Dell S2722DGM", Description = "Gaming monitor 27,VA", Category = "Monitori", Price = 765.45m });



            users.Add(new KorisnickiNalog { korisnickoIme = "harun", email = "harun1@gmail.com", PasswordHash = "Harun123.".HashPassword() });
            users.Add(new KorisnickiNalog { korisnickoIme = "harunAdmin", email = "harun2@gmail.com", PasswordHash = "HarunAdmin123.".HashPassword(), isAdmin = true });
            users.Add(new KorisnickiNalog { korisnickoIme = "harunManager", email = "harun3@gmail.com", PasswordHash = "HarunAdmin123.".HashPassword(), isManager = true });


            _dbContext.AddRange(items);
            _dbContext.AddRange(users);

            _dbContext.SaveChanges();

            return Count();
            //return Ok();
        }
    }
}