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
    
    public partial class fs_category
    {
        public int c_id { get; set; }
        public string c_name { get; set; }
        public string c_image { get; set; }
        public Nullable<int> c_res_id { get; set; }
        public int c_status { get; set; }
    
        public virtual fs_restaurant fs_restaurant { get; set; }
    }
}
