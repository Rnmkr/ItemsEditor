namespace ItemsEditor
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PRDB : DbContext
    {
        public PRDB()
            : base("name=PRDB1")
        {
        }

        public virtual DbSet<Cambio> Cambio { get; set; }
        public virtual DbSet<Falla> Falla { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<Personal> Personal { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cambio>()
                .Property(e => e.Legajo)
                .IsUnicode(false);

            modelBuilder.Entity<Cambio>()
                .Property(e => e.Tecnico)
                .IsUnicode(false);

            modelBuilder.Entity<Cambio>()
                .Property(e => e.NumeroPedido)
                .IsUnicode(false);

            modelBuilder.Entity<Cambio>()
                .Property(e => e.Producto)
                .IsUnicode(false);

            modelBuilder.Entity<Cambio>()
                .Property(e => e.Modelo)
                .IsUnicode(false);

            modelBuilder.Entity<Cambio>()
                .Property(e => e.ArticuloItem)
                .IsUnicode(false);

            modelBuilder.Entity<Cambio>()
                .Property(e => e.CategoriaItem)
                .IsUnicode(false);

            modelBuilder.Entity<Cambio>()
                .Property(e => e.DescripcionItem)
                .IsUnicode(false);

            modelBuilder.Entity<Cambio>()
                .Property(e => e.VersionItem)
                .IsUnicode(false);

            modelBuilder.Entity<Cambio>()
                .Property(e => e.UUID)
                .IsUnicode(false);

            modelBuilder.Entity<Cambio>()
                .Property(e => e.CodigoFalla)
                .IsUnicode(false);

            modelBuilder.Entity<Cambio>()
                .Property(e => e.DescripcionFalla)
                .IsUnicode(false);

            modelBuilder.Entity<Cambio>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<Cambio>()
                .Property(e => e.EstadoCambio)
                .IsUnicode(false);

            modelBuilder.Entity<Cambio>()
                .Property(e => e.SupervisorModificacion)
                .IsUnicode(false);

            modelBuilder.Entity<Falla>()
                .Property(e => e.CodigoFalla)
                .IsUnicode(false);

            modelBuilder.Entity<Falla>()
                .Property(e => e.CategoriaFalla)
                .IsUnicode(false);

            modelBuilder.Entity<Falla>()
                .Property(e => e.DescripcionCategoria)
                .IsUnicode(false);

            modelBuilder.Entity<Falla>()
                .Property(e => e.DescripcionFalla)
                .IsUnicode(false);

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

            modelBuilder.Entity<Pedido>()
                .Property(e => e.NumeroPedido)
                .IsUnicode(false);

            modelBuilder.Entity<Pedido>()
                .Property(e => e.NumeroReproceso)
                .IsUnicode(false);

            modelBuilder.Entity<Pedido>()
                .Property(e => e.Cantidad)
                .IsUnicode(false);

            modelBuilder.Entity<Pedido>()
                .Property(e => e.ArticuloProducto)
                .IsUnicode(false);

            modelBuilder.Entity<Pedido>()
                .Property(e => e.DescripcionProducto)
                .IsUnicode(false);

            modelBuilder.Entity<Pedido>()
                .Property(e => e.ArticuloBarebone)
                .IsUnicode(false);

            modelBuilder.Entity<Pedido>()
                .Property(e => e.ArticuloGabinete)
                .IsUnicode(false);

            modelBuilder.Entity<Pedido>()
                .Property(e => e.PedirEmbalaje)
                .IsUnicode(false);

            modelBuilder.Entity<Pedido>()
                .Property(e => e.EstadoMercaderia)
                .IsUnicode(false);

            modelBuilder.Entity<Pedido>()
                .Property(e => e.FechaIngreso)
                .IsUnicode(false);

            modelBuilder.Entity<Pedido>()
                .Property(e => e.FechaVerificado)
                .IsUnicode(false);

            modelBuilder.Entity<Pedido>()
                .Property(e => e.FechaEgreso)
                .IsUnicode(false);

            modelBuilder.Entity<Pedido>()
                .Property(e => e.NombreIngresante)
                .IsUnicode(false);

            modelBuilder.Entity<Pedido>()
                .Property(e => e.ApellidoIngresante)
                .IsUnicode(false);

            modelBuilder.Entity<Pedido>()
                .Property(e => e.LegajoIngresante)
                .IsUnicode(false);

            modelBuilder.Entity<Pedido>()
                .Property(e => e.NombreVerificador)
                .IsUnicode(false);

            modelBuilder.Entity<Pedido>()
                .Property(e => e.ApellidoVerificador)
                .IsUnicode(false);

            modelBuilder.Entity<Pedido>()
                .Property(e => e.LegajoVerificador)
                .IsUnicode(false);

            modelBuilder.Entity<Pedido>()
                .Property(e => e.NombreSupervisorDesignado)
                .IsUnicode(false);

            modelBuilder.Entity<Pedido>()
                .Property(e => e.ApellidoSupervisorDesignado)
                .IsUnicode(false);

            modelBuilder.Entity<Pedido>()
                .Property(e => e.LegajoSupervisorDesignado)
                .IsUnicode(false);

            modelBuilder.Entity<Pedido>()
                .Property(e => e.EstadoPedido)
                .IsUnicode(false);

            modelBuilder.Entity<Pedido>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<Personal>()
                .Property(e => e.NumeroLegajo)
                .IsUnicode(false);

            modelBuilder.Entity<Personal>()
                .Property(e => e.NumeroAcceso)
                .IsFixedLength();

            modelBuilder.Entity<Personal>()
                .Property(e => e.Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Personal>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Personal>()
                .Property(e => e.SegundoNombre)
                .IsUnicode(false);

            modelBuilder.Entity<Personal>()
                .Property(e => e.FechaIngreso)
                .IsUnicode(false);

            modelBuilder.Entity<Personal>()
                .Property(e => e.Sector)
                .IsUnicode(false);

            modelBuilder.Entity<Personal>()
                .Property(e => e.Domicilio)
                .IsUnicode(false);

            modelBuilder.Entity<Personal>()
                .Property(e => e.DNI)
                .IsUnicode(false);

            modelBuilder.Entity<Personal>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Personal>()
                .Property(e => e.Celular)
                .IsUnicode(false);

            modelBuilder.Entity<Personal>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Personal>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);
        }
    }
}
