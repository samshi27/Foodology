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

    public partial class fs_item
    {
        public int i_id { get; set; }
        public string i_name { get; set; }
        public string i_price { get; set; }
        public string i_desc { get; set; }
        public Nullable<int> i_c_id { get; set; }
        public string i_image { get; set; }
        public Nullable<int> i_r_id { get; set; }
        public Nullable<int> i_status { get; set; }

        [NotMapped]
        public List<fs_category> CategoryCollection { get; set; }

        public virtual fs_category fs_category { get; set; }
        public virtual fs_restaurant fs_restaurant { get; set; }
    }
}
