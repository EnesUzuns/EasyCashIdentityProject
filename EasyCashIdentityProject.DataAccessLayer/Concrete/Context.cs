﻿using EasyCashIdentityProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.DataAccessLayer.Concrete
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=192.168.1.10;port=3306;database=EasyCashDb;user=root;password=beeNEO!34680", new MySqlServerVersion(new Version(8, 0, 31)));
        }
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }
        public DbSet<CustomerAccountProcess> CustomerAccountProcesses { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CustomerAccountProcess>()
                   .HasOne(x => x.SenderCustomer)
                   .WithMany(y  => y.CustomerSender)
                   .HasForeignKey(z => z.SenderID)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<CustomerAccountProcess>()
                .HasOne(x => x.ReceiverCustomer)
                .WithMany(y => y.CustomerReceiver)
                .HasForeignKey(z => z.ReceiverID)
                .OnDelete(DeleteBehavior.ClientSetNull);
            
            base.OnModelCreating(builder);
        }
    }
}
