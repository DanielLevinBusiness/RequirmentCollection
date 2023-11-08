using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OpsRequirmentCollectionWeb.Models
{
    public class BudgetItemPRO
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

    }
}
