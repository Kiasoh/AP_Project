using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public interface IPerson <T>
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string email { get; set; }
        //string id { get; set; }

 //       T GetPerson(int id);


    }
}
