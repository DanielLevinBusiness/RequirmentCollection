using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OpsRequirmentCollectionWeb.Models
{
    public class ProductBoards
    {
        [Key]
        public int ID { get; set; }
        public string ProjectName { get; set; }
        public string FormFactor { get; set; }
        public string Skew { get; set; }
        public string PartNumber { get; set; }
        public float PricePerUnit { get; set; }

    }
}
