

namespace Lands.Domain
{
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
           
        }

        public DbSet<Lands.Domain.User> Users { get; set; }

        public DbSet<Lands.Domain.UserType> UserTypes { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<GroupTeam> GroupTeams { get; set; }
    }
}
