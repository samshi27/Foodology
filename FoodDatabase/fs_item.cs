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
    
    public partial class fs_item
    {
        public int i_id { get; set; }

        [Required(ErrorMessage = "Required Field"), DisplayName("Item Name")]
        public string i_name { get; set; }

        [Required(ErrorMessage = "Required Field"), DisplayName("Price")]
        public string i_price { get; set; }

        [Required(ErrorMessage = "Required Field"), DisplayName("Description")]
        public string i_desc { get; set; }
        public Nullable<int> i_c_id { get; set; }

        [Required(ErrorMessage = "Required Field"), DisplayName("Image")]
        public string i_image { get; set; }
        public Nullable<int> i_r_id { get; set; }
        public Nullable<int> i_status { get; set; }
    
        public virtual fs_category fs_category { get; set; }
        public virtual fs_restaurant fs_restaurant { get; set; }
    }
}
