

using HotelApp.API.Database.Entities;
using HotelApp.API.Databases.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.API.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Hotel> Hotels { get; set; } = null!;
        public DbSet<HotelRoom> HotelRooms { get; set; } = null!;
        public DbSet<Province> Provinces { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;

    }
}