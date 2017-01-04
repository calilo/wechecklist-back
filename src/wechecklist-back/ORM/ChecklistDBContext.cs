using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using wechecklist_back.ORM.Models;

namespace wechecklist_back.ORM
{
    public class ChecklistDBContext : DbContext
    {
        public ChecklistDBContext(DbContextOptions<ChecklistDBContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Checklist> Checklists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.OpenId)
                .IsUnique();

            modelBuilder.Entity<Checklist>()
                .HasIndex(c => new { c.PublishUserId, c.CreateTime });
            modelBuilder.Entity<Checklist>()
                .HasIndex(c => new { c.SubscribeUserId, c.CreateTime });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);


            //Users.Add(new User { HeadImgUrl = "defaultImg.png", UserName = "sunz", Token = "@bhsxx898ssdgg", ExpiryDate = DateTime.Parse("2018/01/01"), RefreshDate = DateTime.Parse("2017/10/01"), OpenId = "bhsxx898ssdgg", UserId = 1, UserType = (int)EnumUserUserType.NormalUser, CreateTime = DateTime.Now, CreateUserId = 1 });
            //SaveChanges();
        }
    }
}
