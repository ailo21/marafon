using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;

namespace Prypo.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {        
        [StringLength(256)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [StringLength(256)]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [StringLength(256)]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }
        [Display(Name = "Дата рождения")]
        public DateTime BirthDate { get; set; }
        [StringLength(8)]
        public string Gender { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            //userIdentity.AddClaim(new Claim(ClaimTypes.DateOfBirth, this.BirthDate.ToString()));
            //userIdentity.AddClaim(new Claim(ClaimTypes.Gender, this.Gender));
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
        //public DateTime BirthDate { get; set; }
        //public string Gender { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}