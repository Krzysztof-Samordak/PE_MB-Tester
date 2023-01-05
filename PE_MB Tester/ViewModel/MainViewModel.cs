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

namespace PE_MB_Tester.MainViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        //USB Devices initialization
        FT232 _relayController = new FT232();
        Logger logger = new Logger();
        USBDevice penDrive = new USBDevice();
        USBDevice hubController = new USBDevice();
        USBDevice usbCompositeDevice = new USBDevice();
        USBDevice audioController = new USBDevice();
        USBDevice networkAdapter = new USBDevice();
        USBDevice flashDisk = new USBDevice();
        EL KtEL34143a = new EL();

        //Timer setup
        const int time = 10;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        //Auxiliary variables
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
        private string _startStopButton = en;
        private string _testerStatus;
        private string _testerStatusColor;
        private string _testResult;
        private string _testResultColor;
        private string _elStatusColor = "Red";
        private string _usbTestResult;
        private string _powerLimiterTestResult;

        //Create tests list
        List<Test> tests = new List<Test>();

        public ICommand startStopTestCommand { get; set; }

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
        public string startStopButton
        {
            get { return _startStopButton; }
            set
            {
                if (value == en || value == dis)
                {
                    _startStopButton = value;
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
        public string usbTestResult
        {
            get { return _usbTestResult; }
            set
            {
                if (value == pass)
                {
                    _usbTestResult = value;
                    OnPropertyChanged();
                }
                else if (value == fail)
                {
                    _usbTestResult = value;
                    OnPropertyChanged();
                }
                else if (value == clear)
                {
                    _usbTestResult = "";
                    OnPropertyChanged();
                }
            }
        }
        public string powerLimiterTestResult
        {
            get { return _powerLimiterTestResult; }
            set
            {
                if (value == pass)
                {
                    _powerLimiterTestResult = value;
                    OnPropertyChanged();
                }
                else if (value == fail)
                {
                    _powerLimiterTestResult = value;
                    OnPropertyChanged();
                }
                else if (value == clear)
                {
                    _powerLimiterTestResult = "";
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
            ReadConfig();

            startStopTestCommand = new RelayCommand(StartStopTestClick);

            //Create tests list
            tests.Add(new Test() { id = 0, name = "USB Controller Test", value = "", result = "" });
            tests.Add(new Test() { id = 1, name = "Power Limiter Test", value = "", result = "" });

            /*            logger.logMultiple(_relayController.FindDevices(), "Found FTDI devices: ");
                        logger.log("Required FTDI device is connected: " + (_relayController.Connect()).ToString());
                        logger.log("Bit_bang Mode on: " + (_relayController.SetBitBangMode()).ToString());
                        logger.log("Turned on power: " + (_relayController.TurnOnPin(31).ToString()));
                        _relayController.Read();
                        _relayController.TurnOffAllPins();
             */

            //setup Timer
            dispatcherTimer.Tick += DispatcherTimerTick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, time);

            //check for required devices
            checkRequiredDevices();

            //start WMI USB Devices watcher
            StartEventWatcher();

            //set the Test Setup to ready
            testerStatus = ready;
            logger.log(ready);
            logger.log(KtEL34143a._measuredValueMaxLimit.ToString());
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
                    penDrive.GetExpextedVid(ConfigurationManager.AppSettings.Get("PenDriveVid"));
                    penDrive.GetExpextedName(ConfigurationManager.AppSettings.Get("PenDriveName"));

                    hubController.GetExpextedVid(ConfigurationManager.AppSettings.Get("UsbHubControllerVid"));
                    hubController.GetExpextedName(ConfigurationManager.AppSettings.Get("UsbHubControllerName"));

                    audioController.GetExpextedVid(ConfigurationManager.AppSettings.Get("UsbAudioControllerVid"));
                    audioController.GetExpextedName(ConfigurationManager.AppSettings.Get("UsbAudioControllerName"));

                    usbCompositeDevice.GetExpextedVid(ConfigurationManager.AppSettings.Get("UsbCompositeDeviceVid"));
                    usbCompositeDevice.GetExpextedName(ConfigurationManager.AppSettings.Get("UsbCompositeDeviceName"));

                    networkAdapter.GetExpextedVid(ConfigurationManager.AppSettings.Get("NetworkAdapterVid"));
                    networkAdapter.GetExpextedName(ConfigurationManager.AppSettings.Get("NetworkAdapterName"));

                    flashDisk.GetExpextedVid(ConfigurationManager.AppSettings.Get("FlashDiskVid"));
                    flashDisk.GetExpextedName(ConfigurationManager.AppSettings.Get("FlashDiskName"));

                    KtEL34143a.setVisaAdress(ConfigurationManager.AppSettings.Get("ElVisaAdress"));
                    KtEL34143a.setControlAppName(ConfigurationManager.AppSettings.Get("ElControlAppName"));
                    KtEL34143a.setTestLimits(Convert.ToDouble(ConfigurationManager.AppSettings.Get("ElMinTestLimit")), Convert.ToDouble(ConfigurationManager.AppSettings.Get("ElMaxTestLimit")));
                }
                else
                {
                    throw new Exception("Trying to read config value, but it is null! The app might work incorrectly!!");
                }
            }
            catch (Exception ex)
            {
                logger.log(ex.ToString());
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
            logger.log("Detect of inserted device event started");

            WqlEventQuery removeUSBQuery = new WqlEventQuery("SELECT * FROM __InstanceDeletionEvent WITHIN 2 WHERE TargetInstance ISA " +
                "'Win32_PnPEntity'");
            ManagementEventWatcher removeUSBWatcher = new ManagementEventWatcher(removeUSBQuery);
            removeUSBWatcher.EventArrived += new EventArrivedEventHandler(DeviceRemovedEvent);
            removeUSBWatcher.Start();
            logger.log("Detect of removed device event started");
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
            logger.log("Detected Device: " + name + ", Vid/Pid: " + vidPid + ", Status: " + status);
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
            logger.log("Removed device: " + name + ", Vid/Pid: " + vidPid);
            RemoveChekAllIds(vidPid, name);
        }

        bool InsertChekAllIds(string Vid_Pid, string Name)
        {
            bool returnValue = false;
            if (flashDisk.InsertCheck(Vid_Pid, Name))
            {
                CheckTestStatus();
                logger.log("FlashDisk detected");
                returnValue = true;
                if (penDrive.checkIfInserted() && hubController.checkIfInserted())
                {
                }
            }
            else if (penDrive.InsertCheckIdAndName(Vid_Pid, Name))
            {
                CheckTestStatus();
                logger.log("PenDrive detected");
                returnValue = true;
                if (flashDisk.checkIfInserted() && hubController.checkIfInserted())
                {
                }
            }
            else if (hubController.InsertCheckIdAndName(Vid_Pid, Name))
            {
                CheckTestStatus();
                logger.log("HubController detected");
                returnValue = true;
                if (penDrive.checkIfInserted() && flashDisk.checkIfInserted())
                {

                }
            }
            else if (audioController.InsertCheckIdAndName(Vid_Pid, Name))
            {
                CheckTestStatus();
                logger.log("AudioController detected");
                returnValue = true;
                if (usbCompositeDevice.checkIfInserted())
                {
                }
            }
            else if (networkAdapter.InsertCheckIdAndName(Vid_Pid, Name))
            {
                CheckTestStatus();
                logger.log("NetworkAdapter detected");
                returnValue = true;
            }
            else if (usbCompositeDevice.InsertCheckIdAndName(Vid_Pid, Name))
            {
                CheckTestStatus();
                logger.log("UsbCompositeDevice detected");
                returnValue = true;
                if (audioController.checkIfInserted())
                {

                }
            }
            if (penDrive.checkIfInserted() && flashDisk.checkIfInserted() && hubController.checkIfInserted() &&
                usbCompositeDevice.checkIfInserted() && audioController.checkIfInserted() && networkAdapter.checkIfInserted())
            {
                UsbTestPass();
                // Perform limiter test
                FinishTest();
            }
            return returnValue;
        }

        void RemoveChekAllIds(string Vid_Pid, string Name)
        {
            if (penDrive.RemoveCheckId(Vid_Pid))
            {
            }
            else if (flashDisk.RemoveCheckId(Vid_Pid))
            {
            }
            else if (hubController.RemoveCheckId(Vid_Pid))
            {
            }
            else if (audioController.RemoveCheckId(Vid_Pid))
            {
            }
            else if (networkAdapter.RemoveCheckId(Vid_Pid))
            {
            }
            else if (usbCompositeDevice.RemoveCheckId(Vid_Pid))
            {
            }
            if (!penDrive.checkIfInserted() && !flashDisk.checkIfInserted() && !hubController.checkIfInserted() &&
                !usbCompositeDevice.checkIfInserted() && !audioController.checkIfInserted() && !networkAdapter.checkIfInserted())
            {
                TimerStop();
                testerStatus = ready;
                logger.log(ready);
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
            startStopButton = dis;
            testerStatus = testInprogress;
            testResult = clear;
            TimerStart(time);
            logger.log("Running test sequence.");
        }

        public void ManualStartTest()
        {
            startStopButton = dis;
            testerStatus = testInprogress;
            testResult = clear;
            logger.log("Test sequence started manually.");
        }

        public void FinishTest()
        {
            startStopButton = en;
        }

        public void TimerStart(int time)
        {
            dispatcherTimer.Interval = new TimeSpan(0, 0, time);
            dispatcherTimer.Start();
            logger.log("Running timer.");
        }

        public void TimerStop()
        {
            if (dispatcherTimer.IsEnabled)
            {
                dispatcherTimer.Stop();
                logger.log("Timer stopped.");
            }
        }

        private void DispatcherTimerTick(object sender, EventArgs e)
        {
            TimerStop();
            UsbTestFail();
            // Perform limiter test
            FinishTest();
        }

        public void UsbTestFail()
        {
            usbTestResult = fail;
            logger.log("USB Test FAIL!");// Zmien na USB test Result fail
            //testerStatus = testFinished;          Zmien na USB Test Finished
        }
        public void UsbTestPass()
        {
            TimerStop();
            logger.log("USB Test PASS!");
            usbTestResult = pass;                      // Zmien na USB test Result pass
//            testerStatus = testFinished;          Zmien na USB Test Finished
        }

        private void StartStopTestClick(object obj)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(StartStopTest));
        }

        private void StartStopTest(object obj)
        {
            string vidPid = "";
            string name = "";
            string status = "";
            double result;

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
                        logger.log(vidPid);
                        logger.log(name);
                    }
                }
            }
            if (testResult != pass)
            {
                //UsbTestFail();
            }
            if (checkRequiredDevices())
            {
                logger.logMultiple(KtEL34143a.test());
                if (KtEL34143a.getTestResult())
                {
                    // TEST PASS
                }
            }
            startStopButton = en;
        }

        bool checkRequiredDevices()
        {
            bool returnValue = false;

            logger.logMultiple(KtEL34143a.checkConnection());
            if (!KtEL34143a.isConnected())
            {
                MessageBox.Show("Cannot find the Electronic load device!");
                elStatusColor = "Red";
                elStatus = "Disconnected";
            }
            else
            {
                elStatusColor = "Green";
                elStatus = "Connected";
                returnValue = true;
            }
            return returnValue;
        }
    }
}