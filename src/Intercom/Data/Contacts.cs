using Intercom.Core;
using System.Collections.Generic;

namespace Intercom.Data
{
    public class Contacts : Models
    {
        public List<Contact> data { set; get; }
        public string type { get; set; }
        public int total_count { get; set; }
        public Pages pages { get; set; }

        public Contacts()
        {
        }
    }
}