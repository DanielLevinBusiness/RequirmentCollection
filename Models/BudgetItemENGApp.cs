using Microsoft.EntityFrameworkCore;
using SQLite;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpsRequirmentCollectionWeb.Models
{
    public class BudgetItemENGApp
    {
        [Key]
        public int ID { get; set; }
        public string ProjectName { get; set; }
        public string Requestor { get; set; }
        public string PartNumber { get; set; }
        public string BoradName { get; set; }
        public string GroupAndTeam { get; set; }
        public int Quantity { get; set; }
        public int QuantityOther { get; set; }
        public string ItemJustification { get; set; }
        public string Setup { get; set; }
        public string SiStep { get; set; }
        public int? ApprovedQuantity { get; set; }
        public int? ApprovedQuantityOther { get; set; }
        public string? APO { get; set; }
        public string? ETA { get; set; }

    }
}
