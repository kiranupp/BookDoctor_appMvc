//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookDoctor_appMvc
{
    using System;
    using System.Collections.Generic;
    
    public partial class Specialty
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Specialty()
        {
            this.ListofDoctors = new HashSet<ListofDoctor>();
        }
    
        public int id { get; set; }
        public string Specialty1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ListofDoctor> ListofDoctors { get; set; }
    }
}
