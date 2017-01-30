using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Security;
using EQueueVidly.Core;
using EQueueVidly.Domain;
using EQueueVidly.Models;
using EQueueVidly.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace EQueueVidly.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext context = new ApplicationDbContext();

        private UnitOfWork unitOfWork = new UnitOfWork(new ApplicationDbContext());

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        //
        // GET: /Admin/
        [Authorize]
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            
            var user = context.Users.Find(User.Identity.GetUserId());   
            var model = new IndexViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.PhoneNumber,
                Email = user.Email,
                HasPassword = HasPassword(),
            };
            return View(model);
        }

        public async Task<ActionResult> Student()
        {
            var userId = User.Identity.GetUserId();
            var user = context.Users.First(u => u.Id == userId);
            var model = new IndexViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.PhoneNumber,
                HasPassword = HasPassword(),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                /*BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)*/
            };
            return PartialView("AcoountDetails", model);
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        [HttpGet]
        public async Task<ActionResult> EditAccount()
        {
            var userId = User.Identity.GetUserId();
            var user = context.Users.First(u => u.Id == userId);
            IndexViewModel model = new IndexViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.PhoneNumber
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditAccount(IndexViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                user.FirstName = viewModel.FirstName;
                user.LastName = viewModel.LastName;
                user.PhoneNumber = viewModel.Phone;
                var result = UserManager.UpdateAsync(user);
            }
            return RedirectToAction("Teacher", new { Message = ManageMessageId.EditAccountSuccess });
        }

        public ActionResult EditPhoto()
        {
            return View();
        }

        #region Upload Photo

        [HttpPost]
        public async Task<ActionResult> UploadPhoto(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var user = await GetCurrentUserAsync();
                var username = user.UserName;
                var fileExt = Path.GetExtension(file.FileName);
                var fnm = username + ".png";
                if (fileExt.ToLower().EndsWith(".png") || fileExt.ToLower().EndsWith(".jpg") || fileExt.ToLower().EndsWith(".gif"))// Important for security if saving in webroot
                {
                    var db = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                    byte[] image = new byte[file.ContentLength];
                    file.InputStream.Read(image, 0, Convert.ToInt32(file.ContentLength));
                    user.ProfilePicture = image;
                    db.SaveChanges();

                    return RedirectToAction("Index", new { Message = AccountController.ManageMessageId.PhotoUploadSuccess });
                }
                else
                {
                    return RedirectToAction("EditPhoto", new { Message = AccountController.ManageMessageId.FileExtensionError });
                }
            }
            return RedirectToAction("EditPhoto", new { Message = AccountController.ManageMessageId.Error });// PRG
        }

        private async Task<ApplicationUser> GetCurrentUserAsync()
        {
            return await UserManager.FindByIdAsync(User.Identity.GetUserId());
        }

        #endregion Upload Photo
    }

    public enum ManageMessageId
    {
        AddPhoneSuccess,
        EditAccountSuccess,
        ChangePasswordSuccess,
        SetTwoFactorSuccess,
        SetPasswordSuccess,
        RemoveLoginSuccess,
        RemovePhoneSuccess,
        Error
    }
}