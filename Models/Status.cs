using Dapper.Contrib.Extensions;
using Microsoft.EntityFrameworkCore;

namespace OpsRequirmentCollectionWeb.Models
{
    public class submitStatus
    {
        [Key]
        public int ID { get; set; }
        public bool IsOpen { get; set; }

    }
}
