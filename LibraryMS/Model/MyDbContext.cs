using System.Data.Entity;
using System;


namespace Model
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("name=Db")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Classify> Classifys { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Borrow> Borrows { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(x => x.UserName).HasMaxLength(200);
            modelBuilder.Entity<User>().Property(x => x.Password).HasMaxLength(200);
            modelBuilder.Entity<User>().Property(x => x.PhoneNum).HasMaxLength(200);
            modelBuilder.Entity<User>().Property(x => x.Address).HasMaxLength(500);

            modelBuilder.Entity<Classify>().ToTable("Classifys");
            modelBuilder.Entity<Classify>().Property(x => x.Name).HasMaxLength(50);

            modelBuilder.Entity<Book>().Property(x => x.Name).HasMaxLength(200);
            modelBuilder.Entity<Book>().Property(x => x.ISBN).HasMaxLength(200);
            modelBuilder.Entity<Book>().Property(x => x.Press).HasMaxLength(200);
            modelBuilder.Entity<Book>().Property(x => x.Position).HasMaxLength(200);
            modelBuilder.Entity<Book>().Property(x => x.Price).HasPrecision(18, 2);

            modelBuilder.Entity<Admin>().Property(x => x.UserName).HasMaxLength(200);
            modelBuilder.Entity<Admin>().Property(x => x.Password).HasMaxLength(200);
        }
    }
}
