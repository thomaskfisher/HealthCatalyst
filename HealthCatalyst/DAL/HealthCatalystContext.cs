using HealthCatalyst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HealthCatalyst.DAL
{
    public class HealthCatalystContext : DbContext
    {
        public HealthCatalystContext()
            : base("HealthCatalystContext")
        {

        }

        public DbSet<People> Peoples { get; set; }
    }
}