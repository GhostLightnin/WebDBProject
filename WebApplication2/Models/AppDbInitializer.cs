using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
namespace WebApplication2.Models
{
    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };
            var role3 = new IdentityRole { Name = "accountant" };
            var role4 = new IdentityRole { Name = "manager" };
            var role5 = new IdentityRole { Name = "consultant" };
            var role6 = new IdentityRole { Name = "storage_worker" };

            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);
            roleManager.Create(role4);
            roleManager.Create(role5);
            roleManager.Create(role6);



            // создаем пользователей
            var admin = new ApplicationUser { Email = "somemail@mail.ru", UserName = "somemail@mail.ru" };
            string password = "ad46D_ewr3";
            var result = userManager.Create(admin, password);
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);

            }

            var consultant = new ApplicationUser { Email = "somemail_2@mail.ru", UserName = "somemail_2@mail.ru" };
            password = "ad46D_ewr3";
            result = userManager.Create(consultant, password);
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                
                userManager.AddToRole(consultant.Id, role5.Name);

            }

            var manager = new ApplicationUser { Email = "somemail_3@mail.ru", UserName = "somemail_3@mail.ru" };
            password = "ad46D_ewr3";
            result = userManager.Create(manager, password);
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(manager.Id, role4.Name);

            }

            var accountant = new ApplicationUser { Email = "somemail_4@mail.ru", UserName = "somemail_4@mail.ru" };
            password = "ad46D_ewr3";
            result = userManager.Create(accountant, password);
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(accountant.Id, role3.Name);

            }

            var storage_worker = new ApplicationUser { Email = "somemail_5@mail.ru", UserName = "somemail_5@mail.ru" };
            password = "ad46D_ewr3";
            result = userManager.Create(storage_worker, password);
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(storage_worker.Id, role6.Name);

            }
            

            admin = new ApplicationUser { Email = "somemail_1@mail.ru", UserName = "somemail_1@mail.ru" };
            password = "Fefefejijiji333@";
            result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
            }

            base.Seed(context);
        }
    }
}