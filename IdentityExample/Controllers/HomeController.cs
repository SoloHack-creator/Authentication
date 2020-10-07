﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityExample.Controllers
{
    public class HomeController:Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;


        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public  async Task <IActionResult> Login(string username,string password)
        {
            var result = await _userManager.FindByNameAsync(username);
            if(result !=null)
            {
               var signInResult= await _signInManager.PasswordSignInAsync(result, password, false,false) ;
                if (signInResult.Succeeded)
                { 
                    return RedirectToAction("Index"); }
                else
                {
                    return BadRequest();
                }
            }
           
            
            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Register(string username, string password)
        {
            var user = new IdentityUser()
            {
                UserName = username,
                Email = "test.com"

            };
           var result=  await _userManager.CreateAsync(user);

            if(result.Succeeded)
            {

            }
            
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

    }
}
