using HWCodeAssgn.Models;
using System;
using System.Linq;

namespace HWCodeAssgn.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SiteContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Profile.Any())
            {
                return;   // DB has been seeded
            }

            //else: fill DB with test data
            var profiles = new Profile[]
            {
                new Profile{
                    Name ="Gary", 
                    Email ="gtech235@gmail.com", 
                    BirthDate = DateTime.Parse("1993-02-10"),
                    Password = "GarySsPasskey93",
                    City = "Brooklyn",
                    State = "New York",
                    Zip = 11235
                },

                new Profile{
                    Name ="Sabrina", 
                    Email ="S.Ramos@housingworks.com", 
                    BirthDate = DateTime.Parse("2018-01-08"),
                    Password = "SabrinaRsPasskey18",
                    City = "Brooklyn",
                    State = "New York",
                    Zip = 11201
                },

                new Profile{
                    Name ="Chancal", 
                    Email ="C.Gandhwani@housingworks.com", 
                    BirthDate = DateTime.Parse("2019-01-08"),
                    Password = "ChancalGsPasskey19",
                    City = "Brooklyn",
                    State = "New York",
                    Zip = 11201
                }
            

            };
            foreach (Profile p in profiles)
            {
                context.Profile.Add(p);
            }
            context.SaveChanges();

        
        }
    }
}

