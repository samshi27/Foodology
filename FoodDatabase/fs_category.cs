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
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class fs_category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public fs_category()
        {
            this.fs_item = new HashSet<fs_item>();
        }
    
        public int c_id { get; set; }
        public string c_name { get; set; }
        public string c_image { get; set; }
        public Nullable<int> c_r_id { get; set; }
        public Nullable<int> c_status { get; set; }

        [NotMapped]
        public List<fs_restaurant> RestaurantCollection { get; set; }

        public virtual fs_restaurant fs_restaurant { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<fs_item> fs_item { get; set; }
    }
}
