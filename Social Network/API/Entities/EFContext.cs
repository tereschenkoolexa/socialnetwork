using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatUser>()
                .HasKey(t => new { t.IdChat, t.IdUser });
        }
        DbSet<Chat> Chats { get; set; }
        DbSet<ChatUser> ChatUsers { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Wall> Walls { get; set; }

    }
}
