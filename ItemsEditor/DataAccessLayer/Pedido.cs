namespace ItemsEditor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pedido")]
    public partial class Pedido
    {
        public int ID { get; set; }

        [StringLength(20)]
        public string NumeroPedido { get; set; }

        [StringLength(20)]
        public string NumeroReproceso { get; set; }

        [StringLength(10)]
        public string Cantidad { get; set; }

        [StringLength(10)]
        public string ArticuloProducto { get; set; }

        [StringLength(30)]
        public string DescripcionProducto { get; set; }

        [StringLength(30)]
        public string ArticuloBarebone { get; set; }

        [StringLength(30)]
        public string ArticuloGabinete { get; set; }

        [StringLength(30)]
        public string PedirEmbalaje { get; set; }

        [StringLength(30)]
        public string EstadoMercaderia { get; set; }

        [StringLength(30)]
        public string FechaIngreso { get; set; }

        [StringLength(30)]
        public string FechaVerificado { get; set; }

        [StringLength(30)]
        public string FechaEgreso { get; set; }

        [StringLength(30)]
        public string NombreIngresante { get; set; }

        [StringLength(30)]
        public string ApellidoIngresante { get; set; }

        [StringLength(30)]
        public string LegajoIngresante { get; set; }

        [StringLength(30)]
        public string NombreVerificador { get; set; }

        [StringLength(30)]
        public string ApellidoVerificador { get; set; }

        [StringLength(30)]
        public string LegajoVerificador { get; set; }

        [StringLength(30)]
        public string NombreSupervisorDesignado { get; set; }

        [StringLength(30)]
        public string ApellidoSupervisorDesignado { get; set; }

        [StringLength(30)]
        public string LegajoSupervisorDesignado { get; set; }

        [StringLength(30)]
        public string EstadoPedido { get; set; }

        [StringLength(65)]
        public string Observaciones { get; set; }
    }
}
