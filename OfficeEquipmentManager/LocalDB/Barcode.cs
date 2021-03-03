namespace OfficeEquipmentManager.LocalDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Barcode")]
    public partial class Barcode
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Barcode()
        {
            Equipment = new HashSet<Equipment>();
        }

        public int Id { get; set; }

        public long BarcodeValue { get; set; }

        [NotMapped()]
        public string BarcodeStinrg { get { if (BarcodeValue != 0) return Convert.ToString(BarcodeValue); return null; } set { BarcodeStinrg = value; } }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Equipment> Equipment { get; set; }
    }
}
