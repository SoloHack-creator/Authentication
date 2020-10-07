using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Basics.Controllers
{
    public class HomeController:Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Authenticate()
        {
            var loginClaim = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"bob"),
                new Claim(ClaimTypes.Email,"bpb@gmail.com"),
                new Claim("test","copy that")


            };

            var loginLicenseClaim = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"bob"),
                new Claim(ClaimTypes.Email,"bpb@gmail.com"),
                new Claim("license","copy that")


            };

            var loginIdentity = new ClaimsIdentity(loginClaim,"login");
            var licenseIdentity= new ClaimsIdentity(loginLicenseClaim, "license Login");

            var principalUser = new ClaimsPrincipal( new[] { loginIdentity, licenseIdentity });

            HttpContext.SignInAsync(principalUser);

            return RedirectToAction("Index");
        }


    }
}
