using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OurSports1.Data;
using OurSports1.Services;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OurSports1.Models
{
    public class DbInitializer
    {
       

        public static void Initialize(ApplicationDbContext context, IServiceProvider services)
        {

            context.Database.EnsureCreated();



            if (context.Author.Any() && context.Category.Any() && context.Article.Any())
            {
                return;
            }
            var Categories = new Category[]
                       {
            new Category{Title="Israeli Soccer"},
              new Category{Title="World Soccer"},
                new Category{Title="Basketball"},
                  new Category{Title="Nba"},
          new Category{Title="Tennis"}
                       };

            foreach (Category c in Categories)
            {
                context.Category.Add(c);
            }
            context.SaveChanges();

            var Authors = new Author[]
            {
                  new Author{AuthorName="Idan Lazar",BirthDate=Convert.ToDateTime("10/05/2000"),Image="A.jpg"},
                   new Author{AuthorName="Idan Frenkel",BirthDate=Convert.ToDateTime("10/06/2000"),Image="A.jpg"},
                  new Author{AuthorName="Ronel Good",BirthDate=Convert.ToDateTime("10/07/2000"),Image="A.jpg"},
                  new Author{AuthorName="Yuval David",BirthDate=Convert.ToDateTime("07/10/2000"),Image="A.jpg"}

        };
            foreach (Author c in Authors)
            {
                context.Author.Add(c);
            }
            context.SaveChanges();

            int firstidA = context.Author.First().ID;
            int firstidC = context.Category.First().ID;
            var Articles = new Article[]
            {
                  new Article{ Title="Beitar vs Hapoel" , Secondary_title="The most amazing game that i ever saw in my life \n woowww!",Content="if you ever saw this game its a fantatic game wow!", AuthorID=firstidA, CategoryID=firstidC , TimeCreate= DateTime.Now, Image="ביתרירושלים.png"},
                  new Article{ Title="Beitar vs Macabi" , Secondary_title="The most amazing game that i ever saw in my life \n woowww!",Content="if you ever saw this game its a fantatic game wow!", AuthorID=firstidA+1, CategoryID=firstidC+2 , TimeCreate= DateTime.Now, Image="ביתרירושלים.png"},
                  new Article{ Title="Arab vs Jeus" , Secondary_title="The most amazing game that i ever saw in my life \n woowww!",Content="if you ever saw this game its a fantatic game wow!", AuthorID=firstidA+2, CategoryID=firstidC+3 , TimeCreate= DateTime.Now, Image="ביתרירושלים.png"},
                  new Article{ Title="Bamilton" , Secondary_title="The most amazing game that i ever saw in my life \n woowww!",Content="if you ever saw this game its a fantatic game wow!", AuthorID=firstidA, CategoryID=firstidC , TimeCreate= new DateTime(2000,06,15), Image="ביתרירושלים.png"},
                  new Article{ Title="Hapoel" , Secondary_title="The most amazing game that i ever saw in my life \n woowww!",Content="if you ever saw this game its a fantatic game wow!", AuthorID=firstidA+1, CategoryID=firstidC+1 , TimeCreate= DateTime.Now, Image="ביתרירושלים.png"},
        };
            foreach (Article c in Articles)
            {
                context.Article.Add(c);
            }
            context.SaveChanges();
            int firstidAr = context.Article.First().ID;
            var Comments = new Comment[]
            {
                  new Comment{
                      Title ="LOVE it!" ,
                      Content ="Good Article!",
                      WriterName = "King Of the World",
                      ArticleID =firstidAr },
                   new Comment{
                      Title ="LOVE it!" ,
                      Content ="Good Article!",
                      WriterName = "King Of the World",
                      ArticleID =firstidAr+1 },
                    new Comment{
                      Title ="LOVE it!" ,
                      Content ="Good Article!",
                      WriterName = "King Of the World",
                      ArticleID =firstidAr+2 },
                     new Comment{
                      Title ="LOVE it!" ,
                      Content ="Good Article!",
                      WriterName = "King Of the World",
                      ArticleID =firstidAr+3 },
                      new Comment{
                      Title ="LOVE it!" ,
                      Content ="Good Article!",
                      WriterName = "King Of the World",
                      ArticleID =firstidAr+4 },
                       new Comment{
                      Title ="LOVE it!" ,
                      Content ="Good Article!",
                      WriterName = "King Of the World",
                      ArticleID =firstidAr },
                        new Comment{
                      Title ="I hate this very much" ,
                      Content ="Bad i hate it",
                      WriterName = "King Of the World",
                      ArticleID =firstidAr },
                   new Comment{
                      Title ="I hate this very much" ,
                      Content ="Bad i hate it",
                      WriterName = "King Of the World",
                      ArticleID =firstidAr+1 },
                    new Comment{
                      Title ="I hate this very much" ,
                      Content ="Bad i hate it",
                      WriterName = "King Of the World",
                      ArticleID =firstidAr+2 },
                     new Comment{
                      Title ="I hate this very much" ,
                      Content ="Bad i hate it",
                      WriterName = "King Of the World",
                      ArticleID =firstidAr+3 },
                      new Comment{
                      Title ="I hate this very much" ,
                      Content ="Bad i hate it",
                      WriterName = "King Of the World",
                      ArticleID =firstidAr+4 },
                       new Comment{
                      Title ="I hate this very much" ,
                      Content ="Bad i hate it",
                      WriterName = "King Of the World",
                      ArticleID =firstidAr }

                };
            foreach (Comment c in Comments)
            {
                context.Comment.Add(c);
            }
            context.SaveChanges();

        }
    }
}



