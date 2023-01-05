using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_MB_Tester.Tests
{
    internal class Test
    {
        const string pass = "PASS";
        const string fail = "FAIL";
        private string _result;
        public int id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public string result
        {
            get { return _result; }
            set
            {
                if (value == pass)
                {
                    _result = value;
                }
                else if (value == fail)
                {
                    _result = value;
                }
            }
        }
    }
}