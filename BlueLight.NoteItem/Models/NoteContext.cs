using Microsoft.EntityFrameworkCore;

namespace BlueLight.NoteItem.Models
{
    public class NoteContext : DbContext
    {
        public NoteContext(DbContextOptions<NoteContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>().ToTable("Note");
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Note> Notes { get; set; }
    }
}
