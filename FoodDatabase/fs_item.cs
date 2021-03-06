//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FoodDatabase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class fs_item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public fs_item()
        {
            this.fs_order = new HashSet<fs_order>();
        }
    
        public int i_id { get; set; }

        [Required(ErrorMessage = "Required Field."), DisplayName("Item Name")]
        public string i_name { get; set; }

        [Required(ErrorMessage = "Required Field."), DisplayName("Item Price")]
        public string i_price { get; set; }

        [Required(ErrorMessage = "Required Field."), DisplayName("Description")]
        public string i_desc { get; set; }

        [DisplayName("Category ID")]
        public Nullable<int> i_c_id { get; set; }

        [DisplayName("Image")]
        public string i_image { get; set; }

        [DisplayName("Restaurant ID")]
        public Nullable<int> i_r_id { get; set; }

        [DisplayName("Status")]
        public Nullable<int> i_status { get; set; }

        [NotMapped]
        public List<fs_category> CategoryCollection { get; set; }

        public virtual fs_category fs_category { get; set; }
        public virtual fs_restaurant fs_restaurant { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<fs_order> fs_order { get; set; }
    }
}
