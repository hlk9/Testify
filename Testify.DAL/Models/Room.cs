using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testify.DAL.Models
{
    public class Room
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Address { get; set; }
        public bool? Status { get; set; }
    }
}
