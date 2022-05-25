using Business.Concrete;
using Medicine_Chest.EmailServices;
using Medicine_Chest.Identity;
using Medicine_Chest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/{controller}/{action}")]
    public class UserController : ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IEmailSender _emailSender;
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userManager.Users);
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            var result = await _userManager.CreateAsync(user, "HHSHHSHH");

            if (result.Succeeded)
            {

                //generate token
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id,
                    token = token
                });

                //await _emailSender.SendEmailAsync(model.Email, "Hesabınızı onaylayınız", $"Lütfen email hesabınızı onaylamak için linke <a href='https://localhost:44303{url}'> tıklayınız.</a>");
                return Ok(user);
            }
        
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> login(LoginModel model)
        {
            
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError("UserName", "Bu kullanıcı adı ile daha önce hesap oluşturulmamış");
                return NotFound(model);
            }
         
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
            {
                return Ok(model);
            }
            return NotFound(model);
        }

        [HttpPut]
        public async Task<IActionResult> Update(User user)
        {
            await _userManager.UpdateAsync(user);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {

                await _userManager.DeleteAsync(user);


                return Ok();
            }
            return NotFound();
        }
    }
}


