using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator
{
    public class Operations
    {
        public float _fstoperand { get; set; }
        public float _sndoperand { get; set; }

        public char _char1 { get; set; }

        public float _result { get; set; }

    }
    public class History
    {
        public List<History> history { get; set; }
        public History()
        {
            history = new List<History>();
        }
    }
}
