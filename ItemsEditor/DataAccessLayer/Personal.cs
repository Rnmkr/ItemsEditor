namespace ItemsEditor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Personal")]
    public partial class Personal
    {
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        public string NumeroLegajo { get; set; }

        [Required]
        [StringLength(10)]
        public string NumeroAcceso { get; set; }

        [Required]
        [StringLength(30)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }

        [StringLength(30)]
        public string SegundoNombre { get; set; }

        [StringLength(30)]
        public string FechaIngreso { get; set; }

        [StringLength(20)]
        public string Sector { get; set; }

        [StringLength(50)]
        public string Domicilio { get; set; }

        [StringLength(10)]
        public string DNI { get; set; }

        [StringLength(15)]
        public string Telefono { get; set; }

        [StringLength(15)]
        public string Celular { get; set; }

        [StringLength(30)]
        public string email { get; set; }

        [StringLength(65)]
        public string Observaciones { get; set; }
    }
}
