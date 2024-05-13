using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vanalyze.Models
{
    namespace Vanalyze.Models
    {
        public struct WindowRecord
        {
            public string WindowTitle;
            public string ProcessName;

            public WindowRecord(string windowTitle, string processName)
            {
                WindowTitle = windowTitle;
                ProcessName = processName;
            }
        }
    }

}
