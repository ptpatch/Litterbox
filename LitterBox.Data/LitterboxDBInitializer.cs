﻿using Litterbox.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LitterBox.Data
{
    public class LitterboxDBInitializer : CreateDatabaseIfNotExists<LitterboxContext>
    {
        protected override void Seed(LitterboxContext context)
        {
            SeedRoles(context);
            SeedUsers(context);

            SeedCategories(context);
            SeedConfigurations(context);
        }

        public void SeedRoles(LitterboxContext context)
        {
            List<IdentityRole> rolesInDealDouble = new List<IdentityRole>();

            rolesInDealDouble.Add(new IdentityRole() { Name = "Administrator" });
            rolesInDealDouble.Add(new IdentityRole() { Name = "Moderator" });
            rolesInDealDouble.Add(new IdentityRole() { Name = "User" });

            var rolesStore = new RoleStore<IdentityRole>(context);
            var rolesManager = new RoleManager<IdentityRole>(rolesStore);

            foreach (IdentityRole role in rolesInDealDouble)
            {
                if (!rolesManager.RoleExists(role.Name))
                {
                    var result = rolesManager.Create(role);

                    if (result.Succeeded) continue;
                }
            }
        }

        public void SeedUsers(LitterboxContext context)
        {
            var usersStore = new UserStore<LitterboxUser>(context);
            var usersManager = new UserManager<LitterboxUser>(usersStore);

            LitterboxUser admin = new LitterboxUser();
            admin.FullName = "Admin";
            admin.Email = "admin@gmail.com";
            admin.UserName = "admin";
            admin.PhoneNumber = "";
            admin.Country = "";
            admin.City = "";
            admin.Address = "";
            admin.ZipCode = "";
            var password = "1111";

            if (usersManager.FindByEmail(admin.Email) == null)
            {
                var result = usersManager.Create(admin, password);

                if (result.Succeeded)
                {
                    //add necessary roles to admin
                    usersManager.AddToRole(admin.Id, "Administrator");
                    usersManager.AddToRole(admin.Id, "Moderator");
                    usersManager.AddToRole(admin.Id, "User");
                }
            }
        }

        public void SeedCategories(LitterboxContext context)
        {
            Category uncategorized = new Category()
            {
                Name = "Uncategorized",
                SanitizedName = "uncategorized",
                Description = "Products that are not categorized. uncategorised, unclassified - not arranged in any specific grouping.",
                DisplaySeqNo = 0,
                ModifiedOn = DateTime.Now
            };

            context.Categories.Add(uncategorized);

            context.SaveChanges();
        }

        public void SeedConfigurations(LitterboxContext context)
        {
            Configuration slider1Config = new Configuration()
            {
                Key = "Slider1",
                Value = "#TH#Welcome to#MH#LitterBox",
                ConfigurationType = (int)ConfigurationType.HomeSliders,
                ModifiedOn = DateTime.Now
            };

            Configuration dashboardRecordsSizePerPageConfig = new Configuration()
            {
                Key = "DashboardRecordsSizePerPage",
                Value = "10",
                ConfigurationType = (int)ConfigurationType.Other,
                ModifiedOn = DateTime.Now
            };

            Configuration frontendRecordsSizePerPageConfig = new Configuration()
            {
                Key = "FrontendRecordsSizePerPage",
                Value = "6",
                ConfigurationType = (int)ConfigurationType.Other,
                ModifiedOn = DateTime.Now
            };

            Configuration featuredRecordsSizePerPageConfig = new Configuration()
            {
                Key = "FeaturedRecordsSizePerPage",
                Value = "8",
                ConfigurationType = (int)ConfigurationType.Other,
                ModifiedOn = DateTime.Now
            };

            Configuration currencySymbolConfig = new Configuration()
            {
                Key = "CurrencySymbol",
                Value = "$",
                Description = "This currency symbol is shown with prices on website and invoices.",
                ConfigurationType = (int)ConfigurationType.Other,
                ModifiedOn = DateTime.Now
            };

            Configuration priceCurrencyPositionConfig = new Configuration()
            {
                Key = "PriceCurrencyPosition",
                Value = "{price}{currency}",
                Description = "This configuration will set price and currency relation accross the website. {price} will be replaced with the price value and {currency} will be replaced by configured currency symbol.",
                ConfigurationType = (int)ConfigurationType.Other,
                ModifiedOn = DateTime.Now
            };

            Configuration enableCreditCardPayment = new Configuration()
            {
                Key = "EnableCreditCardPayment",
                Value = "true",
                Description = "Set value to true to enable Credit card payments or set value to false to disable credit card payments.",
                ConfigurationType = (int)ConfigurationType.Other,
                ModifiedOn = DateTime.Now
            };

            Configuration enableCashOnDeliveryMethod = new Configuration()
            {
                Key = "EnableCashOnDeliveryMethod",
                Value = "true",
                Description = "Set value to true to enable Cash on Delivery Method or set value to false to disable Cash on Delivery Method.",
                ConfigurationType = (int)ConfigurationType.Other,
                ModifiedOn = DateTime.Now
            };

            Configuration flatDeliveryChargesConfig = new Configuration()
            {
                Key = "FlatDeliveryCharges",
                Value = "0",
                Description = "Set the value for Delivery Charges. This is flat delivery charges rate.",
                ConfigurationType = (int)ConfigurationType.Other,
                ModifiedOn = DateTime.Now
            };

            context.Configurations.AddRange(new List<Configuration> { slider1Config, dashboardRecordsSizePerPageConfig, frontendRecordsSizePerPageConfig, featuredRecordsSizePerPageConfig, currencySymbolConfig, priceCurrencyPositionConfig, enableCreditCardPayment, enableCashOnDeliveryMethod, flatDeliveryChargesConfig });

            context.SaveChanges();
        }
    }
}