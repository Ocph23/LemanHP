using LemanWeb.Models;
using LemanWeb.DataModels;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;

namespace LemanWeb.Api
{
    public class AccountController : ApiController
    {
        // GET: api/Account
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Account/5
        [AllowAnonymous]
        
        [HttpGet]
        public async Task<HttpResponseMessage> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,"Error");
            }

            var c = HttpUtility.UrlDecode(code);
            var UserManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var result = await UserManager.ConfirmEmailAsync(userId,c);
            return Request.CreateResponse(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        // POST: api/Account
        public async Task<HttpResponseMessage> Register(PelangganRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var UserManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var AppRoleManager= Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
                var result = await UserManager.CreateAsync(user, model.Password);
                if(result.Succeeded)
                {

                    var roles = "Customer";
                    if (!await AppRoleManager.RoleExistsAsync(roles))
                    {
                        await AppRoleManager.CreateAsync(new AspNet.Identity.MySQL.IdentityRole { Name = roles });
                    }
                    var role = await UserManager.AddToRoleAsync(user.Id, roles);


                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    string c = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    string code = HttpUtility.UrlEncode(c);
                    var callbackUrl = Url.Link("DefaultApi", new { controller = "Account/ConfirmEmail", userId = user.Id, code = code })   ;
                    await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    using (var db = new OcphDbContext())
                    {
                        db.Pelanggans.Insert(new Pelanggan { Alamat=model.Alamat, Email=model.Email, Nama=model.Nama, TanggalDaftar=DateTime.Now, Telepon=model.Telepon, UserId=user.Id });
                    }
                }
                return Request.CreateResponse(result);
            }else
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error");
            }

            // If we got this far, something failed, redisplay form
        }

        // PUT: api/Account/5
        public void Put(int id, [FromBody]string value)
        {
            
        }

        // DELETE: api/Account/5
        public void Delete(int id)
        {
        }
    }
}
