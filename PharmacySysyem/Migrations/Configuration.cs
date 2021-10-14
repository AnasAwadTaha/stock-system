namespace PharmacySysyem.Migrations
{

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<PharmacySysyem.DAL.PharmacyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "PharmacySysyem.DAL.PharmacyContext";
        }

        protected override void Seed(PharmacySysyem.DAL.PharmacyContext context)
        {
            var Otherexpenses = new List<expence>
            {
                new expence { ExpenceName = "Anas" },
                 new expence { ExpenceName = "Monty" },
                  new expence { ExpenceName = "Salih" },
                   new expence { ExpenceName = "Salary" }

            };
            Otherexpenses.ForEach(s => context.expences.AddOrUpdate(e => e.ExpenceName, s));
            context.SaveChanges();

            var Units = new List<Unit>
            {
                new Unit { UnitName = "ÚáÈÉ" },
                 new Unit { UnitName = "ÔÑíØ" }

            };
            Units.ForEach(s => context.Units.AddOrUpdate(u => u.UnitName, s));
            context.SaveChanges();

            var Classifications = new List<Classification>
            {
                new Classification { ClassificationName = "Medicine" },
                 new Classification { ClassificationName = "Medical Tools" },
                  new Classification { ClassificationName = "Cosmetics" },
                   new Classification { ClassificationName = "Accessories" }


            };
            Classifications.ForEach(s => context.Classifications.AddOrUpdate(c => c.ClassificationName, s));
            context.SaveChanges();
        }
    }
}
