using System;
using System.Collections.Generic;
using System.Text;

namespace CheckLinksConsole
{
    using Microsoft.EntityFrameworkCore;

    // inheriting from DbContext 
    public class LinksDb : DbContext
    {
        public DbSet<LinkCheckResult> Links {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = @"Server=localhost;Database=Links;User Id=sa;Password=whatever12!";
            optionsBuilder.UseSqlServer(connection);
        }
    }

}