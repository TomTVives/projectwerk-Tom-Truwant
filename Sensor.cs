using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectwerk_Tom_Truwant
{
    internal class Sensor
    {
        public int Meetafstand { get; set; }
        public int Type { get; set; }
        public string Merk { get; set; }

        public string merksensor()
        {
            return $"Merk: {Merk}";
        }
        public string typesensor()
        {
            return $"Type: {Type}";
        }
        public string afstandsensor()
        {
            return $"Meetafstand: {Meetafstand} meter";
        }
    }
}
