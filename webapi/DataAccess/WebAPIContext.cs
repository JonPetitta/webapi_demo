namespace webapi.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class WebAPIContext : DbContext
    {
        public WebAPIContext()
            : base("name=mysql")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserTask> UserTasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<UserTask>()
                .Property(e => e.Title)
                .IsUnicode(false);
        }
    }
}
