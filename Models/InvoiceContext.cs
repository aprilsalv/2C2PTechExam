using Microsoft.EntityFrameworkCore;

namespace _2C2PTechExam.Models
{
    public class InvoiceContext : DbContext
    {
        public InvoiceContext(DbContextOptions<InvoiceContext> options) : base(options)
        {
        }


       public DbSet<Invoice> Employee { get; set; }
    }
}
