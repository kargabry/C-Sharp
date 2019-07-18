namespace Shop.Migrations
{
    using Shop.DAL;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Shop.DAL.CoursesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Shop.DAL.CoursesContext";
        }

        protected override void Seed(Shop.DAL.CoursesContext context)
        {
            CourseInitializer.SeedCoursesData(context);

        }
    }
}
