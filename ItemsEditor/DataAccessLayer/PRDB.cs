namespace ItemsEditor.DataAccessLayer
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PRDB : DbContext
    {
        public PRDB()
            : base("name=PRDB")
        {
        }

        public virtual DbSet<Item> Item { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .Property(e => e.TipoProducto)
                .IsUnicode(false);

            modelBuilder.Entity<Item>()
                .Property(e => e.ModeloProducto)
                .IsUnicode(false);

            modelBuilder.Entity<Item>()
                .Property(e => e.ArticuloItem)
                .IsUnicode(false);

            modelBuilder.Entity<Item>()
                .Property(e => e.CategoriaItem)
                .IsUnicode(false);

            modelBuilder.Entity<Item>()
                .Property(e => e.VersionItem)
                .IsUnicode(false);

            modelBuilder.Entity<Item>()
                .Property(e => e.DescripcionItem)
                .IsUnicode(false);

            modelBuilder.Entity<Item>()
                .Property(e => e.UUID)
                .IsUnicode(false);
        }
    }
}
