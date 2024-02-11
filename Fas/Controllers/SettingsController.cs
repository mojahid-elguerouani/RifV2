using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FasDemo.Data;
using FasDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FasDemo.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        //dependency injection through constructor, to directly access services
        public SettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}