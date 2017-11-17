namespace ItemsEditor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Item")]
    public partial class Item
    {
        public int ID { get; set; }

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
        public string VersionItem { get; set; }

        [Required]
        [StringLength(30)]
        public string DescripcionItem { get; set; }

        [StringLength(60)]
        public string UUID { get; set; }
    }
}
