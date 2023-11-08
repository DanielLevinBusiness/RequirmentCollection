using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OpsRequirmentCollectionWeb.Models
{
    public class BudgetItemSI
    {
        [Key]
        public int ID { get; set; }
        public string ProjectName { get; set; }
        public string Requestor { get; set; }
        public string PartNumber { get; set; }
        public string PackageNumber { get; set; }
        public string GroupAndTeam { get; set; }
        public int Quantity { get; set; }
        public int QuantityOther { get; set; }
        public string ItemJustification { get; set; }
        public string MoldType { get; set; }
        public string BlindOrTested { get; set; }
        public string SiFLV { get; set; }
        public string MiniSkew { get; set; }
        public string Step { get; set; }
        public string Recipient { get; set; }
        public string DeliveryAddress { get; set; }
        public string ETDDate { get; set; }

    }
}
