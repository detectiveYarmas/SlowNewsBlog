using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SlowNewsBlog.Domain.Factories;
using SlowNewsBlog.Domain.Managers;
using SlowNewsBlog.Models;
using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SlowNewsBlog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;

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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public ActionResult Bloggers()
        {
            var context = new ApplicationDbContext();
            //This is our role
            var blogger = (from r in context.Roles where r.Name.Contains("Blogger") select r).FirstOrDefault();
            //This is the list of users in the role
            var bloggers = context.Users.Where(u => u.Roles.Select(r => r.RoleId).Contains(blogger.Id));

            var bloggerVM = bloggers.Select(user => new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Role = "Blogger"
            }).ToList();

            var admin = (from r in context.Roles where r.Name.Contains("Admin") select r).FirstOrDefault();
            var admins = context.Users.Where(u => u.Roles.Select(r => r.RoleId).Contains(admin.Id));

            var adminVM = admins.Select(user => new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Role = "Admin"
            }).ToList();

            var model = new GroupedViewModel { Bloggers = bloggerVM, Admins = adminVM };

            return View(model);
        }

        [HttpGet]
        public ActionResult AddBlogger()
        {
            var context = new ApplicationDbContext();
            var roles = context.Roles;
            var model = new RegisterViewModel();

            model.Roles = roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            });

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddBlogger(RegisterViewModel model)
        {
            var context = new ApplicationDbContext();

            if (ModelState.IsValid)
            {

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                    var role = roleManager.FindById(model.Role);
                    UserManager.AddToRole(user.Id, role.Name);
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    context.SaveChanges();

                    return RedirectToAction("Bloggers", "Admin");
                }
                AddErrors(result);
            }

            var roles = context.Roles;
            model.Roles = roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            });

            return View(model);
        }

        [HttpGet]
        public ActionResult EditBlogger(string id)
        {
            var context = new ApplicationDbContext();
            var roles = context.Roles;
            var editedUser = UserManager.FindById(id);
            var model = new RegisterViewModel();


            model.Roles = roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id
            });

            model.Id = editedUser.Id;
            model.Email = editedUser.Email;


            foreach (var role in editedUser.Roles)
            {
                model.Role = role.RoleId;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult EditBlogger(RegisterViewModel model)
        {
            var context = new ApplicationDbContext();
            var roles = context.Roles;
            var user = UserManager.FindById(model.Id);


            if (!string.IsNullOrEmpty(model.EditedPassword))
            {
                user.PasswordHash = UserManager.PasswordHasher.HashPassword(model.EditedPassword);
            }

            user.Email = model.Email;

            var oldRole = user.Roles.SingleOrDefault().RoleId;

            if (!user.Roles.Any(r => r.RoleId == model.Role))
            {

                var dbuser = context.Users.SingleOrDefault(u => u.Id == model.Id);
                dbuser.Roles.Clear();

                context.SaveChanges();

                var role = roles.Where(r => r.Id == model.Role).Select(r => r.Name).SingleOrDefault();

                UserManager.RemoveFromRole(user.Id, oldRole);
                UserManager.AddToRole(user.Id, role);
            }

            UserManager.Update(user);

            return RedirectToAction("Bloggers", "Admin");
        }

        [HttpGet]
        public ActionResult HashTags()
        {
            var hashMgr = HashTagManagerFactory.Create();
            var hashTags = hashMgr.GetAllHashTags();

            var model = new HashTagListViewModel();
            model.HashTags = hashTags.HashTags;

            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteHashTag(int hashTagId)
        {
            var hashMgr = HashTagManagerFactory.Create();
            var hashtag = hashMgr.GetHashTag(hashTagId);

            hashMgr.RemoveHashTag(hashtag.HashTag.HashTagId);

            return RedirectToAction("HashTags");

        }

        [HttpGet]
        public ActionResult EditHashTag(int hashTagId)
        {
            var hashMgr = HashTagManagerFactory.Create();
            var toEdit = hashMgr.GetHashTag(hashTagId);

            var model = new EditHashTagViewModel();

            model.HashTag = toEdit.HashTag;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditHashTag(EditHashTagViewModel model)
        {
            var hashMgr = HashTagManagerFactory.Create();
            HashTag hash = new HashTag();

            if (ModelState.IsValid)
            {
                hash.HashTagName = model.HashTag.HashTagName;
                hash.Approved = model.HashTag.Approved;
                hashMgr.EditHashTag(hash);

                return RedirectToAction("HashTags");
            }
            return View(model);
        }

    }
}
