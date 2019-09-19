using GPSReaderNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RdaLog
{
    public class RdaLog
    {
        private FormMain formMain;
        private RdaLogConfig config;
        private string _rafa;
        public string rafa { get { return _rafa; } }
        private string _rda;
        public string rda { get { return _rda; } }
        private string _wff;
        public string wff { get { return _wff; } }
        private string _loc;
        public string loc { get { return _loc; } }
        private string _userField;
        public string userField { get { return _userField; } }

        private Coords _coords;
        public Coords coords { get { return _coords.clone(); } }


    }
}
