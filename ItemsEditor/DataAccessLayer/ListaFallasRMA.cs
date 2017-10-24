namespace ItemsEditor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ListaFallasRMA")]
    public partial class ListaFallasRMA
    {
        public int ID { get; set; }

        [Required]
        [StringLength(5)]
        public string CodigoFalla { get; set; }

        [Required]
        [StringLength(5)]
        public string CategoriaFalla { get; set; }

        [Required]
        [StringLength(50)]
        public string DescripcionFalla { get; set; }
    }
}
