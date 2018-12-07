using System.Data.Entity.ModelConfiguration;

namespace Lands.Domain
{
    internal class MatchesMap : EntityTypeConfiguration<Match>
    {
        public MatchesMap()
        {
            HasRequired(o => o.Home)
                .WithMany(m => m.Homes)
                .HasForeignKey(m => m.HomeId);

            HasRequired(o => o.Visitor)
                .WithMany(m => m.Visitors)
                .HasForeignKey(m => m.VisitorId);
        }
    }
}