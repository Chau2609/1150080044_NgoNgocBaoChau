//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Chau.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TyGia
    {
        public string MaNguyenTe { get; set; }
        public System.DateTime NgayApDungGiaMoi { get; set; }
        public string QuyRaVietNamDong { get; set; }
    
        public virtual DMNguyenTe DMNguyenTe { get; set; }
    }
}
