using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Shop.Models;
using System.Data.Entity.Migrations;
using Shop.Migrations;

namespace Shop.DAL
{
    public class CourseInitializer : MigrateDatabaseToLatestVersion<CoursesContext, Configuration>
    {
        public static void SeedCoursesData(CoursesContext context)
        {
            var category = new List<Category>
            {
                new Category() { CategoryId=1, CategoryName="asp", IconFileName="asp.png", CategoryDescription="opis aps"},
                new Category() { CategoryId=2, CategoryName="java", IconFileName="java.png", CategoryDescription="opis java"},
                new Category() { CategoryId=3, CategoryName="php", IconFileName="php.png", CategoryDescription="opis php"},
            };

            category.ForEach(k => context.Categories.AddOrUpdate(k));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course() {Authors="Tomek", Title="asp.net mvc", CategoryId=1, Price=99, Bestseller=true, PictureFileName="asp.png", AddDate=DateTime.Now, Description="opis kursu"},
                new Course() {Authors="Jacek", Title="asp.net mvc2", CategoryId=2, Price=120, Bestseller=true, PictureFileName="asp2.png", AddDate=DateTime.Now, Description="opis kursu2"},
                new Course() {Authors="Kazik", Title="asp.net mvc3", CategoryId=3, Price=150, Bestseller=true, PictureFileName="asp3.png", AddDate=DateTime.Now, Description="opis kursu3"},
            };

            courses.ForEach(k => context.Courses.AddOrUpdate(k));
            context.SaveChanges();
        }
    }
}