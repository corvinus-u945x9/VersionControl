using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRF_3.Gyak3._0.Entities
{
    public class User
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string FullName { get; set; }
      
    }
}
