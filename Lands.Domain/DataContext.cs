

namespace Lands.Domain
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public class DataContext : DbContext
    {
        protected DataContext() : base("DefaultConnection")
        {

        }
    }
}
