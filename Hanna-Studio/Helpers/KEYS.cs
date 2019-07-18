using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanna_Studio.Helpers
{
    class KEYS
    {
        private const String EXPORT_KEY = "HannaIsTheBest99"; //16 char secret key for exporting a game file. Opening a game file in a client makes use of the same key
        public static String getExportKey() { return EXPORT_KEY; }
    }
}
