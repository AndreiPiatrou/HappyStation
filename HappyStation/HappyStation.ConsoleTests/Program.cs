using System;

using HappyStation.Core.DatabaseContext;
using HappyStation.Core.Entities;

namespace HappyStation.ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new DatabaseContext())
            {
                db.Comments.Add(new Comment
                {
                    CreatedAt = DateTime.Now,
                    Text = "test",
                    UserName = "username"
                });

                db.SaveChanges();
            }
        }
    }
}
