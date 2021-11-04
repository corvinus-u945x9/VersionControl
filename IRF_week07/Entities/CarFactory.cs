using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRF_week07
{
    class CarFactory:IToyFactory
    {
        public Toy CreateNew()
        {
            return new Car();
        }
    }
}
