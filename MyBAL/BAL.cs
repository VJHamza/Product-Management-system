using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using MyDAL;

namespace MyBAL
{
    public class BAL
    {
        //DBManager db = new DBManager();
        public int DataInsert(UserDTO us)
        {
            return DBManager.DataInsert(us);

        }
    }
}
