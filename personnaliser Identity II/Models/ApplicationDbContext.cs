﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Indentity.Models
{
    public class ApplicationDbContext:IdentityDbContext<CustomUser>
    {
        public ApplicationDbContext() : base()
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }


    public class CustomUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public async Task<ClaimsIdentity> GenerateIdentiyAsync(UserManager <CustomUser> manager)
        {
            //Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType

            var userIdentity = await manager.CreateIdentityAsync(this, 
            DefaultAuthenticationTypes.ApplicationCookie)
            ;

            return userIdentity;

        } 

    }


    public static class SecurityRoles
    {
        public const string Admin = "admin";
        public const string IT = "IT";
        public const string Accounting  = "accounting";

    }

}