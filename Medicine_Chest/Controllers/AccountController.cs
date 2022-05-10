﻿using Medicine_Chest.EmailServices;
using Medicine_Chest.Helpers;
using Medicine_Chest.Identity;
using Medicine_Chest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Uno.Expressions;

namespace Medicine_Chest.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IEmailSender _emailSender;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }
        public IActionResult login(string ReturnUrl)
        {

            return View(new LoginModel()
            {
                ReturnUrl = ReturnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError("UserName", "Bu kullanıcı adı ile daha önce hesap oluşturulmamış");
                return View(model);
            }
            if(!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Lütfen email hesabınıza gelen link ile üyeliğinizi aktifleştiriniz.");
                return View(model);

            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl ?? "~/Home/Index");
            }
            return View(model);
        }


        /// <summary>
        /// Sayfa ilk yüklendiğinde filtre ile ilgili elemanların hazırlanması için geçerli olan bir durumdur
        /// </summary>
        /// <param name="model">Model nesnesi</param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult FiltreleKullanici(KullaniciViewModel model)
        {
            try
            {

                var kullanicilar = _userManager.Users;



                if (!string.IsNullOrEmpty(model.NameSorgu))
                {
                    kullanicilar=kullanicilar.Where(user => user.Name == model.NameSorgu);
                }
                if (!string.IsNullOrEmpty(model.SurnameSorgu))
                {
                    kullanicilar = kullanicilar.Where(user => user.Surname == model.SurnameSorgu);
                }
                if (!string.IsNullOrEmpty(model.IdentificationNoSorgu))
                {
                    kullanicilar = kullanicilar.Where(user => user.IdentificationNo == model.IdentificationNoSorgu);
                }
                if (!string.IsNullOrEmpty(model.UserNameSorgu))
                {
                    kullanicilar = kullanicilar.Where(user => user.UserName == model.UserNameSorgu);
                }

                model.UserList = kullanicilar;
                return View("KullaniciIslemleri", model);
            }
            catch (Exception exception)
            {
                return null; /*Mesaj.Goster(ArayuzSabitler.SorgulamaBaslik, "Sorgulama İşlemi Sırasında Bir Hata Oluştu: " + exception.GetAllMessages(), MesajTip.Hata);*/
            }
        }

     
        public IActionResult KullaniciIslemleri()
        {
            //// AJAX metodlarında IIS adresi bize lazım olacağından
            //// burada ilk değer atamasında bulunuyoruz
            //Session["IISAdresi"] = System.Configuration.ConfigurationManager.AppSettings["IISAdresi"];
           
            var model = new KullaniciViewModel();
            model.UserList = _userManager.Users;
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> KullaniciIslemi(string Id, string islemTuru)
        {
            var dict = new Dictionary<string, string>();
           
            var mesaj = "";
            try
            {
                //LogMesaj = SessionManagement.AktifKullanici.Id + " id'li kullanıcı" + kodRafineriTuruId + " idli Kod Arıza Türü ile ilgili bilgileri getirdi";
                //// işlem türü de burada seçiliyor
                //LogIslemTuruId = SessionManagement.LogIslemTuruListesi.First(j => j.Kod == LogIslemTur.DetayGoruntuleme.GetHashCode()).Id;
                //LogIslem.EkleLog(LogAnaTurId, LogAltTurId, LogIslemTuruId, LogMesaj, dict, Request, ControllerContext);
                if (islemTuru != IslemTurSabitler.IslemTuruKayitEkleme || !string.IsNullOrEmpty(Id))
                {
                    var user = await _userManager.FindByIdAsync(Id);
                    if (user != null)
                    {

                        return PartialView("_KullaniciIslemi", new KullaniciIslemViewModel()
                        {
                            Id = user.Id,
                            UserName = user.UserName,
                            Name = user.Name,
                            Surname = user.Surname,
                            Email = user.Email,
                            //EmailConfirmed = user.EmailConfirmed,
                            PhoneNumber = user.PhoneNumber,
                            //IsDelete = user.IsDelete,
                            //SelectedRoles = selectedRoles
                            IslemTuru = islemTuru,

                        }) ;
                    }
                    return PartialView("_KullaniciIslemi", new KullaniciIslemViewModel());
                }
                else
                {

                    return PartialView("_KullaniciIslemi", new KullaniciIslemViewModel()
                    {
                        IslemTuru = islemTuru,
                    });
            } }
            catch (Exception exception)
            {
            

                ModelState.Clear();
                ModelState.AddModelError("", "esnasında bir hata oluştu.");
                Response.StatusCode = 400;
                return PartialView("_KullaniciIslemi", new KullaniciIslemViewModel());
                //return Json(new JsonSonuc { HataMi = true, Mesaj = mesaj.Mesaj }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken] ///form güvenliği için sadece post methodları için olur gelen formun gönderilen formla aynı mi ?
        public async Task<IActionResult> KullaniciIslemi(KullaniciIslemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_KullaniciIslemi", model);
            }
            if (model.IslemTuru != IslemTurSabitler.IslemTuruKayitEkleme)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Name = model.Name;
                    user.Surname = model.Surname;
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    //user.EmailConfirmed = model.EmailConfirmed;
                    user.PhoneNumber = model.PhoneNumber;
                    //user.IsDelete = model.IsDelete;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        //var userRoles = await _userManager.GetRolesAsync(user);
                        //selectedRoles = selectedRoles ?? new string[] { };
                        //await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToArray<string>());
                        //await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles).ToArray<string>());
                     
                    }
                }
            }
            else
            {

                var user = new User
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    IdentificationNo = model.IdentificationNo,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    UserName = model.UserName
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {

                    //generate token
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var url = Url.Action("ConfirmEmail", "Account", new
                    {
                        userId = user.Id,
                        token = token
                    });

                    await _emailSender.SendEmailAsync(model.Email, "Hesabınızı onaylayınız", $"Lütfen email hesabınızı onaylamak için linke <a href='https://localhost:44303{url}'> tıklayınız.</a>");
                    return RedirectToAction("KullaniciIslemleri", "Account");
                }
            }
            return View("KullaniciIslemleri", model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult KullaniciListesi()
        {
            return PartialView("_KullaniciListesi", _userManager.Users);

        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (String.IsNullOrEmpty(userId) || String.IsNullOrEmpty(token))
            {
                //error tempdata 
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                //error ver tempdata ile 
                return View();
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                //tempdata 
                return View();

            }
            return View();
        }
    }
        }
