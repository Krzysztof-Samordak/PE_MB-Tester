/// <copyright>3Shape A/S</copyright>
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
        //Auxiliary variables
        const int _KtELconnected = 111;
        const double divider = 1000;
        bool _isConnected = false;
        bool _lastTestResult = false;
        double _lastTestResultValue = 0;

        //Name of test file that will be started as proc - set to default
        string _fileName = "ThreeShapeKtEL30000.exe";

        //DC Electronic Load VISA adress
        string _resourceName = "";

        //Test configuration variables - set to default
        public double measuredValueMinLimit = 1.18;
        public double measuredValueMaxLimit = 1.24;
        double _startCurrent = 1.17;
        double _maxCurrent = 1.24;
        double _currentIncreasement = 0.001;
        bool _checkVoltageAfterTest = false;

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
            string arguments = _resourceName + " " + "test" + " " + _startCurrent + " " + _maxCurrent + " " + _currentIncreasement + " " + _checkVoltageAfterTest;
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
                testResult = ((double)proc.ExitCode) / divider;
                _lastTestResultValue = testResult;
                if (testResult >= measuredValueMinLimit && testResult <= measuredValueMaxLimit)
                {
                    returnValue[0] = "Test - PASS, result: " + testResult;
                    _lastTestResult = true;
                }
            }
            return returnValue;
        }
        public void setTestLimits(double minTestLimit, double maxTestLimit)
        {
            measuredValueMinLimit = minTestLimit;
            measuredValueMaxLimit = maxTestLimit;
        }
        public double getTestResultValue()
        {
            return _lastTestResultValue;
        }

        public bool getTestResult()
        {
            return _lastTestResult;
        }

        public void setTestParameters(double startCurrent, double maxCurrent, double currentIncreasement, bool checkVoltageAfterTest)
        {
            _startCurrent = startCurrent;
            _maxCurrent = maxCurrent;
            _currentIncreasement = currentIncreasement;
            _checkVoltageAfterTest = checkVoltageAfterTest;
        }
        public double startCurrent
        {
            get { return _startCurrent; }
        }
        public double maxCurrent
        {
           get { return _maxCurrent; }
        }
    }
}