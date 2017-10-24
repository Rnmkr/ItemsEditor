namespace ItemsEditor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CambioRMA")]
    public partial class CambioRMA
    {
        public int ID { get; set; }

        public int IdCambio { get; set; }

        [Required]
        [StringLength(10)]
        public string NumeroLegajoTecnico { get; set; }

        [Required]
        [StringLength(30)]
        public string NombreTecnico { get; set; }

        [Required]
        [StringLength(30)]
        public string ApellidoTecnico { get; set; }

        [Required]
        [StringLength(30)]
        public string FechaCambio { get; set; }

        [Required]
        [StringLength(5)]
        public string HoraCambio { get; set; }

        [Required]
        [StringLength(20)]
        public string NumeroPedido { get; set; }

        [Required]
        [StringLength(30)]
        public string TipoProducto { get; set; }

        [Required]
        [StringLength(30)]
        public string ModeloProducto { get; set; }

        [Required]
        [StringLength(10)]
        public string ArticuloItem { get; set; }

        [Required]
        [StringLength(5)]
        public string CategoriaItem { get; set; }

        [Required]
        [StringLength(30)]
        public string DescripcionItem { get; set; }

        [Required]
        [StringLength(30)]
        public string VersionItem { get; set; }

        [Required]
        [StringLength(30)]
        public string SerialNumberItem { get; set; }

        [Required]
        [StringLength(5)]
        public string CodigoFalla { get; set; }

        [Required]
        [StringLength(50)]
        public string DescripcionFalla { get; set; }

        [Required]
        [StringLength(65)]
        public string Observaciones { get; set; }

        [Required]
        [StringLength(10)]
        public string EstadoCambio { get; set; }

        [Required]
        [StringLength(50)]
        public string SupervisorModificacion { get; set; }

        [Required]
        [StringLength(30)]
        public string FechaModificacion { get; set; }
    }
}
