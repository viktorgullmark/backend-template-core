using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BaseBackend.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BaseApi.Models;
using BaseBackend.Entities;
using BaseBackend.Services;
using Microsoft.AspNetCore.Authorization;

namespace BaseApi.Controllers
{
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        private BaseContext _dbContext;

        private readonly TestService _testService;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, BaseContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;

            // instantiate services
            _testService = new TestService(_dbContext);
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("Error", "Passwords don't match");
                return BadRequest(ModelState);
            }

            var newUser = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var userCreationResult = await _userManager.CreateAsync(newUser, model.Password);
            if (!userCreationResult.Succeeded)
            {
                foreach (var error in userCreationResult.Errors)
                    ModelState.AddModelError("Error", error.Description);
                return BadRequest(ModelState);
            }

            return Ok();
        }

        // test-endpoint
        [Route("Test")]
        [HttpGet]
        public async Task<Test> Test()
        {
            var test = await _testService.GetTestById(1);
            return test;
        }
    }
}
