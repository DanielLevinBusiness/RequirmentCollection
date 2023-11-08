using Microsoft.EntityFrameworkCore;

namespace OpsRequirmentCollectionWeb.Models
{
    [Keyless]
    public class Requestor
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
    }
}
