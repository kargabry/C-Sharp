using Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Web;

namespace Shop.DAL
{
    public class CoursesContext : DbContext
    {
        public CoursesContext() : base("CoursesContext")
        {

        }
        static CoursesContext()
        {
            Database.SetInitializer<CoursesContext>(new CourseInitializer());
        }

        public static CoursesContext Create()
        {
            return new CoursesContext();
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderPosition> OrderPosition { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // using System.Data.Entity.ModelConfiguration.Conventions;
            // Wyłącza konwencję, która automatycznie tworzy liczbę mnogą dla nazw tabel w bazie danych
            // Zamiast Category zostałaby stworzona tabela o nazwie Categorys
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); 
        }
    }
}