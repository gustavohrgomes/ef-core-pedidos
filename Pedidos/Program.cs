using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Pedidos
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new Data.ApplicationContext();

            var exists = db.Database.GetPendingMigrations().Any();
            if (exists)
            {
                // codigo
            }

            Console.WriteLine("Hello World!");
        }
    }
}
