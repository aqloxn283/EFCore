using BlueLight.NoteItem.Models.Relationship;
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
            modelBuilder.Entity<Note>().ToTable("Note"); //修改生成表名不采用复数
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
