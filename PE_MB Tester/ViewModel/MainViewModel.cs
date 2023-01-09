/// <copyright>3Shape A/S</copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FTD2XX_NET;
using System.Configuration;
using System.Windows;
using System.Management;
using System.Drawing;
using System.Windows.Threading;
using System.Windows.Input;
using PE_MB_Tester.Commands;
using PE_MB_Tester.USBDevices;
using PE_MB_Tester.ELs;
using PE_MB_Tester.Tests;
using System.Collections.ObjectModel;
namespace PE_MB_Tester.MainViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        //USB Devices initialization
        FT232 _relayController = new FT232();
        Logger _logger = new Logger();
        USBDevice _penDrive = new USBDevice();
        USBDevice _hubController = new USBDevice();
        USBDevice _usbCompositeDevice = new USBDevice();
        USBDevice _audioController = new USBDevice();
        USBDevice _networkAdapter = new USBDevice();
        USBDevice _flashDisk = new USBDevice();
        EL _ktEL34143a = new EL();

        //Timer setup
        const int time = 10;
        DispatcherTimer _dispatcherTimer = new DispatcherTimer();

        //Set number of tests to perform
        const int setNumberOfTests = 4;

        //Auxiliary variables
        int _numberOfTests = setNumberOfTests;
        const string en = "True";
        const string dis = "False";
        const string ready = "Open, close for auto start";
        const string testInprogress = "Test in progress!";
        const string testFinished = "Test finished!";
        const string pass = "PASS";
        const string fail = "FAIL";
        const string clear = "clear";

        //View variables setup
        private string _elStatus = "Disconnected";
        private string _startButton = en;
        private string _testerStatus;
        private string _testerStatusColor;
        private string _testResult;
        private string _testResultColor;
        private string _elStatusColor = "Red";
        private ObservableCollection<Test> _tests;

        //Create ICommand object
        public ICommand startTestCommand { get; set; }

        public ObservableCollection<Test> tests
        {
            get { return _tests; }

            set
            {
                _tests = value;
                OnPropertyChanged();
            }
        }
        public string testerStatusColor
        {
            get { return _testerStatusColor; }
            set
            {
                Color c = Color.FromName(value);
                if (c.IsKnownColor)
                {
                    _testerStatusColor = value;
                    OnPropertyChanged();
                }
            }
        }
        public string startButton
        {
            get { return _startButton; }
            set
            {
                if (value == en || value == dis)
                {
                    _startButton = value;
                    OnPropertyChanged();
                }
            }
        }
        public string testerStatus
        {
            get { return _testerStatus; }
            set
            {
                if(value == ready)
                {
                    testerStatusColor = "Orange";
                    _testerStatus = value;
                    OnPropertyChanged();
                }else if (value == testInprogress)
                {
                    testerStatusColor = "Red";
                    _testerStatus = value;
                    OnPropertyChanged();
                }else if (value == testFinished)
                {
                    testerStatusColor = "Green";
                    _testerStatus = value;
                    OnPropertyChanged();
                }
            }
        }
        public string testResult
        {
            get { return _testResult; }
            set
            {
                if (value == pass)
                {
                    testResultColor = "Green";
                    _testResult = value;
                    OnPropertyChanged();
                }else if(value == fail)
                {
                    testResultColor = "Red";
                    _testResult = value;
                    OnPropertyChanged();
                }
                else if (value == clear)
                {
                    _testResult = "";
                    OnPropertyChanged();
                }
            }
        }
        public string testResultColor
        {
            get { return _testResultColor; }
            set
            {
                Color c = Color.FromName(value);
                if (c.IsKnownColor)
                {
                    _testResultColor = value;
                    OnPropertyChanged();
                }
            }
        }
        public string elStatus
        {
            get { return _elStatus; }
            set
            {

                    _elStatus = value;
                    OnPropertyChanged();
            }
        }
        public string elStatusColor
        {
            get { return _elStatusColor; }
            set
            {
                Color c = Color.FromName(value);
                if (c.IsKnownColor)
                {
                    _elStatusColor = value;
                    OnPropertyChanged();
                }
            }
        }


        public MainViewModel()
        {
            //Read Configuration file and attach values do adequate variables
            ReadConfig();

            startTestCommand = new RelayCommand(StartTestClick);

            //Create tests list
            tests = new ObservableCollection<Test>();


            /*            logger.logMultiple(_relayController.FindDevices(), "Found FTDI devices: ");
                        logger.log("Required FTDI device is connected: " + (_relayController.Connect()).ToString());
                        logger.log("Bit_bang Mode on: " + (_relayController.SetBitBangMode()).ToString());
                        logger.log("Turned on power: " + (_relayController.TurnOnPin(31).ToString()));
                        _relayController.Read();
                        _relayController.TurnOffAllPins();
             */

            //setup Timer
            _dispatcherTimer.Tick += DispatcherTimerTick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, time);

            //check for required devices
            checkRequiredDevices();

            //start WMI USB Devices watcher
            StartEventWatcher();

            //set the Test Setup to ready
            testerStatus = ready;
            _logger.log(ready);
            _logger.log(_ktEL34143a._measuredValueMaxLimit.ToString());
        }

        ~MainViewModel()
        {
            //           _relayController.TurnOffAllPins();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void ReadConfig()
        {
            try
            {
                if ((ConfigurationManager.AppSettings.Get("ftdiSerialNumber") is not null) &&
                    ConfigurationManager.AppSettings.Get("PenDriveVid") is not null &&
                    ConfigurationManager.AppSettings.Get("PenDriveName") is not null &&
                    ConfigurationManager.AppSettings.Get("UsbAudioControllerVid") is not null &&
                    ConfigurationManager.AppSettings.Get("UsbHubControllerName") is not null &&
                    ConfigurationManager.AppSettings.Get("UsbAudioControllerVid") is not null &&
                    ConfigurationManager.AppSettings.Get("UsbAudioControllerName") is not null &&
                    ConfigurationManager.AppSettings.Get("UsbCompositeDeviceVid") is not null &&
                    ConfigurationManager.AppSettings.Get("UsbCompositeDeviceName") is not null &&
                    ConfigurationManager.AppSettings.Get("NetworkAdapterVid") is not null &&
                    ConfigurationManager.AppSettings.Get("NetworkAdapterName") is not null &&
                    ConfigurationManager.AppSettings.Get("FlashDiskVid") is not null &&
                    ConfigurationManager.AppSettings.Get("FlashDiskName") is not null &&
                    ConfigurationManager.AppSettings.Get("ElVisaAdress") is not null &&
                    ConfigurationManager.AppSettings.Get("ElControlAppName") is not null &&
                    ConfigurationManager.AppSettings.Get("ElMinTestLimit") is not null &&
                    ConfigurationManager.AppSettings.Get("ElMaxTestLimit") is not null)
                {
                    _relayController.setRequiredSerialNumber(ConfigurationManager.AppSettings.Get("ftdiSerialNumber"));
                    _penDrive.GetExpextedVid(ConfigurationManager.AppSettings.Get("PenDriveVid"));
                    _penDrive.GetExpextedName(ConfigurationManager.AppSettings.Get("PenDriveName"));

                    _hubController.GetExpextedVid(ConfigurationManager.AppSettings.Get("UsbHubControllerVid"));
                    _hubController.GetExpextedName(ConfigurationManager.AppSettings.Get("UsbHubControllerName"));

                    _audioController.GetExpextedVid(ConfigurationManager.AppSettings.Get("UsbAudioControllerVid"));
                    _audioController.GetExpextedName(ConfigurationManager.AppSettings.Get("UsbAudioControllerName"));

                    _usbCompositeDevice.GetExpextedVid(ConfigurationManager.AppSettings.Get("UsbCompositeDeviceVid"));
                    _usbCompositeDevice.GetExpextedName(ConfigurationManager.AppSettings.Get("UsbCompositeDeviceName"));

                    _networkAdapter.GetExpextedVid(ConfigurationManager.AppSettings.Get("NetworkAdapterVid"));
                    _networkAdapter.GetExpextedName(ConfigurationManager.AppSettings.Get("NetworkAdapterName"));

                    _flashDisk.GetExpextedVid(ConfigurationManager.AppSettings.Get("FlashDiskVid"));
                    _flashDisk.GetExpextedName(ConfigurationManager.AppSettings.Get("FlashDiskName"));

                    _ktEL34143a.setVisaAdress(ConfigurationManager.AppSettings.Get("ElVisaAdress"));
                    _ktEL34143a.setControlAppName(ConfigurationManager.AppSettings.Get("ElControlAppName"));
                    _ktEL34143a.setTestLimits(Convert.ToDouble(ConfigurationManager.AppSettings.Get("ElMinTestLimit")), Convert.ToDouble(ConfigurationManager.AppSettings.Get("ElMaxTestLimit")));
                }
                else
                {
                    throw new Exception("Trying to read config value, but it is null! The app might work incorrectly!!");
                }
            }
            catch (Exception ex)
            {
                _logger.log(ex.ToString());
                MessageBox.Show(ex.Message);
            }
        }

        void StartEventWatcher()
        {
            WqlEventQuery insertUSBQuery = new WqlEventQuery("SELECT * FROM __InstanceCreationEvent WITHIN 2 WHERE TargetInstance ISA " +
                "'Win32_PnPEntity'");
            ManagementEventWatcher insertUSBWatcher = new ManagementEventWatcher(insertUSBQuery);
            insertUSBWatcher.EventArrived += new EventArrivedEventHandler(DeviceInsertedEvent);
            insertUSBWatcher.Start();
            _logger.log("Detect of inserted device event started");

            WqlEventQuery removeUSBQuery = new WqlEventQuery("SELECT * FROM __InstanceDeletionEvent WITHIN 2 WHERE TargetInstance ISA " +
                "'Win32_PnPEntity'");
            ManagementEventWatcher removeUSBWatcher = new ManagementEventWatcher(removeUSBQuery);
            removeUSBWatcher.EventArrived += new EventArrivedEventHandler(DeviceRemovedEvent);
            removeUSBWatcher.Start();
            _logger.log("Detect of removed device event started");
        }

        void DeviceInsertedEvent(object sender, EventArrivedEventArgs e)
        {
            string vidPid = "";
            string name = "";
            string status = "";

            ManagementBaseObject instance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
            vidPid = (string)instance.GetPropertyValue("PNPDeviceID");
            name = (string)instance.GetPropertyValue("Name");
            status = (string)instance.GetPropertyValue("Status");
            _logger.log("Detected Device: " + name + ", Vid/Pid: " + vidPid + ", Status: " + status);
            if (status == "OK")
            {
                InsertChekAllIds(vidPid, name);
            }
        }

        void DeviceRemovedEvent(object sender, EventArrivedEventArgs e)
        {
            string vidPid;
            string name;

            ManagementBaseObject instance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
            vidPid = (string)instance.GetPropertyValue("PNPDeviceID");
            name = (string)instance.GetPropertyValue("Name");
            _logger.log("Removed device: " + name + ", Vid/Pid: " + vidPid);
            RemoveChekAllIds(vidPid, name);
        }

        bool InsertChekAllIds(string Vid_Pid, string Name)
        {
            bool returnValue = false;
            if (_flashDisk.InsertCheck(Vid_Pid, Name))
            {
                CheckTestStatus();
                _logger.log("FlashDisk detected");
                _flashDisk.lastTestResult = true;
                returnValue = true;
            }
            else if (_penDrive.InsertCheckIdAndName(Vid_Pid, Name))
            {
                CheckTestStatus();
                _logger.log("PenDrive detected");
                _penDrive.lastTestResult = true;
                returnValue = true;
            }
            else if (_hubController.InsertCheckIdAndName(Vid_Pid, Name))
            {
                CheckTestStatus();
                _logger.log("HubController detected");
                _hubController.lastTestResult = true;
                returnValue = true;
            }
            else if (_audioController.InsertCheckIdAndName(Vid_Pid, Name))
            {
                CheckTestStatus();
                _logger.log("AudioController detected");
                _audioController.lastTestResult = true;
                returnValue = true;
            }
            else if (_networkAdapter.InsertCheckIdAndName(Vid_Pid, Name))
            {
                CheckTestStatus();
                _logger.log("NetworkAdapter detected");
                _networkAdapter.lastTestResult = true;
                returnValue = true;
            }
            else if (_usbCompositeDevice.InsertCheckIdAndName(Vid_Pid, Name))
            {
                CheckTestStatus();
                _logger.log("UsbCompositeDevice detected");
                _usbCompositeDevice.lastTestResult = true;
                returnValue = true;
            }
            if (_penDrive.checkIfInserted() && _flashDisk.checkIfInserted() && _hubController.checkIfInserted() &&
                _usbCompositeDevice.checkIfInserted() && _audioController.checkIfInserted() && _networkAdapter.checkIfInserted())
            {
                // Perform limiter test
                startButton = en;
            }
            return returnValue;
        }

        void RemoveChekAllIds(string Vid_Pid, string Name)
        {
            if (_penDrive.RemoveCheckId(Vid_Pid))
            {
            }
            else if (_flashDisk.RemoveCheckId(Vid_Pid))
            {
            }
            else if (_hubController.RemoveCheckId(Vid_Pid))
            {
            }
            else if (_audioController.RemoveCheckId(Vid_Pid))
            {
            }
            else if (_networkAdapter.RemoveCheckId(Vid_Pid))
            {
            }
            else if (_usbCompositeDevice.RemoveCheckId(Vid_Pid))
            {
            }
            if (!_penDrive.checkIfInserted() && !_flashDisk.checkIfInserted() && !_hubController.checkIfInserted() &&
                !_usbCompositeDevice.checkIfInserted() && !_audioController.checkIfInserted() && !_networkAdapter.checkIfInserted())
            {
                TimerStop();
                testerStatus = ready;
                _logger.log(ready);
            }
        }

        void CheckTestStatus()
        {
            if (testerStatus != testInprogress)
            {
                StartTest();
            }
        }

        public void StartTest()
        {
            startButton = dis;
            testerStatus = testInprogress;
            testResult = clear;
            TimerStart(time);
            _logger.log("Running test sequence.");
        }

        public void ManualStartTest()
        {
            startButton = dis;
            testerStatus = testInprogress;
            testResult = clear;
            _logger.log("Test sequence started manually.");
        }

        public void TimerStart(int time)
        {
            _dispatcherTimer.Interval = new TimeSpan(0, 0, time);
            _dispatcherTimer.Start();
            _logger.log("Running timer.");
        }

        public void TimerStop()
        {
            if (_dispatcherTimer.IsEnabled)
            {
                _dispatcherTimer.Stop();
                _logger.log("Timer stopped.");
            }
        }

        private void DispatcherTimerTick(object sender, EventArgs e)
        {
            TimerStop();
            // Perform limiter test
            startButton = en;
        }

        private void StartTestClick(object obj)
        {
            tests.Clear();
            checkRequiredDevices();
            clearAllUSBLastTestResults();
            ThreadPool.QueueUserWorkItem(new WaitCallback(USBTest));
            ThreadPool.QueueUserWorkItem(new WaitCallback(PowerLimiterTest));

        }

        private void USBTest(object obj)
        {
            string vidPid = "";
            string name = "";
            string status = "";

            ManualStartTest();
            foreach (ManagementObject device in new ManagementObjectSearcher(String.Format(@"SELECT * FROM Win32_PNPEntity")).Get())
            {
                vidPid = (string)device.GetPropertyValue("PNPDeviceID");
                name = (string)device.GetPropertyValue("Name");
                status = (string)device.GetPropertyValue("Status");
                if (status == "OK")
                {
                    if(InsertChekAllIds(vidPid, name))
                    {
                        _logger.log(vidPid);
                        _logger.log(name);
                    }
                }
            }
            App.Current.Dispatcher.Invoke(new Action(() =>checkUSBTestResults()));
            checkIfFinishedAllTests();
        }

        bool checkRequiredDevices()
        {
            bool returnValue = false;

            _logger.logMultiple(_ktEL34143a.checkConnection());
            if (!_ktEL34143a.isConnected())
            {
                MessageBox.Show("Cannot find the Electronic load device!");
                elStatusColor = "Red";
                elStatus = "Disconnected";
                _numberOfTests = setNumberOfTests - 1;
            }
            else
            {
                elStatusColor = "Green";
                elStatus = "Connected";
                _numberOfTests = setNumberOfTests;
                returnValue = true;
            }
            return returnValue;
        }

        public void PassTest(int id, string name)
        {
            Test tmp = new Test { Id = id, Name = name, value = "", result = "" };
            if (tests.Contains(tmp))
            {
                tests.First(s => s.Name == tmp.Name).value = en;
                tests.First(s => s.Name == tmp.Name).result = pass;
            }
            else
            {
                tests.Add(new Test() { Id = id, Name = "name", value = en, result = pass });
            }
        }

        public void clearAllUSBLastTestResults()
        {
            _penDrive.clearLastTestResult();
            _flashDisk.clearLastTestResult();
            _hubController.clearLastTestResult();
            _usbCompositeDevice.clearLastTestResult();
            _audioController.clearLastTestResult();
            _networkAdapter.clearLastTestResult();
        }

        public void checkUSBTestResults()
        {
            if (_penDrive.lastTestResult == true && _hubController.lastTestResult == true && _flashDisk.lastTestResult == true)
            {
                tests.Add(new Test() { Id = 01, Name = "USB Controller Detection", value = en, result = pass });
            }
            else
            {
                tests.Add(new Test() { Id = 01, Name = "USB Controller Detection", value = dis, result = fail });
            }
            if (_audioController.lastTestResult == true && _usbCompositeDevice.lastTestResult == true)
            {
                tests.Add(new Test() { Id = 02, Name = "Sound Controller Detection", value = en, result = pass });
            }
            else
            {
                tests.Add(new Test() { Id = 02, Name = "Sound Controller Detection", value = dis, result = fail });
            }
            if (_networkAdapter.lastTestResult == true)
            {
                tests.Add(new Test() { Id = 03, Name = "Network Adapter Detection", value = en, result = pass });
            }
            else
            {
                tests.Add(new Test() { Id = 03, Name = "Network Adapter Detection", value = dis, result = fail });
            }
        }

        public void PowerLimiterTest(object obj)
        {
            double result;


            if (_ktEL34143a.isConnected())
            {
                _logger.logMultiple(_ktEL34143a.test());
                result = _ktEL34143a.getTestResultValue();
                if (_ktEL34143a.getTestResult())
                {
                    App.Current.Dispatcher.Invoke(new Action(() => { tests.Add(new Test() { Id = 04, Name = "Power Limiter Test", value = Convert.ToString(result), result = pass }); }));
                }
                else
                {
                    App.Current.Dispatcher.Invoke(new Action(() => { tests.Add(new Test() { Id = 04, Name = "Power Limiter Test", value = Convert.ToString(result), result = fail }); }));
                }
            }
            checkIfFinishedAllTests();
        }

        public void checkIfFinishedAllTests()
        {
            bool testsFinished = false;
            bool testsPassed = false;

            if (testerStatus == testInprogress)
            {
                if (tests.Count == _numberOfTests)
                {
                    testsFinished = true;
                    testsPassed = true;
                    for (int i = 0; i < _numberOfTests; i++)
                    {
                        if (tests.ElementAt(i).result == "")
                        {
                            testsFinished = false;
                        }
                        if (tests.ElementAt(i).result != pass)
                        {
                            testsPassed = false;
                        }
                    }
                    if (testsFinished)
                    {
                        startButton = en;
                        if (testsPassed)
                        {
                            testResult = pass;
                        }
                        else
                        {
                            testResult = fail;
                        }
                    }
                    testerStatus = testFinished;
                }
            }
        }
    }
}