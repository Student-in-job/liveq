using System.Data.Entity.Validation;
using System.Text;
using System.Web;
using EQueueVidly.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace EQueueVidly.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EQueueVidly.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "EQueueVidly.Models.ApplicationDbContext";
        }

        protected override void Seed(EQueueVidly.Models.ApplicationDbContext context)
        {

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var appRoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            /*var EveryDay = context.IrerationTypes.First(i => i.Span == 1);
            
           context.IrerationTypes.Add(new IrerationType(){Name = "Не повторять",Span = 0});
           context.IrerationTypes.Add(new IrerationType(){Name = "Каждый день",Span = 1});
           context.IrerationTypes.Add(new IrerationType() { Name = "Каждые 2 недели", Span = 14 });
           context.IrerationTypes.Add(new IrerationType() { Name = "Каждую неделю", Span = 7 });*/

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                
                var role = new IdentityRole { Name = "Admin" };
                appRoleManager.Create(role);
            }
            if (!context.Roles.Any(r => r.Name == "Teacher"))
            {
                var role = new IdentityRole { Name = "Teacher" };
                appRoleManager.Create(role);
            }
            if (!context.Roles.Any(r => r.Name == "Student"))
            {
                var role = new IdentityRole { Name = "Student" };
                appRoleManager.Create(role);
            }

            context.SaveChanges();

            if (!context.Users.Any(t => t.UserName == "admin@equeue.com"))
            {
                var user = new ApplicationUser()
                {
                    UserName = "admin@equeue.com",
                    Email = "admin@equeue.com",
                    PhoneNumber = "9959995959"
                };

                
                    userManager.Create(user, "QweRTy!@#098");
                    userManager.AddToRole(user.Id, "Admin");
                    // Your code...
                    // Could also be before try if you know the exception occurs in SaveChanges
                context.SaveChanges();
//                SaveChanges(context);
                
                
            }
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }

        /// <summary>
        /// Wrapper for SaveChanges adding the Validation Messages to the generated exception
        /// </summary>
        /// <param name="context">The context.</param>
        private void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
        }
    }

}
