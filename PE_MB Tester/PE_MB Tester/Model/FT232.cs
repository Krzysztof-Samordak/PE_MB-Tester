/// <copyright>3Shape A/S</copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTD2XX_NET;
using System.Diagnostics;


using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;

    public class FT232
    {
        static FTDI _ftdi = new FTDI();
        static FTDI.FT_STATUS _ftStatus = FTDI.FT_STATUS.FT_OK;
        static UInt32 _bytesWritten = 0;
        string _requiredSerialNumber = "";
        byte[] _gpioState = new byte[1];             // TX, RX, RTS, CTS, DTR, DSR, DCD, RI   - pinout;
        bool _deviceConnectedFlag = false;

        public string TurnOnPin(byte bits)
        {
            _gpioState[0] = bits;
            _ftStatus = _ftdi.Write(_gpioState, _gpioState.Length, ref _bytesWritten);
            return _ftStatus.ToString();
        }
        public string TurnOffAllPins()
        {
            _gpioState[0] = 0;
            _ftStatus = _ftdi.Write(_gpioState, _gpioState.Length, ref _bytesWritten);
            return _ftStatus.ToString();
        }
        public string[] FindDevices()
        {
            string[] devices;
            UInt32 numDevices = 0;

            _ftdi.GetNumberOfDevices(ref numDevices);
            devices = new string[numDevices];
            var pDest = new FTD2XX_NET.FTDI.FT_DEVICE_INFO_NODE[numDevices];
            _ftdi.GetDeviceList(pDest);
            for (int i = 0; i < numDevices; i++)
            {
                devices[i] = (string.Format(pDest[i].SerialNumber));
                if (devices[i] == _requiredSerialNumber)
                {
                    _deviceConnectedFlag = true;
                }
            }
            return devices;
        }

        public string Connect()
        {
            _ftdi.SetBaudRate(921600);
            _ftStatus = FTDI.FT_STATUS.FT_DEVICE_NOT_FOUND;
            if (_deviceConnectedFlag == true)
            {
                _ftStatus = _ftdi.OpenBySerialNumber(_requiredSerialNumber);
            }
            return _ftStatus.ToString();
        }

        public string SetBitBangMode()
        {
            _ftStatus = FTDI.FT_STATUS.FT_DEVICE_NOT_FOUND;
            if (_deviceConnectedFlag == true)
            {
                if (_ftdi.IsOpen)
                {
                    _ftStatus = _ftdi.SetBitMode(255, 1);
                }
            }
            return _ftStatus.ToString();
        }

        public void setRequiredSerialNumber(string serialNumber)
        {
            _requiredSerialNumber = serialNumber;
        }

        public string Close()
        {
            string returnValue = "";
            _ftStatus = _ftdi.SetBitMode(0b11111111, 1);
            returnValue = _ftStatus.ToString();
            _ftStatus = _ftdi.Close();
            returnValue += _ftStatus.ToString();
            return returnValue;
        }

        public void Read()
        {

            byte bitMode = 0;
            Debug.WriteLine(_ftdi.GetPinStates(ref bitMode));
            Debug.WriteLine(bitMode);
        }
    }