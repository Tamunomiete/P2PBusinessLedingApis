using Microsoft.EntityFrameworkCore;
using P2PBusinessLedingApis.DAL.Models.Users;
using System.Data;

namespace P2PBusinessLedingApis
{
    public partial class P2PContext : DbContext
    {
        public P2PContext()
        {
        }
        public P2PContext(DbContextOptions<P2PContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-LKKLGQJ;Initial Catalog=PeertoPeerDbLending;Integrated Security=True;TrustServerCertificate=True");
                //< add name = "PrimeHopePSBEntities" connectionString = "metadata=res://*/Models.Model1.csdl|res://*/Models.Model1.ssdl|res://*/Models.Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=DESKTOP-OQ86OTU\MSSQLSERVER19;initial catalog=Hope;integrated security=false; User ID=sa; Password=infosight!12;App=EntityFramework&quot;" providerName = "System.Data.EntityClient" />
            }
        }

        // DbSet for each entity/table
        public DbSet<tbl_users> tbl_users { get; set; }
        public DbSet<tbl_Roles> tbl_Roles { get; set; }
       

        // Add DbSet for other entities as needed

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships, constraints, etc.
            // Example: modelBuilder.Entity<Loan>().HasKey(l => l.LoanId);

            // Add configurations for relationships, indexes, etc.
            // Example: modelBuilder.Entity<Loan>().HasOne(l => l.Borrower).WithMany(u => u.Loans).HasForeignKey(l => l.BorrowerUserId);
        }
    }
}
