using Microsoft.EntityFrameworkCore;
using System;
using Hahn.ApplicatonProcess.July2021.Domain.Entities;

namespace Hahn.ApplicatonProcess.July2021.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }
    }
}
