namespace ItemsEditor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cambio")]
    public partial class Cambio
    {
        public int ID { get; set; }

        public int IdCambio { get; set; }

        [Required]
        [StringLength(6)]
        public string Legajo { get; set; }

        [Required]
        [StringLength(30)]
        public string Tecnico { get; set; }

        public DateTime FechaCambio { get; set; }

        [Required]
        [StringLength(13)]
        public string NumeroPedido { get; set; }

        [Required]
        [StringLength(30)]
        public string Producto { get; set; }

        [Required]
        [StringLength(30)]
        public string Modelo { get; set; }

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

        [StringLength(60)]
        public string UUID { get; set; }

        [Required]
        [StringLength(5)]
        public string CodigoFalla { get; set; }

        [Required]
        [StringLength(60)]
        public string DescripcionFalla { get; set; }

        [StringLength(60)]
        public string Observaciones { get; set; }

        [Required]
        [StringLength(10)]
        public string EstadoCambio { get; set; }

        [StringLength(30)]
        public string SupervisorModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}
