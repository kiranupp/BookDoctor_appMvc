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
    
    public partial class Timeslot
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Timeslot()
        {
            this.Appointments = new HashSet<Appointment>();
        }
    
        public int id { get; set; }
        public string timeslot1 { get; set; }
        public Nullable<int> Doctorid { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ListofDoctor ListofDoctor { get; set; }
    }
}