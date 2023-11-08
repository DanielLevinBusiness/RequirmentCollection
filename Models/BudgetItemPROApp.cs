using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OpsRequirmentCollectionWeb.Models
{
    public class BudgetItemPROApp
    {
        [Key]
        public int ID { get; set; }
        public string ProjectName { get; set; }
        public string Requestor { get; set; }
        public string PartNumber { get; set; }
        public string FormFactor { get; set; }
        public string GroupAndTeam { get; set; }
        public int Quantity { get; set; }
        public int QuantityOther { get; set; }
        public string ItemJustification { get; set; }
        public string Skew { get; set; }
        public int? ApprovedQuantity { get; set; }
        public int? ApprovedQuantityOther { get; set; }
        public string? APO { get; set; }
        public string? ETA { get; set; }

    }
}
