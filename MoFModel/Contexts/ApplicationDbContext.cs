using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoFModel.Entities;

namespace MoFModel.Contexts
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer("Server=tcp:studydbserver.database.windows.net,1433;Initial Catalog=MoFDbServer;Persist Security Info=False;User ID=study;Password=test3089!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        public DbSet<MovieTheater> MovieTheater { get; set; }
        public DbSet<Review> Review { get; set; }
    }
}
