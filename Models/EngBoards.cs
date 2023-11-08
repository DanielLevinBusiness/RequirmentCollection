using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OpsRequirmentCollectionWeb.Models
{
    public class EngBoards
    {
        [Key]
        public int ID { get; set; }
        public string ProjectName { get; set; }
        public string BoardName { get; set; }
        public string Setup { get; set; }
        public string PartNumber { get; set; }
        public float PricePerUnit { get; set; }
        public string? Comment { get; set; }

    }
}
