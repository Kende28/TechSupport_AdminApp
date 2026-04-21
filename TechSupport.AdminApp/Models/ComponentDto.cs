using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.AdminApp.Models
{
    // Alkatrész adatmodell (szerkesztéshez)
    public class ComponentDto
    {
        // Alkatrész azonosító
        public int PartId { get; set; }
        
        // Alkatrész megnevezése
        public string PartName { get; set; }
        
        // Alkatrész leírása
        public string? PartDescription { get; set; }
        
        // Alkatrész láthatósága
        public bool PartVisible { get; set; }
    }
}
