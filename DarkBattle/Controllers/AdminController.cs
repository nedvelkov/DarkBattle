using DarkBattle.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkBattle.Controllers
{
    public class AdminController:Controller
    {
        public IActionResult AddCreature()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCreature(CreateCreatureViewModel model)
        {

            //TODO : Add creatures to Db
            ;
            return Redirect("/");
        }
        public IActionResult AddAreas()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAreas(CreateCreatureViewModel model)
        {

            //TODO : Add creatures to Db
            ;
            return Redirect("/");
        }
    }
}
