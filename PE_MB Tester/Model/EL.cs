using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace PE_MB_Tester.ELs
{
    class EL
    {
        const int _KtELconnected = 111;
        public double _measuredValueMinLimit = 0;
        public double _measuredValueMaxLimit = 1.3;
        string _resourceName = "";
        string _fileName = "";
        bool _isConnected = false;
        bool _lastTestResult = false;
        double _lastTestResultValue = 0;

        public bool isConnected()
        {
            return _isConnected;
        }

        public string[] checkConnection()
        {
            string[] returnValue = new string[2];
            returnValue[0] = "Connection - FAIL";
            returnValue[1] = "";
            string arguments = _resourceName + " " + "connect";
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = _fileName,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            if (_resourceName is not null && _resourceName.Length > 0)
            {
                //add chcecking if there is file and add try catch
                proc.Start();
                while (!proc.StandardOutput.EndOfStream)
                {
                    returnValue[1] = returnValue[1] + proc.StandardOutput.ReadLine() + "; ";
                }
                if (proc.ExitCode == 111)
                {
                    returnValue[0] = "Connection - PASS";
                    _isConnected = true;
                }else
                {
                    _isConnected = false;
                }
            }
            return returnValue;
        }
        public void setVisaAdress(string name)
        {
            _resourceName = name;
        }

        public void setControlAppName(string name)
        {
            _fileName = name;
        }

        public string[] test()
        {
            string[] returnValue = new string[2];
            returnValue[0] = "Test - FAIL";
            returnValue[1] = "";
            string arguments = _resourceName + " " + "test";
            double testResult;
            _lastTestResult = false;
            _lastTestResultValue = 0;
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = _fileName,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            if (_resourceName is not null && _resourceName.Length > 0)
            {
                //add chcecking if there is file and add try catch
                proc.Start();
                while (!proc.StandardOutput.EndOfStream)
                {
                    returnValue[1] = returnValue[1] + proc.StandardOutput.ReadLine() + "; ";
                }
                testResult = (double)proc.ExitCode / 100;
                _lastTestResultValue = testResult;
                if (testResult >= _measuredValueMinLimit && testResult <= _measuredValueMaxLimit)
                {
                    returnValue[0] = "Test - PASS" + "&" + testResult;
                    _lastTestResult = true;
                }
            }
            return returnValue;
        }
        public void setTestLimits(double minTestLimit, double maxTestLimit)
        {
            _measuredValueMinLimit = minTestLimit;
            _measuredValueMaxLimit = maxTestLimit;
        }
        public double getTestResultValue()
        {
            return _lastTestResultValue;
        }

        public bool getTestResult()
        {
            return _lastTestResult;
        }
    }
}