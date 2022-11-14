﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using eTicketShop.Core;


namespace eTicketShop.Controllers
{
    public class RoleController : Controller
    {
            public IActionResult Index()
            {
                return View();
            }

            [Authorize(Policy = Constants.Policies.RequireAdmin)]
            public IActionResult Manager()
            {
                return View();
            }

            //[Authorize(Policy = "RequireAdmin")]
            [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.Manager}")]
            public IActionResult Admin()
            {
                return View();
            }
        }
}