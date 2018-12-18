using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserDTO
    {
        public int userID { get; set; }
        public string login { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public int IsActive { get; set; }
        public string picURL { get; set; }
        public int IsAdmin { get; set; }
        public DateTime createdOn { get; set; }
    }
}
