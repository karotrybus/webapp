//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace webapp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Boat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }
        public int Bunks { get; set; }
        public bool isRented { get; set; }
        public int SkipperId { get; set; }
        public int PortId { get; set; }
    
        public virtual Skipper Skipper { get; set; }
        public virtual Port Port { get; set; }
    }
}
