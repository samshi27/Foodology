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
    
    public partial class fs_user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public fs_user()
        {
            this.fs_dish = new HashSet<fs_dish>();
        }
    
        public int u_id { get; set; }
        public string u_firstname { get; set; }
        public string u_lastname { get; set; }
        public string u_gender { get; set; }
        public string u_contact { get; set; }
        public string u_email { get; set; }
        public string u_password { get; set; }
        public string u_username { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<fs_dish> fs_dish { get; set; }
    }
}
