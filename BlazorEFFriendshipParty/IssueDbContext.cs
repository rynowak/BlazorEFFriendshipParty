using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace BlazorEFFriendshipParty
{
    public class IssueDbContext : DbContext
    {
        private static int TotalCount = 1;

        public IssueDbContext(DbContextOptions<IssueDbContext> options)
            : base(options)
        {
            InstanceCount = Interlocked.Increment(ref TotalCount);
        }

        public DbSet<Issue> Issues { get; set; }

        public int InstanceCount { get; }
    }
}
