using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator
{
    class Singleton
    {
        private Commands _singleton;
        public Commands singleton
        {
            get
            {
                if(_singleton == null)
                {
                    _singleton = new Commands();
                }
                return _singleton;
            }
        }
    }
}
