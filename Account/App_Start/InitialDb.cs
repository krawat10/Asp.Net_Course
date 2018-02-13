using Account.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Account.App_Start
{
    /// <summary>
    /// Initalize user database each time when app ic debbuged
    /// </summary>
    public class IdentityDbInitalizer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        public override void InitializeDatabase(ApplicationDbContext context)
        {
            // Manager for users. This class works with context designed for user (table with passwords etc).
            // Works with ApplicationUser objects. It is an abstraction to create new users, adding new roles etc.
            var UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            // Managers for users roles. This class works with db context designed for IdentityRole obejcts.
            // It is an abstraction to create new roles, checking roles etc.
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //Create our additional field
            var myUserInfo = new MyUserInfo { FirstName = "Jan", LastName = "Kowalski" };

            //Create new role name
            string roleName = "Admin";

            //password for new user
            string password = "p@ssw0rd";

            //Create new user (add to db) through ApplicationUserManager
            UserManager.Create(new ApplicationUser() { UserName = "Kazio", UserInfo = myUserInfo }); 

            //If 'Admin' role doesn't exist in Role Manager entity
            if (!RoleManager.RoleExists(roleName))
            {
                //Add new role in role db
                var roleResult = RoleManager.Create(new IdentityRole(roleName));
            }

            //Create another user
            var user = new ApplicationUser();
            user.UserName = "Baretk";
            user.Email = "krawat10@gmail.com";
            user.UserInfo = myUserInfo;

            //Add user to user's collection through ApplicationUserManager
            var createResult = UserManager.Create(user, password);

            //If user is created return specific result
            if(createResult.Succeeded)
            {
                var result = UserManager.AddToRole(user.Id, roleName);
            }


            base.InitializeDatabase(context);
        }
    }
}