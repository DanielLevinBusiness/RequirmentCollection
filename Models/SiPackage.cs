using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OpsRequirmentCollectionWeb.Models
{
    public class SiPackage
    {
        [Key]
        public int ID { get; set; }
        public string ProjectName { get; set; }
        public string PackageNumber { get; set; }
        public string PartNumber { get; set; }
        public float PricePerUnit { get; set; }
        public string SiFLV { get; set; }
        public string Comment { get; set; }

    }
}
