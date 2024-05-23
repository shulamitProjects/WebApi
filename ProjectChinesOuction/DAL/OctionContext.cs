using Microsoft.EntityFrameworkCore;
using ProjectChinesOuction.Models;

namespace ProjectChinesOuction.DAL
{

    public class OctionContext : DbContext
    {
        

        public OctionContext(DbContextOptions<OctionContext> options) : base(options)
        {

        }

        //הגדרת הטבלאות שנרצה שיהיו לנו בDB עם שמות הטבלאות שיווצרו
        public DbSet<Donor> Donor { get; set; }
        //public DbSet<EnumGiftCategory> EnumGiftCategory { get; set; }
        //public DbSet<EnumRoleUser> EnumRoleUser { get; set; }
        public DbSet<Gift> Gift { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Raffle> Raffle { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Winner> Winner { get; set; }



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("Data Source=SRV2\\PUPILS;Initial Catalog=Lesson;Integrated Security=True");
        //    //Data Source=SRV2\PUPILS;Initial Catalog=Lesson;Integrated Security=True
        //    //Data Source=LAPTOP-SDPMCEIP;Initial Catalog=EFCoreLesson;Integrated Security=True
        //}
        //protected override void OnModelCreating(ModelBuilder modelBuilder)//שינוים על המודל
        //{
        //    base.OnModelCreating(modelBuilder);
        //}

    }
}

