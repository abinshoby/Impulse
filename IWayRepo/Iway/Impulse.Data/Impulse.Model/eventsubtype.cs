//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Impulse.Model
{
    using System;
    using System.Collections.Generic;

    public partial class EventSubType
    {
        public EventSubType()
        {
            //this.EventDetails = new HashSet<EventDetail>();
            //this.EventType_Place = new HashSet<EventType_Place>();
        }

        public int EventSubTypeId { get; set; }
        public string SubTypeName { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> EventTypeId { get; set; }

        // public virtual ICollection<EventDetail> EventDetails { get; set; }
        public virtual EventType EventType { get; set; }
        // public virtual ICollection<EventType_Place> EventType_Place { get; set; }
    }
    public class EventSubTypeModel
    {

        public int EventLocationId { get; set; }
        public int EventSubTypeId { get; set; }
        public string SubTypeName { get; set; }
        public bool IsDeleted { get; set; }
        public string Health { get; set; }
    }


}



