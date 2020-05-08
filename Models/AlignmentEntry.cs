using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace app_template.Models
{
    public class AlignmentEntry
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public int AlignmentType { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
