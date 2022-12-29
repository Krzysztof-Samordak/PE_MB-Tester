/******************************************************
 *       Copyright Keysight Technologies 2021
 ******************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Resources;
using System.Text;
using System.Threading;
using MindWorks.Nimbus;
using Ivi.Driver;


[assembly: DriverCapabilities(Capabilities.AllExceptInterchangeCheck)]

[assembly: SCPICompliant(true)]
[assembly: BooleanCommand("1", "0")]

namespace Keysight.KtEL30000
{
    public sealed class KtEL30000 : Driver,
        IKtEL30000,
        IKtEL30000Acquisition, IKtEL30000Calibration,
        IKtEL30000Digital,
        IKtEL30000Display,
        IKtEL30000IOControl,
        IKtEL30000DataLog,
        IKtEL30000List,
        IKtEL30000Measurement,
        IKtEL30000Memory,
        IKtEL30000Operation,
        IKtEL30000OperationConstantCurrent,
        IKtEL30000OperationConstantCurrentProtection,
        IKtEL30000OperationConstantCurrentSlew,
        IKtEL30000OperationConstantPower,
        IKtEL30000OperationConstantPowerProtection,
        IKtEL30000OperationConstantPowerSlew,
        IKtEL30000OperationConstantResistance, IKtEL30000OperationConstantResistanceSlew,
        IKtEL30000OperationConstantVoltage, IKtEL30000OperationConstantVoltageSlew,
        IKtEL30000Status,
        IKtEL30000StatusOperation,
        IKtEL30000StatusQuestionable,
        IKtEL30000StatusStandardEvent,
        IKtEL30000Sweep,
        IIviDriver,
        IIviDriverOperation,
        IIviDriverIdentity,
        IIviDriverUtility,
        IKtEL30000System,
        IKtEL30000SystemTracing,
        IKtEL30000Transient,
        IKtEL30000Trigger,
        IKtEL30000TriggerAcquire,
        IKtEL30000TriggerDlog,
        IKtEL30000TriggerTransient
    {
        #region Properties to access cached references to the C++ API bridge class for each interface
        // BEGIN ROOFTOP-GENERATED CODE --- Do not edit or insert code in this region (including the enclosing #region/#endregion) ---
                    
        // CppApi - Initialized in the driver constructor
        internal Bridge.KtEL30000 CppApi = null;
        
        // Keysight.KtEL30000.KtEL30000Acquisition
        private Bridge.KtEL30000Acquisition cppApiAcquisition = null;
        internal Bridge.KtEL30000Acquisition CppApiAcquisition
        {
            get
            {
                if (cppApiAcquisition == null)
                    cppApiAcquisition = CppApi.Acquisition_;
                return cppApiAcquisition;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000Calibration
        private Bridge.KtEL30000Calibration cppApiCalibration = null;
        internal Bridge.KtEL30000Calibration CppApiCalibration
        {
            get
            {
                if (cppApiCalibration == null)
                    cppApiCalibration = CppApi.Calibration_;
                return cppApiCalibration;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000Digital
        private Bridge.KtEL30000Digital cppApiDigital = null;
        internal Bridge.KtEL30000Digital CppApiDigital
        {
            get
            {
                if (cppApiDigital == null)
                    cppApiDigital = CppApi.Digital_;
                return cppApiDigital;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000Display
        private Bridge.KtEL30000Display cppApiDisplay = null;
        internal Bridge.KtEL30000Display CppApiDisplay
        {
            get
            {
                if (cppApiDisplay == null)
                    cppApiDisplay = CppApi.Display_;
                return cppApiDisplay;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000IOControl
        private Bridge.KtEL30000IOControl cppApiIOControl = null;
        internal Bridge.KtEL30000IOControl CppApiIOControl
        {
            get
            {
                if (cppApiIOControl == null)
                    cppApiIOControl = CppApi.IOControl_;
                return cppApiIOControl;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000DataLog
        private Bridge.KtEL30000DataLog cppApiDataLog = null;
        internal Bridge.KtEL30000DataLog CppApiDataLog
        {
            get
            {
                if (cppApiDataLog == null)
                    cppApiDataLog = CppApi.DataLog_;
                return cppApiDataLog;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000List
        private Bridge.KtEL30000List cppApiList = null;
        internal Bridge.KtEL30000List CppApiList
        {
            get
            {
                if (cppApiList == null)
                    cppApiList = CppApi.List_;
                return cppApiList;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000Measurement
        private Bridge.KtEL30000Measurement cppApiMeasurement = null;
        internal Bridge.KtEL30000Measurement CppApiMeasurement
        {
            get
            {
                if (cppApiMeasurement == null)
                    cppApiMeasurement = CppApi.Measurement_;
                return cppApiMeasurement;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000Memory
        private Bridge.KtEL30000Memory cppApiMemory = null;
        internal Bridge.KtEL30000Memory CppApiMemory
        {
            get
            {
                if (cppApiMemory == null)
                    cppApiMemory = CppApi.Memory_;
                return cppApiMemory;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000Operation
        private Bridge.KtEL30000Operation cppApiOperation = null;
        internal Bridge.KtEL30000Operation CppApiOperation
        {
            get
            {
                if (cppApiOperation == null)
                    cppApiOperation = CppApi.Operation_;
                return cppApiOperation;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000OperationConstantCurrent
        private Bridge.KtEL30000OperationConstantCurrent cppApiOperationConstantCurrent = null;
        internal Bridge.KtEL30000OperationConstantCurrent CppApiOperationConstantCurrent
        {
            get
            {
                if (cppApiOperationConstantCurrent == null)
                    cppApiOperationConstantCurrent = CppApiOperation.ConstantCurrent_;
                return cppApiOperationConstantCurrent;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000OperationConstantCurrentProtection
        private Bridge.KtEL30000OperationConstantCurrentProtection cppApiOperationConstantCurrentProtection = null;
        internal Bridge.KtEL30000OperationConstantCurrentProtection CppApiOperationConstantCurrentProtection
        {
            get
            {
                if (cppApiOperationConstantCurrentProtection == null)
                    cppApiOperationConstantCurrentProtection = CppApiOperationConstantCurrent.Protection_;
                return cppApiOperationConstantCurrentProtection;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000OperationConstantCurrentSlew
        private Bridge.KtEL30000OperationConstantCurrentSlew cppApiOperationConstantCurrentSlew = null;
        internal Bridge.KtEL30000OperationConstantCurrentSlew CppApiOperationConstantCurrentSlew
        {
            get
            {
                if (cppApiOperationConstantCurrentSlew == null)
                    cppApiOperationConstantCurrentSlew = CppApiOperationConstantCurrent.Slew_;
                return cppApiOperationConstantCurrentSlew;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000OperationConstantPower
        private Bridge.KtEL30000OperationConstantPower cppApiOperationConstantPower = null;
        internal Bridge.KtEL30000OperationConstantPower CppApiOperationConstantPower
        {
            get
            {
                if (cppApiOperationConstantPower == null)
                    cppApiOperationConstantPower = CppApiOperation.ConstantPower_;
                return cppApiOperationConstantPower;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000OperationConstantPowerProtection
        private Bridge.KtEL30000OperationConstantPowerProtection cppApiOperationConstantPowerProtection = null;
        internal Bridge.KtEL30000OperationConstantPowerProtection CppApiOperationConstantPowerProtection
        {
            get
            {
                if (cppApiOperationConstantPowerProtection == null)
                    cppApiOperationConstantPowerProtection = CppApiOperationConstantPower.Protection_;
                return cppApiOperationConstantPowerProtection;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000OperationConstantPowerSlew
        private Bridge.KtEL30000OperationConstantPowerSlew cppApiOperationConstantPowerSlew = null;
        internal Bridge.KtEL30000OperationConstantPowerSlew CppApiOperationConstantPowerSlew
        {
            get
            {
                if (cppApiOperationConstantPowerSlew == null)
                    cppApiOperationConstantPowerSlew = CppApiOperationConstantPower.Slew_;
                return cppApiOperationConstantPowerSlew;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000OperationConstantResistance
        private Bridge.KtEL30000OperationConstantResistance cppApiOperationConstantResistance = null;
        internal Bridge.KtEL30000OperationConstantResistance CppApiOperationConstantResistance
        {
            get
            {
                if (cppApiOperationConstantResistance == null)
                    cppApiOperationConstantResistance = CppApiOperation.ConstantResistance_;
                return cppApiOperationConstantResistance;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000OperationConstantResistanceSlew
        private Bridge.KtEL30000OperationConstantResistanceSlew cppApiOperationConstantResistanceSlew = null;
        internal Bridge.KtEL30000OperationConstantResistanceSlew CppApiOperationConstantResistanceSlew
        {
            get
            {
                if (cppApiOperationConstantResistanceSlew == null)
                    cppApiOperationConstantResistanceSlew = CppApiOperationConstantResistance.Slew_;
                return cppApiOperationConstantResistanceSlew;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000OperationConstantVoltage
        private Bridge.KtEL30000OperationConstantVoltage cppApiOperationConstantVoltage = null;
        internal Bridge.KtEL30000OperationConstantVoltage CppApiOperationConstantVoltage
        {
            get
            {
                if (cppApiOperationConstantVoltage == null)
                    cppApiOperationConstantVoltage = CppApiOperation.ConstantVoltage_;
                return cppApiOperationConstantVoltage;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000OperationConstantVoltageSlew
        private Bridge.KtEL30000OperationConstantVoltageSlew cppApiOperationConstantVoltageSlew = null;
        internal Bridge.KtEL30000OperationConstantVoltageSlew CppApiOperationConstantVoltageSlew
        {
            get
            {
                if (cppApiOperationConstantVoltageSlew == null)
                    cppApiOperationConstantVoltageSlew = CppApiOperationConstantVoltage.Slew_;
                return cppApiOperationConstantVoltageSlew;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000Status
        private Bridge.KtEL30000Status cppApiStatus = null;
        internal Bridge.KtEL30000Status CppApiStatus
        {
            get
            {
                if (cppApiStatus == null)
                    cppApiStatus = CppApi.Status_;
                return cppApiStatus;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000StatusOperation
        private Bridge.KtEL30000StatusOperation cppApiStatusOperation = null;
        internal Bridge.KtEL30000StatusOperation CppApiStatusOperation
        {
            get
            {
                if (cppApiStatusOperation == null)
                    cppApiStatusOperation = CppApiStatus.Operation_;
                return cppApiStatusOperation;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000StatusQuestionable
        private Bridge.KtEL30000StatusQuestionable cppApiStatusQuestionable = null;
        internal Bridge.KtEL30000StatusQuestionable CppApiStatusQuestionable
        {
            get
            {
                if (cppApiStatusQuestionable == null)
                    cppApiStatusQuestionable = CppApiStatus.Questionable_;
                return cppApiStatusQuestionable;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000StatusStandardEvent
        private Bridge.KtEL30000StatusStandardEvent cppApiStatusStandardEvent = null;
        internal Bridge.KtEL30000StatusStandardEvent CppApiStatusStandardEvent
        {
            get
            {
                if (cppApiStatusStandardEvent == null)
                    cppApiStatusStandardEvent = CppApiStatus.StandardEvent_;
                return cppApiStatusStandardEvent;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000Sweep
        private Bridge.KtEL30000Sweep cppApiSweep = null;
        internal Bridge.KtEL30000Sweep CppApiSweep
        {
            get
            {
                if (cppApiSweep == null)
                    cppApiSweep = CppApi.Sweep_;
                return cppApiSweep;
            }
        }
        
        // Ivi.Driver.IviDriverOperation
        private Bridge.IviDriverOperation cppApiDriverOperation = null;
        internal Bridge.IviDriverOperation CppApiDriverOperation
        {
            get
            {
                if (cppApiDriverOperation == null)
                    cppApiDriverOperation = CppApi.DriverOperation_;
                return cppApiDriverOperation;
            }
        }
        
        // Ivi.Driver.IviDriverUtility
        private Bridge.IviDriverUtility cppApiUtility = null;
        internal Bridge.IviDriverUtility CppApiUtility
        {
            get
            {
                if (cppApiUtility == null)
                    cppApiUtility = CppApi.Utility_;
                return cppApiUtility;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000System
        private Bridge.KtEL30000System cppApiSystem = null;
        internal Bridge.KtEL30000System CppApiSystem
        {
            get
            {
                if (cppApiSystem == null)
                    cppApiSystem = CppApi.System_;
                return cppApiSystem;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000SystemTracing
        private Bridge.KtEL30000SystemTracing cppApiSystemTracing = null;
        internal Bridge.KtEL30000SystemTracing CppApiSystemTracing
        {
            get
            {
                if (cppApiSystemTracing == null)
                    cppApiSystemTracing = CppApiSystem.Tracing_;
                return cppApiSystemTracing;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000Transient
        private Bridge.KtEL30000Transient cppApiTransient = null;
        internal Bridge.KtEL30000Transient CppApiTransient
        {
            get
            {
                if (cppApiTransient == null)
                    cppApiTransient = CppApi.Transient_;
                return cppApiTransient;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000Trigger
        private Bridge.KtEL30000Trigger cppApiTrigger = null;
        internal Bridge.KtEL30000Trigger CppApiTrigger
        {
            get
            {
                if (cppApiTrigger == null)
                    cppApiTrigger = CppApi.Trigger_;
                return cppApiTrigger;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000TriggerAcquire
        private Bridge.KtEL30000TriggerAcquire cppApiTriggerAcquire = null;
        internal Bridge.KtEL30000TriggerAcquire CppApiTriggerAcquire
        {
            get
            {
                if (cppApiTriggerAcquire == null)
                    cppApiTriggerAcquire = CppApiTrigger.Acquire_;
                return cppApiTriggerAcquire;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000TriggerDlog
        private Bridge.KtEL30000TriggerDlog cppApiTriggerDlog = null;
        internal Bridge.KtEL30000TriggerDlog CppApiTriggerDlog
        {
            get
            {
                if (cppApiTriggerDlog == null)
                    cppApiTriggerDlog = CppApiTrigger.Dlog_;
                return cppApiTriggerDlog;
            }
        }
        
        // Keysight.KtEL30000.KtEL30000TriggerTransient
        private Bridge.KtEL30000TriggerTransient cppApiTriggerTransient = null;
        internal Bridge.KtEL30000TriggerTransient CppApiTriggerTransient
        {
            get
            {
                if (cppApiTriggerTransient == null)
                    cppApiTriggerTransient = CppApiTrigger.Transient_;
                return cppApiTriggerTransient;
            }
        }
            
        // END ROOFTOP-GENERATED CODE --- Do not edit or insert code in this region (including the enclosing #region/#endregion) ---
        #endregion
        
        #region Constructors

        public KtEL30000(string resourceName, bool idQuery, bool reset)
            : this(resourceName, idQuery, reset, String.Empty)
        {
        }

        public KtEL30000(string resourceName, bool idQuery, bool reset, string options)
        {
            // CppAPI constructor
            CppApi = new Bridge.KtEL30000(resourceName, idQuery, reset, options);

            // Do not remove this call to base.Initialize.  Note: reset parameter set to false as it is handled by the CppAPI constructor.
            base.Initialize(resourceName, idQuery, false, options, LockType.AppDomain, String.Empty, 2000, 2000, 5000); // TODO: set times as appropriate for instrument
                                                                                                                        // ..., IO,  Reset, SelfTest); timeouts in milliseconds
            FinalConstruct();
        }

        public KtEL30000(string resourceName, bool idQuery, bool reset, LockType lockType, string accessKey, string options)
        {
            // CppAPI constructor
            CppApi = new Bridge.KtEL30000(resourceName, idQuery, reset, options);

            // Do not remove this call to base.Initialize
            base.Initialize(resourceName, idQuery, reset, options, lockType, accessKey, 2000, 2000, 5000); // TODO: set times as appropriate for instrument
                                                                                                           // ..., IO,  Reset, SelfTest); timeouts in milliseconds
            FinalConstruct();
        }

        protected override void CleanupUnmanagedResources()
        {
            // Use this method to cleanup any driver-specific unmanaged resources
            if (CppApi != null)
                CppApi.Dispose();
        }

        #endregion

        #region Nimbus Overrides

        /// <summary>
        /// Initializes an instrument I/O communication session. 
        /// </summary>
        protected override void InitializeIO()
        {
            return; // Nothing to do here.  CppAPI/CMI is created in constructor.
        }

        internal override void CloseIO()
        {
            if (CppApi != null)
                CppApi.Close();
        }

        /// <summary>
        /// Clears the instrument I/O session of any errors that may exist.  
        /// </summary>
        protected override void ClearIO()
        {
            // This method clears the status of the instrument and removes any errors that may have occurred before
            // the driver was initialized.  This improves the ability of the driver to initialize when the instrument
            // is already in an error state.  In 488.2-compliant devices, this method sends the *CLS command.

            //CppApiSystem.ClearIO();
            return;  // Do nothing here as CppAPI/CMI constructor does this.
        }

        /// <summary>
        /// This method communicates with the instrument to determine and store basic instrument characteristics, such as the instrument manufacturer, model,
        /// serial number, and firmware revision.
        /// </summary>
        protected override void InitializeIdentification()
        {
            // This method retrieves basic information about the instrument and stores it in the session.  In 
            // 488.2-compliance devices, this method parses the results of the *IDN? query.
            //
            this.Session.InstrumentManufacturer = CppApi.Identity_.InstrumentManufacturer;
            this.Session.InstrumentModel = CppApi.Identity_.InstrumentModel;
            this.Session.InstrumentSerialNumber = CppApiSystem.SerialNumber;
            this.Session.InstrumentFirmwareRevision = CppApi.Identity_.InstrumentFirmwareRevision;

            return;
        }

        /// <summary>
        /// If QueryInstrStatus is enabled for the driver session, this method communicates with the instrument to determine
        /// if the previous operation resulted in an instrument error.  An InstrumentStatusException is thrown if an error 
        /// occurred. 
        /// </summary>
        protected override void PollInstrumentErrors()
        {
            if (!this.Session.InitOptions.QueryInstrumentStatusEnabled || this.Session.InitOptions.SimulationEnabled)
            {
                // User has not enabled polling or is running in simulation mode, so just return without doing anything.
                return;
            }

            // This method queries the status system of the instrument to determine if an error occurred.  In 
            // 488.2-compliant devices, this method performs a *ESR? query.
            //
            return;  // Do nothing here as CMI I/O code implements PollInstrumentErrors();
        }

        /// <summary>
        /// Process a custom DriverSetup option.  Custom DriverSetup options are driver-defined name-value pairs that must be
        /// processed by this method.  Custom DriverSetup options do *not* include those options common to all Nimbus drivers, such as the "Model" and
        /// "Trace" options.  This method is only called when Nimbus cannot interpret a DriverSetup option.
        /// </summary>
        /// <param name="name">Name of the custom DriverSetup option.</param>
        /// <param name="value">Value of the custom DriverSetup option.</param>
        internal override void ProcessCustomDriverSetupOption(string name, string value)
        {
            if (name.ToLower() == "udplogenabled" ||
                 name.ToLower() == "udpaddress" ||
                name.ToLower() == "tracearraysizemax")
            {
                // Do nothing, these valid options are processed by the CppApi constructor
                return;
            }

            throw ErrorService.UnknownOption(name);
        }
        #endregion

        #region Helper Methods and Variables

        // Private class member variables and functions used internally by the driver.  For example:
        // internal int _simMyVar = 123;


        internal void ResetVars()
        {
            // Resets any driver member variables to initial settings.
            // Called by the IviDriver.Utility.Reset and ResetWithDefaults methods.
            // _simMyVar = 123;
        }

        internal void FinalConstruct()
        {
            // Called from driver constructors.  Use for any additonal initialization tasks as needed.

            // TODO: Initialize any selector style repcap selctor properties to first instance.
            // For example:
            // Marker.ActiveMarker = "Marker1";
        }

        public static Exception TranslateException(string what)
        {
            Exception ex;

            // Translate known CppApi/CMI/RCL/ScpiIoSupport exceptions to IVI.NET equivalents.
            // Instrument specific exceptions must be configured to call this method in KtEL30000Properties.xml <ExceptionPropagation> section.
            // Add additional exception message mapping here as needed.

            // IVI Inherent exceptions
            if (what.Contains("Instrument error"))
                ex = new Ivi.Driver.InstrumentStatusException(what);
            else if (what.Contains("Instrument ID query failed"))
                ex = new Ivi.Driver.IdQueryFailedException(what);
            else if (what.Contains("Unknown resource"))
                ex = new Ivi.Driver.IOException(what);
            else if (what.ToLower().Contains("timeout error") || what.ToLower().Contains("timeout occurred"))
                ex = new Ivi.Driver.IOTimeoutException(what);
            else if (what.Contains("Physical name '' invalid"))
                ex = new Ivi.Driver.SelectorNameRequiredException(what);
            else if (what.Contains("Repeated Capability selector") || what.Contains("already exist in RepCap") ||
                     what.Contains("Must only contain characters") || what.Contains("custom added physical name"))
                ex = new Ivi.Driver.SelectorNameException(what);
            else if (what.ToLower().Contains("physical name") || what.Contains("Repeated Capability name"))
                ex = new Ivi.Driver.UnknownPhysicalNameException(what);
            else if (what.ToLower().Contains("out of range") || what.ToLower().Contains("invalid range") || what.ToLower().Contains("must be greater than 0"))
                ex = new Ivi.Driver.OutOfRangeException(what);
            else if (what.Contains("No response"))
                ex = new Ivi.Driver.UnexpectedResponseException(what);
            else if (what.ToLower().Contains("invalid argument") || what.ToLower().Contains("invalid value") || what.ToLower().Contains("value not supported"))
                ex = new Ivi.Driver.ValueNotSupportedException(what);
            else if (what.ToLower().Contains("not supported"))
                ex = new Ivi.Driver.OperationNotSupportedException(what);

            // Instrument specific exceptions
            //else if (what.Contains("My exception message"))
            //    ex = new Keysight.KtEL30000.MyException(what);

            else // Default if none of the above
                ex = new Ivi.Driver.IOException(what);

            return ex;
        }

        #endregion


        // IviDriver Inherent Interfaces
        #region IIviDriver

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        public IIviDriverOperation DriverOperation
        {
            get
            {
                return base.DefaultImplementation.Inherent.DriverOperation;
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        public IIviDriverIdentity Identity
        {
            get
            {
                return base.DefaultImplementation.Inherent.Identity;
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        public IIviDriverUtility Utility
        {
            get
            {
                return base.DefaultImplementation.Inherent.Utility;
            }
        }

        [DriverMethod(SuppressPollInstrumentErrors = true)]
        public void Close()
        {
            base.DefaultImplementation.Inherent.Close();
        }

        #endregion

        #region IIviComponentIdentity

        [DriverProperty(Lock = false, SuppressPollInstrumentErrors = true)]
        string IIviComponentIdentity.Description
        {
            get
            {
                return base.DefaultImplementation.Inherent.Description;
            }
        }

        [DriverProperty(Lock = false, SuppressPollInstrumentErrors = true)]
        string IIviComponentIdentity.Revision
        {
            get
            {
                return base.DefaultImplementation.Inherent.Revision;
            }
        }

        [DriverProperty(Lock = false, SuppressPollInstrumentErrors = true)]
        string IIviComponentIdentity.Vendor
        {
            get
            {
                return base.DefaultImplementation.Inherent.Vendor;
            }
        }

        #endregion

        #region IIviDriverIdentity

        [DriverProperty(Lock = false, SuppressPollInstrumentErrors = true)]
        string IIviDriverIdentity.InstrumentManufacturer
        {
            get
            {
                return base.DefaultImplementation.Inherent.InstrumentManufacturer;
            }
        }

        [DriverProperty(Lock = false, SuppressPollInstrumentErrors = true)]
        string IIviDriverIdentity.InstrumentModel
        {
            get
            {
                return base.DefaultImplementation.Inherent.InstrumentModel;
            }
        }

        [DriverProperty(Lock = false, SuppressPollInstrumentErrors = true)]
        string IIviDriverIdentity.InstrumentFirmwareRevision
        {
            get
            {
                return base.DefaultImplementation.Inherent.InstrumentFirmwareRevision;
            }
        }

        [DriverProperty(Lock = false, SuppressPollInstrumentErrors = true)]
        string IIviDriverIdentity.Identifier
        {
            get
            {
                return base.DefaultImplementation.Inherent.Identifier;
            }
        }

        [DriverProperty(Lock = false, SuppressPollInstrumentErrors = true)]
        int IIviDriverIdentity.SpecificationMajorVersion
        {
            get
            {
                return base.DefaultImplementation.Inherent.SpecificationMajorVersion;
            }
        }

        [DriverProperty(Lock = false, SuppressPollInstrumentErrors = true)]
        int IIviDriverIdentity.SpecificationMinorVersion
        {
            get
            {
                return base.DefaultImplementation.Inherent.SpecificationMinorVersion;
            }
        }

        [DriverMethod(SuppressPollInstrumentErrors = true)]
        string[] IIviDriverIdentity.GetSupportedInstrumentModels()
        {
            return base.DefaultImplementation.Inherent.GetSupportedInstrumentModels();
        }

        [DriverMethod(SuppressPollInstrumentErrors = true)]
        string[] IIviDriverIdentity.GetGroupCapabilities()
        {
            return base.DefaultImplementation.Inherent.GetGroupCapabilities();
        }

        #endregion

        #region IIviDriverOperation

        [DriverEvent(SuppressPollInstrumentErrors = true)]
        event EventHandler<WarningEventArgs> IIviDriverOperation.Warning
        {
            add
            {
                base.Warning += value;
            }
            remove
            {
                base.Warning -= value;
            }
        }

        [DriverEvent(SuppressPollInstrumentErrors = true)]
        event EventHandler<CoercionEventArgs> IIviDriverOperation.Coercion
        {
            add
            {
                base.Coercion += value;
            }
            remove
            {
                base.Coercion -= value;
            }
        }

        [DriverEvent(SuppressPollInstrumentErrors = true)]
        event EventHandler<InterchangeCheckWarningEventArgs> IIviDriverOperation.InterchangeCheckWarning
        {
            add
            {
                throw ErrorService.OperationNotSupported("IIviDriverOperation", "InterchangeCheckWarning");
                //base.InterchangeCheckWarning += value;
            }
            remove
            {
                throw ErrorService.OperationNotSupported("IIviDriverOperation", "InterchangeCheckWarning");
                //base.InterchangeCheckWarning -= value;
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        string IIviDriverOperation.LogicalName
        {
            get
            {
                return base.DefaultImplementation.Inherent.LogicalName;
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        string IIviDriverOperation.IOResourceDescriptor
        {
            get
            {
                //return base.DefaultImplementation.Inherent.IOResourceDescriptor;
                return CppApiDriverOperation.GetIOResourceDescriptor();

            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        bool IIviDriverOperation.Cache
        {
            get
            {
                return base.DefaultImplementation.Inherent.Cache;
            }
            set
            {
                base.DefaultImplementation.Inherent.Cache = value;
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        bool IIviDriverOperation.QueryInstrumentStatus
        {
            get
            {
                //return base.DefaultImplementation.Inherent.QueryInstrumentStatus;
                return CppApiDriverOperation.GetQueryInstrumentStatus();

            }
            set
            {
                base.DefaultImplementation.Inherent.QueryInstrumentStatus = value;
                CppApiDriverOperation.SetQueryInstrumentStatus(value);
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        bool IIviDriverOperation.RangeCheck
        {
            get
            {
                return base.DefaultImplementation.Inherent.RangeCheck;
            }
            set
            {
                base.DefaultImplementation.Inherent.RangeCheck = value;
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        bool IIviDriverOperation.Simulate
        {
            get
            {
                return base.DefaultImplementation.Inherent.Simulate;
            }
            set
            {
                // Nimbus only allows setting simulation from false to true.
                base.DefaultImplementation.Inherent.Simulate = value;
                CppApiDriverOperation.SetSimulate(value);
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        string IIviDriverOperation.DriverSetup
        {
            get
            {
                //return base.DefaultImplementation.Inherent.DriverSetup;
                return CppApiDriverOperation.GetDriverSetup();
            }
        }

        [DriverMethod(SuppressPollInstrumentErrors = true)]
        void IIviDriverOperation.ResetInterchangeCheck()
        {
            base.DefaultImplementation.Inherent.ResetInterchangeCheck();
        }

        [DriverMethod(SuppressPollInstrumentErrors = true)]
        void IIviDriverOperation.InvalidateAllAttributes()
        {
            base.DefaultImplementation.Inherent.InvalidateAllAttributes();
        }

        #endregion

        #region IIviDriverUtility

        [DriverMethod(SuppressPollInstrumentErrors = true)]
        ErrorQueryResult IIviDriverUtility.ErrorQuery()
        {
            return CppApiUtility.ErrorQuery();
        }

        [DriverMethod(SuppressPollInstrumentErrors = true)]
        SelfTestResult IIviDriverUtility.SelfTest()
        {
            return CppApiUtility.SelfTest();
        }

        [DriverMethod(SuppressPollInstrumentErrors = true)]
        void IIviDriverUtility.Disable()
        {
            CppApiUtility.Disable();
        }

        [DriverMethod(SuppressPollInstrumentErrors = true)]
        void IIviDriverUtility.ResetWithDefaults()
        {
            CppApiUtility.ResetWithDefaults();
            ResetVars(); // Reset any driver member variables
        }

        [DriverMethod(SuppressPollInstrumentErrors = true)]
        void IIviDriverUtility.Reset()
        {
            CppApiUtility.Reset();
            ResetVars(); // Reset any driver member variables
        }

        [DriverMethod(Lock = false, SuppressPollInstrumentErrors = true)]
        IIviDriverLock IIviDriverUtility.Lock()
        {
            return base.DefaultImplementation.Inherent.Lock();
        }

        [DriverMethod(Lock = false, SuppressPollInstrumentErrors = true)]
        IIviDriverLock IIviDriverUtility.Lock(PrecisionTimeSpan maxTime)
        {
            return base.DefaultImplementation.Inherent.Lock(maxTime);
        }

        #endregion


        // KtEL30000 Instrument Specific Interfaces
        #region IKtEL30000

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        public IKtEL30000System System
        {
            get
            {
                return this;
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        public IKtEL30000Status Status
        {
            get
            {
                return this;
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        public IKtEL30000Trigger Trigger
        {
            get
            {
                return this;
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        public IKtEL30000Memory Memory
        {
            get
            {
                return this;
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        public IKtEL30000Transient Transient
        {
            get
            {
                return this;
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        public IKtEL30000Operation Operation
        {
            get
            {
                return this;
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        public IKtEL30000Calibration Calibration
        {
            get
            {
                return this;
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        public IKtEL30000Display Display
        {
            get
            {
                return this;
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        public IKtEL30000Acquisition Acquisition
        {
            get
            {
                return this;
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        public IKtEL30000Measurement Measurement
        {
            get
            {
                return this;
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        public IKtEL30000List List
        {
            get
            {
                return this;
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        public IKtEL30000Digital Digital
        {
            get
            {
                return this;
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        public IKtEL30000DataLog DataLog
        {
            get
            {
                return this;
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        public IKtEL30000Sweep Sweep
        {
            get
            {
                return this;
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        public IKtEL30000IOControl IOControl
        {
            get
            {
                return this;
            }
        }

        #endregion

        #region IKtEL30000Status Members

        [DriverMethod]
        void IKtEL30000Status.Clear()
        {
            // Rooftop generated
            CppApiStatus.Clear();
        }

        [DriverMethod]
        void IKtEL30000Status.Preset()
        {
            // Rooftop generated
            CppApiStatus.Preset();
        }

        [DriverMethod]
        StatusByteFlags IKtEL30000Status.SerialPoll()
        {
            // Rooftop generated
            return CppApiStatus.SerialPoll();
        }

        [DriverEvent]
        event EventHandler<ServiceRequestEventArgs> IKtEL30000Status.ServiceRequestEvent
        {
            add
            {
                CppApiStatus.ServiceRequestEventAdapter.Add(value);      // Rooftop generated
            }
            remove
            {
                CppApiStatus.ServiceRequestEventAdapter.Remove(value);   // Rooftop generated
            }
        }

        [DriverMethod]
        void IKtEL30000Status.DisableServiceRequestEvents()
        {
            // Rooftop generated
            CppApiStatus.DisableServiceRequestEvents();
        }

        [DriverMethod]
        void IKtEL30000Status.EnableServiceRequestEvents()
        {
            // Rooftop generated
            CppApiStatus.EnableServiceRequestEvents();
        }

        [DriverMethod]
        Keysight.KtEL30000.StatusByteFlags IKtEL30000Status.ReadStatusByteRegister()
        {
            // Rooftop generated
            return CppApiStatus.ReadStatusByteRegister();
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        Keysight.KtEL30000.StatusByteFlags IKtEL30000Status.ServiceRequestEnableRegister
        {
            get
            {
                return CppApiStatus.ServiceRequestEnableRegister;      // Rooftop generated
            }
            set
            {
                CppApiStatus.ServiceRequestEnableRegister = value;     // Rooftop generated
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        IKtEL30000StatusOperation IKtEL30000Status.Operation
        {
            get
            {
                return this;
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        IKtEL30000StatusQuestionable IKtEL30000Status.Questionable
        {
            get
            {
                return this;
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        IKtEL30000StatusStandardEvent IKtEL30000Status.StandardEvent
        {
            get
            {
                return this;
            }
        }

        #endregion

        #region IKtEL30000StatusOperation Members

        [DriverMethod]
        Keysight.KtEL30000.StatusOperationFlags IKtEL30000StatusOperation.ReadConditionRegister()
        {
            // Rooftop generated
            return CppApiStatusOperation.ReadConditionRegister();
        }

        [DriverMethod]
        StatusOperationFlags IKtEL30000StatusOperation.ReadEventRegister()
        {
            // Rooftop generated
            return CppApiStatusOperation.ReadEventRegister();
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        Keysight.KtEL30000.StatusOperationFlags IKtEL30000StatusOperation.EnableRegister
        {
            get
            {
                return CppApiStatusOperation.EnableRegister;      // Rooftop generated
            }
            set
            {
                CppApiStatusOperation.EnableRegister = value;     // Rooftop generated
            }
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        Keysight.KtEL30000.StatusOperationFlags IKtEL30000StatusOperation.NegativeTransitionFilter
        {
            get
            {
                return CppApiStatusOperation.NegativeTransitionFilter;      // Rooftop generated
            }
            set
            {
                CppApiStatusOperation.NegativeTransitionFilter = value;     // Rooftop generated
            }
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        Keysight.KtEL30000.StatusOperationFlags IKtEL30000StatusOperation.PositiveTransitionFilter
        {
            get
            {
                return CppApiStatusOperation.PositiveTransitionFilter;      // Rooftop generated
            }
            set
            {
                CppApiStatusOperation.PositiveTransitionFilter = value;     // Rooftop generated
            }
        }

        #endregion

        #region IKtEL30000StatusQuestionable Members

        [DriverMethod]
        StatusQuestionableFlags IKtEL30000StatusQuestionable.ReadConditionRegister()
        {
            // Rooftop generated
            return CppApiStatusQuestionable.ReadConditionRegister();
        }

        [DriverMethod]
        StatusQuestionableFlags IKtEL30000StatusQuestionable.ReadEventRegister()
        {
            // Rooftop generated
            return CppApiStatusQuestionable.ReadEventRegister();
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        StatusQuestionableFlags IKtEL30000StatusQuestionable.EnableRegister
        {
            get
            {
                return CppApiStatusQuestionable.EnableRegister;      // Rooftop generated
            }
            set
            {
                CppApiStatusQuestionable.EnableRegister = value;     // Rooftop generated
            }
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        StatusQuestionableFlags IKtEL30000StatusQuestionable.PositiveTransitionFilter
        {
            get
            {
                return CppApiStatusQuestionable.PositiveTransitionFilter;      // Rooftop generated
            }
            set
            {
                CppApiStatusQuestionable.PositiveTransitionFilter = value;     // Rooftop generated
            }
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        StatusQuestionableFlags IKtEL30000StatusQuestionable.NegativeTransitionFilter
        {
            get
            {
                return CppApiStatusQuestionable.NegativeTransitionFilter;      // Rooftop generated
            }
            set
            {
                CppApiStatusQuestionable.NegativeTransitionFilter = value;     // Rooftop generated
            }
        }

        #endregion

        #region IKtEL30000StatusStandardEvent Members

        [DriverMethod]
        Keysight.KtEL30000.StatusStandardEventFlags IKtEL30000StatusStandardEvent.ReadEventRegister()
        {
            // Rooftop generated
            return CppApiStatusStandardEvent.ReadEventRegister();
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        Keysight.KtEL30000.StatusStandardEventFlags IKtEL30000StatusStandardEvent.EnableRegister
        {
            get
            {
                return CppApiStatusStandardEvent.EnableRegister;      // Rooftop generated
            }
            set
            {
                CppApiStatusStandardEvent.EnableRegister = value;     // Rooftop generated
            }
        }

        #endregion

        #region IKtEL30000System

        [DriverMethod]
        void IKtEL30000System.RecallState(int state)
        {
            CppApiSystem.RecallState(state);
        }

        [DriverMethod]
        void IKtEL30000System.SaveState(int state)
        {
            CppApiSystem.SaveState(state);
        }

        [DriverProperty(Lock = false, SuppressPollInstrumentErrors = true)]

        string IKtEL30000System.SerialNumber
        {
            get
            {
                return this.Session.InstrumentSerialNumber;
            }

        }

        [DriverMethod]
        void IKtEL30000System.ClearIO()
        {
            // Rooftop generated
            CppApiSystem.ClearIO();
        }

        [DriverMethod(SuppressPollInstrumentErrors = true)]
        byte[] IKtEL30000System.ReadBytes()
        {
            if (this.Session.InitOptions.SimulationEnabled)
            {
                return Encoding.ASCII.GetBytes("Driver in simulation mode.");
                //throw ErrorService.OperationNotAvailableInSimulationMode("ReadBytes");
            }

            return CppApiSystem.ReadBytes();
        }

        [DriverMethod(SuppressPollInstrumentErrors = true)]
        string IKtEL30000System.ReadString()
        {
            if (this.Session.InitOptions.SimulationEnabled)
            {
                return "Driver in simulation mode.";
                //throw ErrorService.OperationNotAvailableInSimulationMode("ReadString");
            }

            return CppApiSystem.ReadString();
        }

        [DriverMethod]
        void IKtEL30000System.WaitForOperationComplete(TimeSpan maxTime)
        {
            // Rooftop generated
            //CppApiSystem.WaitForOperationComplete(maxTime);
            CppApiSystem.WaitForOperationComplete(new PrecisionTimeSpan(maxTime));  // TODO: had to convert to PTS...
        }

        [DriverMethod]
        void IKtEL30000System.WriteBytes(byte[] buffer)
        {
            // Rooftop generated
            CppApiSystem.WriteBytes(buffer);
        }

        [DriverMethod]
        void IKtEL30000System.WriteString(string data)
        {
            // Rooftop generated
            CppApiSystem.WriteString(data);
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        PrecisionTimeSpan IKtEL30000System.IOTimeout
        {
            get
            {
                return CppApiSystem.IOTimeout;      // Rooftop generated
            }
            set
            {
                CppApiSystem.IOTimeout = value;     // Rooftop generated
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        IKtEL30000SystemTracing IKtEL30000System.Tracing
        {
            get
            {
                return this;
            }
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        Keysight.KtEL30000.PowerOnStateMode IKtEL30000System.PowerOnStateMode
        {
            get
            {
                return CppApiSystem.PowerOnStateMode;      // Rooftop generated
            }
            set
            {
                CppApiSystem.PowerOnStateMode = value;     // Rooftop generated
            }
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        bool IKtEL30000System.BeepEnabled
        {
            get
            {
                return CppApiSystem.BeepEnabled;      // Rooftop generated
            }
            set
            {
                CppApiSystem.BeepEnabled = value;     // Rooftop generated
            }
        }

        [DriverMethod]
        void IKtEL30000System.SetBeep()
        {
            // Rooftop generated
            CppApiSystem.SetBeep();
        }

        #endregion

        #region IKtEL30000SystemTracing Members

        [DriverMethod]
        void IKtEL30000SystemTracing.Flush()
        {
            // Rooftop generated
            CppApiSystemTracing.Flush();
        }

        [DriverMethod]
        void IKtEL30000SystemTracing.Write(string message)
        {
            // Rooftop generated
            CppApiSystemTracing.Write(message);
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        bool IKtEL30000SystemTracing.Enabled
        {
            get
            {
                return CppApiSystemTracing.Enabled;      // Rooftop generated
            }
            set
            {
                CppApiSystemTracing.Enabled = value;     // Rooftop generated
            }
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        string IKtEL30000SystemTracing.FileName
        {
            get
            {
                return CppApiSystemTracing.FileName;      // Rooftop generated
            }
            set
            {
                CppApiSystemTracing.FileName = value;     // Rooftop generated
            }
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        Keysight.KtEL30000.TracingLevel IKtEL30000SystemTracing.Level
        {
            get
            {
                return CppApiSystemTracing.Level;      // Rooftop generated
            }
            set
            {
                CppApiSystemTracing.Level = value;     // Rooftop generated
            }
        }

        #endregion
        #region IKtEL30000Operation

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        IKtEL30000OperationConstantCurrent IKtEL30000Operation.ConstantCurrent
        {
            get
            {
                return this;
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        IKtEL30000OperationConstantVoltage IKtEL30000Operation.ConstantVoltage
        {
            get
            {
                return this;
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        IKtEL30000OperationConstantResistance IKtEL30000Operation.ConstantResistance
        {
            get
            {
                return this;
            }
        }

        [DriverMethod]
        void IKtEL30000Operation.ConfigureMode(Keysight.KtEL30000.OperationMode mode, string channelList)
        {
            // Rooftop generated
            CppApiOperation.ConfigureMode(mode, channelList);
        }

        [DriverMethod]
        OperationMode[] IKtEL30000Operation.GetConfigurationMode(string channelList)
        {
            // Rooftop generated
            return CppApiOperation.GetConfigurationMode(channelList);
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        IKtEL30000OperationConstantPower IKtEL30000Operation.ConstantPower
        {
            get
            {
                return this;
            }
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        [NotSupported(Models.EL34143A, Models.EL33133A)]
        Keysight.KtEL30000.PairMode IKtEL30000Operation.PairMode
        {
            get
            {
                return CppApiOperation.PairMode;      // Rooftop generated
            }
            set
            {
                CppApiOperation.PairMode = value;     // Rooftop generated
            }
        }

        [DriverMethod]
        void IKtEL30000Operation.ClearProtection(string channelList)
        {
            // Rooftop generated
            CppApiOperation.ClearProtection(channelList);
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        bool IKtEL30000Operation.InputShortEnabled
        {
            get
            {
                return CppApiOperation.InputShortEnabled;      // Rooftop generated
            }
            set
            {
                CppApiOperation.InputShortEnabled = value;     // Rooftop generated
            }
        }

        #endregion

        #region IKtEL30000Calibration

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        int IKtEL30000Calibration.SecureCode
        {
            set
            {
                CppApiCalibration.SecureCode = value;     // Rooftop generated
            }
        }

        [DriverMethod]
        void IKtEL30000Calibration.SetSecureCodeEnabled(bool state, string passCode)
        {
            // Rooftop generated
            CppApiCalibration.SetSecureCodeEnabled(state, passCode);
        }

        [DriverMethod]
        bool IKtEL30000Calibration.GetSecureStateEnabled()
        {
            // Rooftop generated
            return CppApiCalibration.GetSecureStateEnabled();
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        string IKtEL30000Calibration.Message
        {
            get
            {
                return CppApiCalibration.Message;      // Rooftop generated
            }
            set
            {
                CppApiCalibration.Message = value;     // Rooftop generated
            }
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        bool IKtEL30000Calibration.AutoSaveEnabled
        {
            get
            {
                return CppApiCalibration.AutoSaveEnabled;      // Rooftop generated
            }
            set
            {
                CppApiCalibration.AutoSaveEnabled = value;     // Rooftop generated
            }
        }

        [DriverMethod]
        int IKtEL30000Calibration.GetCount()
        {
            // Rooftop generated
            return CppApiCalibration.GetCount();
        }

        [DriverMethod]
        void IKtEL30000Calibration.SetInputCurrentLowLevel(Keysight.KtEL30000.MinMaxDefMode currentLevel, string channelList)
        {
            // Rooftop generated
            CppApiCalibration.SetInputCurrentLowLevel(currentLevel, channelList);
        }

        [DriverMethod]
        void IKtEL30000Calibration.SetInputCurrentLowData(double current, string channelList)
        {
            // Rooftop generated
            CppApiCalibration.SetInputCurrentLowData(current, channelList);
        }

        [DriverMethod]
        void IKtEL30000Calibration.SetInputVoltageLowLevel(Keysight.KtEL30000.MinMaxDefMode voltageLevel, string channelList)
        {
            // Rooftop generated
            CppApiCalibration.SetInputVoltageLowLevel(voltageLevel, channelList);
        }

        [DriverMethod]
        void IKtEL30000Calibration.SetCurrentLimitLevel(Keysight.KtEL30000.MinMaxDefMode currentLimitLevel, string channelList)
        {
            // Rooftop generated
            CppApiCalibration.SetCurrentLimitLevel(currentLimitLevel, channelList);
        }

        [DriverMethod]
        void IKtEL30000Calibration.SetInputVoltageLowData(double voltage, string channelList)
        {
            // Rooftop generated
            CppApiCalibration.SetInputVoltageLowData(voltage, channelList);
        }

        [DriverMethod]
        void IKtEL30000Calibration.SetCurrentLimitData(double limitCurrentData, string channelList)
        {
            // Rooftop generated
            CppApiCalibration.SetCurrentLimitData(limitCurrentData, channelList);
        }

        [DriverMethod]
        void IKtEL30000Calibration.SetInputVoltageHighData(double voltage, string channelList)
        {
            // Rooftop generated
            CppApiCalibration.SetInputVoltageHighData(voltage, channelList);
        }

        [DriverMethod]
        void IKtEL30000Calibration.SetInputCurrentMediumLevel(Keysight.KtEL30000.MinMaxDefMode currentLevel, string channelList)
        {
            // Rooftop generated
            CppApiCalibration.SetInputCurrentMediumLevel(currentLevel, channelList);
        }

        [DriverMethod]
        void IKtEL30000Calibration.SetInputCurrentHighLevel(Keysight.KtEL30000.CalibrationCurrentLevel currentLevel, string channelList)
        {
            // Rooftop generated
            CppApiCalibration.SetInputCurrentHighLevel(currentLevel, channelList);
        }

        [DriverMethod]
        void IKtEL30000Calibration.SetInputCurrentMediumData(double current, string channelList)
        {
            // Rooftop generated
            CppApiCalibration.SetInputCurrentMediumData(current, channelList);
        }

        [DriverMethod]
        void IKtEL30000Calibration.SetInputCurrentHighData(double current, string channelList)
        {
            // Rooftop generated
            CppApiCalibration.SetInputCurrentHighData(current, channelList);
        }

        [DriverMethod]
        void IKtEL30000Calibration.SetInputVoltageHighLevel(Keysight.KtEL30000.MinMaxDefMode voltageLevel, string channelList)
        {
            // Rooftop generated
            CppApiCalibration.SetInputVoltageHighLevel(voltageLevel, channelList);
        }

        [DriverMethod]
        void IKtEL30000Calibration.Save()
        {
            // Rooftop generated
            CppApiCalibration.Save();
        }

        #endregion

        #region IKtEL30000Display

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        bool IKtEL30000Display.Enabled
        {
            get
            {
                return CppApiDisplay.Enabled;      // Rooftop generated
            }
            set
            {
                CppApiDisplay.Enabled = value;     // Rooftop generated
            }
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        string IKtEL30000Display.Text
        {
            get
            {
                return CppApiDisplay.Text;      // Rooftop generated
            }
            set
            {
                CppApiDisplay.Text = value;     // Rooftop generated
            }
        }

        [DriverMethod]
        void IKtEL30000Display.ClearText()
        {
            // Rooftop generated
            CppApiDisplay.ClearText();
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        [NotSupported(Models.EL33133A, Models.EL34143A)]
        Keysight.KtEL30000.ViewMeter IKtEL30000Display.View
        {
            get
            {
                return CppApiDisplay.View;      // Rooftop generated
            }
            set
            {
                CppApiDisplay.View = value;     // Rooftop generated
            }
        }

        #endregion

        #region IKtEL30000Measurement

        [DriverMethod]
        double[] IKtEL30000Measurement.Fetch(Keysight.KtEL30000.MeasurementFunction measurementFunction, Keysight.KtEL30000.MeasurementType measurementType, string channelList)
        {
            // Rooftop generated
            return CppApiMeasurement.Fetch(measurementFunction, measurementType, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000Measurement.Measure(Keysight.KtEL30000.MeasurementFunction measurementFunction, Keysight.KtEL30000.MeasurementType measurementType, string channelList)
        {
            // Rooftop generated
            return CppApiMeasurement.Measure(measurementFunction, measurementType, channelList);
        }

        [DriverMethod]
        void IKtEL30000Measurement.SetVoltageMeasurementEnabled(bool voltageMeasurementEnabled, string channelList)
        {
            // Rooftop generated
            CppApiMeasurement.SetVoltageMeasurementEnabled(voltageMeasurementEnabled, channelList);
        }

        [DriverMethod]
        bool[] IKtEL30000Measurement.GetVoltageMeasurementEnabled(string channelList)
        {
            // Rooftop generated
            var returnVal = default(Bridge.BooleanVector);   // return value

            returnVal = CppApiMeasurement.GetVoltageMeasurementEnabled(channelList);

            return returnVal.ToArray();   // return value
        }

        [DriverMethod]
        bool[] IKtEL30000Measurement.GetCurrentMeasurementEnabled(string channelList)
        {
            // Rooftop generated
            var returnVal = default(Bridge.BooleanVector);   // return value

            returnVal = CppApiMeasurement.GetCurrentMeasurementEnabled(channelList);

            return returnVal.ToArray();   // return value
        }

        [DriverMethod]
        void IKtEL30000Measurement.SetCurrentMeasurementEnabled(bool currentMeasurementEnabled, string channelList)
        {
            // Rooftop generated
            CppApiMeasurement.SetCurrentMeasurementEnabled(currentMeasurementEnabled, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000Measurement.Fetch(Keysight.KtEL30000.MeasurementFunction measurementFunction, string channelList)
        {
            // Rooftop generated
            return CppApiMeasurement.Fetch(measurementFunction, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000Measurement.Measure(Keysight.KtEL30000.MeasurementFunction measurementFunction, string channelList)
        {
            // Rooftop generated
            return CppApiMeasurement.Measure(measurementFunction, channelList);
        }

        #endregion

        #region IKtEL30000Acquisition

        [DriverMethod]
        void IKtEL30000Acquisition.Abort(string channelList)
        {
            // Rooftop generated
            CppApiAcquisition.Abort(channelList);
        }

        [DriverMethod]
        void IKtEL30000Acquisition.Initiate(string channelList)
        {
            // Rooftop generated
            CppApiAcquisition.Initiate(channelList);
        }

        #endregion

        #region IKtEL30000List

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        void IKtEL30000List.SetCount(int count, string channelList)
        {
            // Rooftop generated
            CppApiList.SetCount(count, channelList);
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        void IKtEL30000List.SetCount(Keysight.KtEL30000.MinMaxDefMode count, string channelList)
        {
            // Rooftop generated
            CppApiList.SetCount(count, channelList);
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        int[] IKtEL30000List.GetCount(Keysight.KtEL30000.MinMaxDefMode count, string channelList)
        {
            // Rooftop generated
            return CppApiList.GetCount(count, channelList);
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        int[] IKtEL30000List.GetCount(string channelList)
        {
            // Rooftop generated
            return CppApiList.GetCount(channelList);
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        double[] IKtEL30000List.GetLevel(Keysight.KtEL30000.OperationMode mode, string channelList)
        {
            // Rooftop generated
            return CppApiList.GetLevel(mode, channelList);
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        void IKtEL30000List.SetLevel(Keysight.KtEL30000.OperationMode mode, double[] levelList, string channelList)
        {
            // Rooftop generated
            CppApiList.SetLevel(mode, levelList, channelList);
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        int[] IKtEL30000List.GetNumberOfPoints(Keysight.KtEL30000.OperationMode mode, string channelList)
        {
            // Rooftop generated
            return CppApiList.GetNumberOfPoints(mode, channelList);
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        void IKtEL30000List.SetDwellTime(PrecisionTimeSpan[] dwellTimeList, string channelList)
        {
            // Rooftop generated
            CppApiList.SetDwellTime(dwellTimeList, channelList);
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        PrecisionTimeSpan[] IKtEL30000List.GetDwellTime(string channelList)
        {
            // Rooftop generated
            return CppApiList.GetDwellTime(channelList);
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        int[] IKtEL30000List.GetNumberOfDwellPoints(string channelList)
        {
            // Rooftop generated
            return CppApiList.GetNumberOfDwellPoints(channelList);
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        void IKtEL30000List.SetTerminationModeEnabled(bool lastStepValueEnabled, string channelList)
        {

            CppApiList.SetTerminationModeEnabled(lastStepValueEnabled, channelList);
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        bool[] IKtEL30000List.GetTerminationModeEnabled(string channelList)
        {
            // Rooftop generated
            var returnVal = default(Bridge.BooleanVector);   // return value

            returnVal = CppApiList.GetTerminationModeEnabled(channelList);

            return returnVal.ToArray();   // return value
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        void IKtEL30000List.SetMode(Keysight.KtEL30000.AutoMode mode, string channelList)
        {
            // Rooftop generated
            CppApiList.SetMode(mode, channelList);
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        AutoMode[] IKtEL30000List.GetMode(string channelList)
        {
            // Rooftop generated
            return CppApiList.GetMode(channelList);
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        void IKtEL30000List.SetTriggerSignalOutputEnabled(bool[] triggerSignalOutputEnabled, Keysight.KtEL30000.ListTriggerSignalOutputMode listTriggerSignalOutputMode, string channelList)
        {
            var triggerSignalOutputEnabledBridge = new Bridge.BooleanVector(triggerSignalOutputEnabled);   // 'in' parameter
            CppApiList.SetTriggerSignalOutputEnabled(triggerSignalOutputEnabledBridge, listTriggerSignalOutputMode, channelList);
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        bool[] IKtEL30000List.GetTriggerSignalOutputEnabled(Keysight.KtEL30000.ListTriggerSignalOutputMode listTriggerSignalOutputMode, string channelList)
        {
            // Rooftop generated
            var returnVal = default(Bridge.BooleanVector);   // return value

            returnVal = CppApiList.GetTriggerSignalOutputEnabled(listTriggerSignalOutputMode, channelList);

            return returnVal.ToArray();   // return value
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        int[] IKtEL30000List.GetTriggerSignalOutputNumberOfPoint(Keysight.KtEL30000.ListTriggerSignalOutputMode listTriggerSignalOutputMode, string channelList)
        {
            // Rooftop generated
            return CppApiList.GetTriggerSignalOutputNumberOfPoint(listTriggerSignalOutputMode, channelList);
        }

        #endregion
        #region IKtEL30000OperationConstantCurrent

        [DriverMethod]
        double[] IKtEL30000OperationConstantCurrent.GetCurrent(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantCurrent.GetCurrent(channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantCurrent.SetTriggerLevel(Keysight.KtEL30000.MinMaxDefMode triggerLevel, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantCurrent.SetTriggerLevel(triggerLevel, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantCurrent.GetTriggerLevel(Keysight.KtEL30000.MinMaxDefMode triggerLevel, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantCurrent.GetTriggerLevel(triggerLevel, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantCurrent.GetTriggerLevel(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantCurrent.GetTriggerLevel(channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantCurrent.SetTriggerLevel(double triggerLevel, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantCurrent.SetTriggerLevel(triggerLevel, channelList);
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        IKtEL30000OperationConstantCurrentProtection IKtEL30000OperationConstantCurrent.Protection
        {
            get
            {
                return this;
            }
        }

        [DriverMethod]
        void IKtEL30000OperationConstantCurrent.SetMode(Keysight.KtEL30000.CurrentMode mode, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantCurrent.SetMode(mode, channelList);
        }

        [DriverMethod]
        CurrentMode[] IKtEL30000OperationConstantCurrent.GetMode(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantCurrent.GetMode(channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantCurrent.GetCurrent(Keysight.KtEL30000.MinMaxDefMode currentLevel, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantCurrent.GetCurrent(currentLevel, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantCurrent.SetCurrent(Keysight.KtEL30000.MinMaxDefMode currentValue, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantCurrent.SetCurrent(currentValue, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantCurrent.SetCurrent(double currentValue, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantCurrent.SetCurrent(currentValue, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantCurrent.SetCurrentRange(Keysight.KtEL30000.MinMaxDefMode range, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantCurrent.SetCurrentRange(range, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantCurrent.GetCurrentRange(Keysight.KtEL30000.MinMaxDefMode range, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantCurrent.GetCurrentRange(range, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantCurrent.GetCurrentRange(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantCurrent.GetCurrentRange(channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantCurrent.SetCurrentRange(double range, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantCurrent.SetCurrentRange(range, channelList);
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        IKtEL30000OperationConstantCurrentSlew IKtEL30000OperationConstantCurrent.Slew
        {
            get
            {
                return this;
            }
        }

        #endregion

        #region IKtEL30000OperationConstantCurrentProtection

        [DriverMethod]
        void IKtEL30000OperationConstantCurrentProtection.SetEnabled(bool protectionEnabled, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantCurrentProtection.SetEnabled(protectionEnabled, channelList);
        }

        [DriverMethod]
        bool[] IKtEL30000OperationConstantCurrentProtection.GetEnabled(string channelList)
        {
            // Rooftop generated
            var returnVal = default(Bridge.BooleanVector);   // return value

            returnVal = CppApiOperationConstantCurrentProtection.GetEnabled(channelList);

            return returnVal.ToArray();   // return value
        }

        [DriverMethod]
        void IKtEL30000OperationConstantCurrentProtection.SetOCPDelayTimerMode(Keysight.KtEL30000.OverCurrentProtectionDelayTimerMode mode, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantCurrentProtection.SetOCPDelayTimerMode(mode, channelList);
        }

        [DriverMethod]
        OverCurrentProtectionDelayTimerMode[] IKtEL30000OperationConstantCurrentProtection.GetOCPDelayTimerMode(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantCurrentProtection.GetOCPDelayTimerMode(channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantCurrentProtection.SetOCPDelayTime(Keysight.KtEL30000.MinMaxDefMode time, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantCurrentProtection.SetOCPDelayTime(time, channelList);
        }

        [DriverMethod]
        PrecisionTimeSpan[] IKtEL30000OperationConstantCurrentProtection.GetOCPDelayTime(Keysight.KtEL30000.MinMaxDefMode time, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantCurrentProtection.GetOCPDelayTime(time, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantCurrentProtection.SetOCPDelayTime(PrecisionTimeSpan time, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantCurrentProtection.SetOCPDelayTime(time, channelList);
        }

        [DriverMethod]
        PrecisionTimeSpan[] IKtEL30000OperationConstantCurrentProtection.GetOCPDelayTime(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantCurrentProtection.GetOCPDelayTime(channelList);
        }

        [DriverMethod]
        bool IKtEL30000OperationConstantCurrentProtection.IsTripped()
        {
            // Rooftop generated
            return CppApiOperationConstantCurrentProtection.IsTripped();
        }

        #endregion

        #region IKtEL30000OperationConstantPower

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        IKtEL30000OperationConstantPowerProtection IKtEL30000OperationConstantPower.Protection
        {
            get
            {
                return this;
            }
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantPower.GetPower(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantPower.GetPower(channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantPower.GetPower(Keysight.KtEL30000.MinMaxDefMode powerLevel, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantPower.GetPower(powerLevel, channelList);
        }

        [DriverMethod]
        PowerMode[] IKtEL30000OperationConstantPower.GetMode(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantPower.GetMode(channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantPower.GetTriggerLevel(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantPower.GetTriggerLevel(channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantPower.GetTriggerLevel(Keysight.KtEL30000.MinMaxDefMode triggerLevel, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantPower.GetTriggerLevel(triggerLevel, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantPower.SetPower(double powerLevel, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantPower.SetPower(powerLevel, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantPower.SetPower(Keysight.KtEL30000.MinMaxDefMode powerLevel, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantPower.SetPower(powerLevel, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantPower.SetMode(Keysight.KtEL30000.PowerMode mode, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantPower.SetMode(mode, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantPower.SetTriggerLevel(Keysight.KtEL30000.MinMaxDefMode triggerLevel, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantPower.SetTriggerLevel(triggerLevel, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantPower.SetTriggerLevel(double triggerLevel, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantPower.SetTriggerLevel(triggerLevel, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantPower.SetPowerRange(double range, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantPower.SetPowerRange(range, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantPower.SetPowerRange(Keysight.KtEL30000.MinMaxDefMode range, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantPower.SetPowerRange(range, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantPower.GetPowerRange(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantPower.GetPowerRange(channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantPower.GetPowerRange(Keysight.KtEL30000.MinMaxDefMode range, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantPower.GetPowerRange(range, channelList);
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        IKtEL30000OperationConstantPowerSlew IKtEL30000OperationConstantPower.Slew
        {
            get
            {
                return this;
            }
        }

        #endregion
        #region IKtEL30000OperationConstantVoltage

        [DriverMethod]
        double[] IKtEL30000OperationConstantVoltage.GetVoltage(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantVoltage.GetVoltage(channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantVoltage.GetVoltage(Keysight.KtEL30000.MinMaxDefMode levelValue, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantVoltage.GetVoltage(levelValue, channelList);
        }

        [DriverMethod]
        VoltageMode[] IKtEL30000OperationConstantVoltage.GetMode(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantVoltage.GetMode(channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantVoltage.GetTriggerLevel(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantVoltage.GetTriggerLevel(channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantVoltage.GetTriggerLevel(Keysight.KtEL30000.MinMaxDefMode levelValue, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantVoltage.GetTriggerLevel(levelValue, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantVoltage.SetVoltage(double voltageValue, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantVoltage.SetVoltage(voltageValue, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantVoltage.SetVoltage(Keysight.KtEL30000.MinMaxDefMode voltageValue, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantVoltage.SetVoltage(voltageValue, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantVoltage.SetMode(Keysight.KtEL30000.VoltageMode mode, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantVoltage.SetMode(mode, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantVoltage.SetTriggerLevel(Keysight.KtEL30000.MinMaxDefMode triggerLevel, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantVoltage.SetTriggerLevel(triggerLevel, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantVoltage.SetTriggerLevel(double triggerLevel, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantVoltage.SetTriggerLevel(triggerLevel, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantVoltage.GetCurrentLimit(Keysight.KtEL30000.MinMaxDefMode currentLimit, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantVoltage.GetCurrentLimit(currentLimit, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantVoltage.SetCurrentLimit(Keysight.KtEL30000.MinMaxDefMode currentLimit, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantVoltage.SetCurrentLimit(currentLimit, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantVoltage.SetCurrentLimit(double currentLimit, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantVoltage.SetCurrentLimit(currentLimit, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantVoltage.GetCurrentLimit(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantVoltage.GetCurrentLimit(channelList);
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        IKtEL30000OperationConstantVoltageSlew IKtEL30000OperationConstantVoltage.Slew
        {
            get
            {
                return this;
            }
        }

        [DriverMethod]
        void IKtEL30000OperationConstantVoltage.SetVoltageRange(double range, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantVoltage.SetVoltageRange(range, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantVoltage.SetVoltageRange(Keysight.KtEL30000.MinMaxDefMode range, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantVoltage.SetVoltageRange(range, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantVoltage.GetVoltageRange(Keysight.KtEL30000.MinMaxDefMode range, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantVoltage.GetVoltageRange(range, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantVoltage.GetVoltageRange(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantVoltage.GetVoltageRange(channelList);
        }

        [DriverMethod]
        SenseMode[] IKtEL30000OperationConstantVoltage.GetVoltageSensingMode(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantVoltage.GetVoltageSensingMode(channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantVoltage.SetVoltageSensingMode(Keysight.KtEL30000.SenseMode senseMode, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantVoltage.SetVoltageSensingMode(senseMode, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantVoltage.SetInhibitVoltageTurnOnLevel(Keysight.KtEL30000.MinMaxDefMode inhibitVoltageTurnOnLevel, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantVoltage.SetInhibitVoltageTurnOnLevel(inhibitVoltageTurnOnLevel, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantVoltage.GetInhibitVoltageTurnOnLevel(Keysight.KtEL30000.MinMaxDefMode inhibitVoltageTurnOnLevel, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantVoltage.GetInhibitVoltageTurnOnLevel(inhibitVoltageTurnOnLevel, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantVoltage.SetInhibitVoltageTurnOnLevel(double inhibitVoltageTurnOnLevel, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantVoltage.SetInhibitVoltageTurnOnLevel(inhibitVoltageTurnOnLevel, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantVoltage.GetInhibitVoltageTurnOnLevel(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantVoltage.GetInhibitVoltageTurnOnLevel(channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantVoltage.SetInhibitVoltageTurnOnMode(Keysight.KtEL30000.InhibitMode inhibitVoltageTurnOnMode, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantVoltage.SetInhibitVoltageTurnOnMode(inhibitVoltageTurnOnMode, channelList);
        }

        [DriverMethod]
        InhibitMode[] IKtEL30000OperationConstantVoltage.GetInhibitVoltageTurnOnMode(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantVoltage.GetInhibitVoltageTurnOnMode(channelList);
        }

        #endregion
        #region IKtEL30000OperationConstantResistance

        [DriverMethod]
        double[] IKtEL30000OperationConstantResistance.GetResistance(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantResistance.GetResistance(channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantResistance.GetResistance(Keysight.KtEL30000.MinMaxDefMode levelValue, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantResistance.GetResistance(levelValue, channelList);
        }

        [DriverMethod]
        ResistanceMode[] IKtEL30000OperationConstantResistance.GetMode(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantResistance.GetMode(channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantResistance.GetTriggerLevel(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantResistance.GetTriggerLevel(channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantResistance.GetTriggerLevel(Keysight.KtEL30000.MinMaxDefMode triggerLevel, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantResistance.GetTriggerLevel(triggerLevel, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantResistance.SetResistance(Keysight.KtEL30000.MinMaxDefMode resistanceValue, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantResistance.SetResistance(resistanceValue, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantResistance.SetResistance(double resistanceValue, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantResistance.SetResistance(resistanceValue, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantResistance.SetTriggerLevel(double triggerLevel, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantResistance.SetTriggerLevel(triggerLevel, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantResistance.SetTriggerLevel(Keysight.KtEL30000.MinMaxDefMode triggerLevel, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantResistance.SetTriggerLevel(triggerLevel, channelList);
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        IKtEL30000OperationConstantResistanceSlew IKtEL30000OperationConstantResistance.Slew
        {
            get
            {
                return this;
            }
        }

        [DriverMethod]
        void IKtEL30000OperationConstantResistance.SetResistanceRange(Keysight.KtEL30000.MinMaxDefMode range, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantResistance.SetResistanceRange(range, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantResistance.SetResistanceRange(double range, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantResistance.SetResistanceRange(range, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantResistance.GetResistanceRange(Keysight.KtEL30000.MinMaxDefMode range, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantResistance.GetResistanceRange(range, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantResistance.GetResistanceRange(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantResistance.GetResistanceRange(channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantResistance.SetMode(Keysight.KtEL30000.ResistanceMode mode, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantResistance.SetMode(mode, channelList);
        }

        #endregion
        #region IKtEL30000OperationConstantPowerProtection

        [DriverMethod]
        bool[] IKtEL30000OperationConstantPowerProtection.GetEnabled(string channelList)
        {
            // Rooftop generated
            var returnVal = default(Bridge.BooleanVector);   // return value

            returnVal = CppApiOperationConstantPowerProtection.GetEnabled(channelList);

            return returnVal.ToArray();   // return value
        }

        [DriverMethod]
        PrecisionTimeSpan[] IKtEL30000OperationConstantPowerProtection.GetOPPDelayTime(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantPowerProtection.GetOPPDelayTime(channelList);
        }

        [DriverMethod]
        PrecisionTimeSpan[] IKtEL30000OperationConstantPowerProtection.GetOPPDelayTime(Keysight.KtEL30000.MinMaxDefMode delayTime, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantPowerProtection.GetOPPDelayTime(delayTime, channelList);
        }

        [DriverMethod]
        bool IKtEL30000OperationConstantPowerProtection.IsTripped()
        {
            // Rooftop generated
            return CppApiOperationConstantPowerProtection.IsTripped();
        }

        [DriverMethod]
        void IKtEL30000OperationConstantPowerProtection.SetEnabled(bool enabled, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantPowerProtection.SetEnabled(enabled, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantPowerProtection.SetOPPDelayTime(Keysight.KtEL30000.MinMaxDefMode delayTime, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantPowerProtection.SetOPPDelayTime(delayTime, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantPowerProtection.SetOPPDelayTime(PrecisionTimeSpan delayTime, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantPowerProtection.SetOPPDelayTime(delayTime, channelList);
        }

        #endregion


        #region IKtEL30000Transient

        [DriverMethod]
        void IKtEL30000Transient.SetMode(Keysight.KtEL30000.TransientMode transientMode, string channelList)
        {
            // Rooftop generated
            CppApiTransient.SetMode(transientMode, channelList);
        }

        [DriverMethod]
        TransientMode[] IKtEL30000Transient.GetMode(string channelList)
        {
            // Rooftop generated
            return CppApiTransient.GetMode(channelList);
        }

        [DriverMethod]
        void IKtEL30000Transient.SetFrequency(Keysight.KtEL30000.MinMaxDefMode frequency, string channelList)
        {
            // Rooftop generated
            CppApiTransient.SetFrequency(frequency, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000Transient.GetFrequency(Keysight.KtEL30000.MinMaxDefMode frequency, string channelList)
        {
            // Rooftop generated
            return CppApiTransient.GetFrequency(frequency, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000Transient.GetFrequency(string channelList)
        {
            // Rooftop generated
            return CppApiTransient.GetFrequency(channelList);
        }

        [DriverMethod]
        void IKtEL30000Transient.SetFrequency(double frequency, string channelList)
        {
            // Rooftop generated
            CppApiTransient.SetFrequency(frequency, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000Transient.GetDutyCycle(Keysight.KtEL30000.MinMaxDefMode dutyCycle, string channelList)
        {
            // Rooftop generated
            return CppApiTransient.GetDutyCycle(dutyCycle, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000Transient.GetDutyCycle(string channelList)
        {
            // Rooftop generated
            return CppApiTransient.GetDutyCycle(channelList);
        }

        [DriverMethod]
        void IKtEL30000Transient.SetDutyCycle(Keysight.KtEL30000.MinMaxDefMode dutyCycle, string channelList)
        {
            // Rooftop generated
            CppApiTransient.SetDutyCycle(dutyCycle, channelList);
        }

        [DriverMethod]
        void IKtEL30000Transient.SetDutyCycle(double dutyCycle, string channelList)
        {
            // Rooftop generated
            CppApiTransient.SetDutyCycle(dutyCycle, channelList);
        }

        [DriverMethod]
        int[] IKtEL30000Transient.GetCount(Keysight.KtEL30000.MinMaxDefMode count, string channelList)
        {
            // Rooftop generated
            return CppApiTransient.GetCount(count, channelList);
        }

        [DriverMethod]
        int[] IKtEL30000Transient.GetCount(string channelList)
        {
            // Rooftop generated
            return CppApiTransient.GetCount(channelList);
        }

        [DriverMethod]
        void IKtEL30000Transient.SetCount(Keysight.KtEL30000.MinMaxDefMode count, string channelList)
        {
            // Rooftop generated
            CppApiTransient.SetCount(count, channelList);
        }

        [DriverMethod]
        void IKtEL30000Transient.SetCount(int count, string channelList)
        {
            // Rooftop generated
            CppApiTransient.SetCount(count, channelList);
        }

        [DriverMethod]
        void IKtEL30000Transient.SetTransientCurrentLevel(Keysight.KtEL30000.MinMaxDefMode transientCurrentLevel, string channelList)
        {
            // Rooftop generated
            CppApiTransient.SetTransientCurrentLevel(transientCurrentLevel, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000Transient.GetTransientCurrentLevel(Keysight.KtEL30000.MinMaxDefMode transientCurrentLevel, string channelList)
        {
            // Rooftop generated
            return CppApiTransient.GetTransientCurrentLevel(transientCurrentLevel, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000Transient.GetTransientCurrentLevel(string channelList)
        {
            // Rooftop generated
            return CppApiTransient.GetTransientCurrentLevel(channelList);
        }

        [DriverMethod]
        void IKtEL30000Transient.SetTransientCurrentLevel(double transientCurrentLevel, string channelList)
        {
            // Rooftop generated
            CppApiTransient.SetTransientCurrentLevel(transientCurrentLevel, channelList);
        }

        [DriverMethod]
        void IKtEL30000Transient.SetTransientPowerLevel(Keysight.KtEL30000.MinMaxDefMode transientPowerLevel, string channelList)
        {
            // Rooftop generated
            CppApiTransient.SetTransientPowerLevel(transientPowerLevel, channelList);
        }

        [DriverMethod]
        void IKtEL30000Transient.SetTransientPowerLevel(double transientPowerLevel, string channelList)
        {
            // Rooftop generated
            CppApiTransient.SetTransientPowerLevel(transientPowerLevel, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000Transient.GetTransientPowerLevel(Keysight.KtEL30000.MinMaxDefMode transientPowerLevel, string channelList)
        {
            // Rooftop generated
            return CppApiTransient.GetTransientPowerLevel(transientPowerLevel, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000Transient.GetTransientPowerLevel(string channelList)
        {
            // Rooftop generated
            return CppApiTransient.GetTransientPowerLevel(channelList);
        }

        [DriverMethod]
        double[] IKtEL30000Transient.GetTransientVoltageLevel(Keysight.KtEL30000.MinMaxDefMode transientVoltageLevel, string channelList)
        {
            // Rooftop generated
            return CppApiTransient.GetTransientVoltageLevel(transientVoltageLevel, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000Transient.GetTransientVoltageLevel(string channelList)
        {
            // Rooftop generated
            return CppApiTransient.GetTransientVoltageLevel(channelList);
        }

        [DriverMethod]
        void IKtEL30000Transient.SetTransientVoltageLevel(Keysight.KtEL30000.MinMaxDefMode transientVoltageLevel, string channelList)
        {
            // Rooftop generated
            CppApiTransient.SetTransientVoltageLevel(transientVoltageLevel, channelList);
        }

        [DriverMethod]
        void IKtEL30000Transient.SetTransientVoltageLevel(double transientVoltageLevel, string channelList)
        {
            // Rooftop generated
            CppApiTransient.SetTransientVoltageLevel(transientVoltageLevel, channelList);
        }

        [DriverMethod]
        void IKtEL30000Transient.SetTransientResistanceLevel(Keysight.KtEL30000.MinMaxDefMode transientResistanceLevel, string channelList)
        {
            // Rooftop generated
            CppApiTransient.SetTransientResistanceLevel(transientResistanceLevel, channelList);
        }

        [DriverMethod]
        void IKtEL30000Transient.SetTransientResistanceLevel(double transientResistanceLevel, string channelList)
        {
            // Rooftop generated
            CppApiTransient.SetTransientResistanceLevel(transientResistanceLevel, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000Transient.GetTransientResistanceLevel(string channelList)
        {
            // Rooftop generated
            return CppApiTransient.GetTransientResistanceLevel(channelList);
        }

        [DriverMethod]
        double[] IKtEL30000Transient.GetTransientResistanceLevel(Keysight.KtEL30000.MinMaxDefMode transientResistanceLevel, string channelList)
        {
            // Rooftop generated
            return CppApiTransient.GetTransientResistanceLevel(transientResistanceLevel, channelList);
        }

        [DriverMethod]
        PrecisionTimeSpan[] IKtEL30000Transient.GetPulseWidth(string channelList)
        {
            // Rooftop generated
            return CppApiTransient.GetPulseWidth(channelList);
        }

        [DriverMethod]
        PrecisionTimeSpan[] IKtEL30000Transient.GetPulseWidth(Keysight.KtEL30000.MinMaxDefMode pulseWidth, string channelList)
        {
            // Rooftop generated
            return CppApiTransient.GetPulseWidth(pulseWidth, channelList);
        }

        [DriverMethod]
        void IKtEL30000Transient.SetPulseWidth(PrecisionTimeSpan pulseWidth, string channelList)
        {
            // Rooftop generated
            CppApiTransient.SetPulseWidth(pulseWidth, channelList);
        }

        [DriverMethod]
        void IKtEL30000Transient.SetPulseWidth(Keysight.KtEL30000.MinMaxDefMode pulseWidth, string channelList)
        {
            // Rooftop generated
            CppApiTransient.SetPulseWidth(pulseWidth, channelList);
        }

        [DriverMethod]
        void IKtEL30000Transient.Initiate(string channelList)
        {
            // Rooftop generated
            CppApiTransient.Initiate(channelList);
        }

        [DriverMethod]
        void IKtEL30000Transient.InitiateContinuousTriggerEnabled(bool initiateContinuousEnabled, string channelList)
        {
            // Rooftop generated
            CppApiTransient.InitiateContinuousTriggerEnabled(initiateContinuousEnabled, channelList);
        }

        [DriverMethod]
        bool[] IKtEL30000Transient.IsInitiateContinuousTriggerEnabled(string channelList)
        {
            // Rooftop generated
            var returnVal = default(Bridge.BooleanVector);   // return value

            returnVal = CppApiTransient.IsInitiateContinuousTriggerEnabled(channelList);

            return returnVal.ToArray();   // return value
        }

        [DriverMethod]
        void IKtEL30000Transient.Abort(string channelList)
        {
            // Rooftop generated
            CppApiTransient.Abort(channelList);
        }

        #endregion

        #region IKtEL30000OperationConstantCurrentSlew

        [DriverMethod]
        void IKtEL30000OperationConstantCurrentSlew.SetNegativeSlewRate(double slewRate, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantCurrentSlew.SetNegativeSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantCurrentSlew.GetNegativeSlewRate(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantCurrentSlew.GetNegativeSlewRate(channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantCurrentSlew.SetNegativeSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantCurrentSlew.SetNegativeSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantCurrentSlew.GetNegativeSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantCurrentSlew.GetSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantCurrentSlew.GetPositiveSlewRate(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantCurrentSlew.GetPositiveSlewRate(channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantCurrentSlew.GetPositiveSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantCurrentSlew.GetPositiveSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantCurrentSlew.SetPositiveSlewRate(double slewRate, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantCurrentSlew.SetPositiveSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantCurrentSlew.SetPositiveSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantCurrentSlew.SetPositiveSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantCurrentSlew.SetPositiveSlewRateMaximumEnabled(bool maxSlewRateEnabled, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantCurrentSlew.SetPositiveSlewRateMaximumEnabled(maxSlewRateEnabled, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantCurrentSlew.SetNegativeSlewRateMaximumEnabled(bool maxSlewRateEnabled, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantCurrentSlew.SetNegativeSlewRateMaximumEnabled(maxSlewRateEnabled, channelList);
        }

        [DriverMethod]
        bool[] IKtEL30000OperationConstantCurrentSlew.GetPositiveSlewRateMaximumEnabled(string channelList)
        {
            // Rooftop generated
            var returnVal = default(Bridge.BooleanVector);   // return value

            returnVal = CppApiOperationConstantCurrentSlew.GetPositiveSlewRateMaximumEnabled(channelList);

            return returnVal.ToArray();   // return value
        }

        [DriverMethod]
        bool[] IKtEL30000OperationConstantCurrentSlew.GetNegativeSlewRateMaximumEnabled(string channelList)
        {
            // Rooftop generated
            var returnVal = default(Bridge.BooleanVector);   // return value

            returnVal = CppApiOperationConstantCurrentSlew.GetNegativeSlewRateMaximumEnabled(channelList);

            return returnVal.ToArray();   // return value
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantCurrentSlew.GetSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantCurrentSlew.GetSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantCurrentSlew.SetSlewRateTrackingEnabled(bool coupleEnabled, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantCurrentSlew.SetSlewRateTrackingEnabled(coupleEnabled, channelList);
        }

        [DriverMethod]
        bool[] IKtEL30000OperationConstantCurrentSlew.GetSlewRateTrackingEnabled(string channelList)
        {
            // Rooftop generated
            var returnVal = default(Bridge.BooleanVector);   // return value

            returnVal = CppApiOperationConstantCurrentSlew.GetSlewRateTrackingEnabled(channelList);

            return returnVal.ToArray();   // return value
        }

        #endregion
        #region IKtEL30000OperationConstantPowerSlew

        #endregion
        #region IKtEL30000Trigger

        #endregion

        #region IKtEL30000OperationConstantPowerSlew

        [DriverMethod]
        double[] IKtEL30000OperationConstantPowerSlew.GetNegativeSlewRate(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantPowerSlew.GetNegativeSlewRate(channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantPowerSlew.GetNegativeSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantPowerSlew.GetNegativeSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        bool[] IKtEL30000OperationConstantPowerSlew.GetNegativeSlewRateMaximumEnabled(string channelList)
        {
            // Rooftop generated
            var returnVal = default(Bridge.BooleanVector);   // return value

            returnVal = CppApiOperationConstantPowerSlew.GetNegativeSlewRateMaximumEnabled(channelList);

            return returnVal.ToArray();   // return value
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantPowerSlew.GetPositiveSlewRate(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantPowerSlew.GetPositiveSlewRate(channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantPowerSlew.GetPositiveSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantPowerSlew.GetPositiveSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        bool[] IKtEL30000OperationConstantPowerSlew.GetPositiveSlewRateMaximumEnabled(string channelList)
        {
            // Rooftop generated
            var returnVal = default(Bridge.BooleanVector);   // return value

            returnVal = CppApiOperationConstantPowerSlew.GetPositiveSlewRateMaximumEnabled(channelList);

            return returnVal.ToArray();   // return value
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantPowerSlew.GetSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantPowerSlew.GetSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        bool[] IKtEL30000OperationConstantPowerSlew.GetSlewRateTrackingEnabled(string channelList)
        {
            // Rooftop generated
            var returnVal = default(Bridge.BooleanVector);   // return value

            returnVal = CppApiOperationConstantPowerSlew.GetSlewRateTrackingEnabled(channelList);

            return returnVal.ToArray();   // return value
        }

        [DriverMethod]
        void IKtEL30000OperationConstantPowerSlew.SetNegativeSlewRate(double slewRate, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantPowerSlew.SetNegativeSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantPowerSlew.SetNegativeSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantPowerSlew.SetNegativeSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantPowerSlew.SetNegativeSlewRateMaximumEnabled(bool maxSlewRateEnabled, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantPowerSlew.SetNegativeSlewRateMaximumEnabled(maxSlewRateEnabled, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantPowerSlew.SetPositiveSlewRate(double slewRate, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantPowerSlew.SetPositiveSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantPowerSlew.SetPositiveSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantPowerSlew.SetPositiveSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantPowerSlew.SetPositiveSlewRateMaximumEnabled(bool maxSlewRateEnabled, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantPowerSlew.SetPositiveSlewRateMaximumEnabled(maxSlewRateEnabled, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantPowerSlew.SetSlewRateTrackingEnabled(bool coupleEnabled, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantPowerSlew.SetSlewRateTrackingEnabled(coupleEnabled, channelList);
        }

        #endregion

        #region IKtEL30000OperationConstantResistanceSlew

        [DriverMethod]
        double[] IKtEL30000OperationConstantResistanceSlew.GetNegativeSlewRate(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantResistanceSlew.GetNegativeSlewRate(channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantResistanceSlew.GetNegativeSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantResistanceSlew.GetNegativeSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        bool[] IKtEL30000OperationConstantResistanceSlew.GetNegativeSlewRateMaximumEnabled(string channelList)
        {
            // Rooftop generated
            var returnVal = default(Bridge.BooleanVector);   // return value

            returnVal = CppApiOperationConstantResistanceSlew.GetNegativeSlewRateMaximumEnabled(channelList);

            return returnVal.ToArray();   // return value
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantResistanceSlew.GetPositiveSlewRate(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantResistanceSlew.GetPositiveSlewRate(channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantResistanceSlew.GetPositiveSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantResistanceSlew.GetPositiveSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        bool[] IKtEL30000OperationConstantResistanceSlew.GetPositiveSlewRateMaximumEnabled(string channelList)
        {
            // Rooftop generated
            var returnVal = default(Bridge.BooleanVector);   // return value

            returnVal = CppApiOperationConstantResistanceSlew.GetPositiveSlewRateMaximumEnabled(channelList);

            return returnVal.ToArray();   // return value
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantResistanceSlew.GetSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantResistanceSlew.GetSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        bool[] IKtEL30000OperationConstantResistanceSlew.GetSlewRateTrackingEnabled(string channelList)
        {
            // Rooftop generated
            var returnVal = default(Bridge.BooleanVector);   // return value

            returnVal = CppApiOperationConstantResistanceSlew.GetSlewRateTrackingEnabled(channelList);

            return returnVal.ToArray();   // return value
        }

        [DriverMethod]
        void IKtEL30000OperationConstantResistanceSlew.SetNegativeSlewRate(double slewRate, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantResistanceSlew.SetNegativeSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantResistanceSlew.SetNegativeSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantResistanceSlew.SetNegativeSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantResistanceSlew.SetNegativeSlewRateMaximumEnabled(bool maxSlewRateEnabled, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantResistanceSlew.SetNegativeSlewRateMaximumEnabled(maxSlewRateEnabled, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantResistanceSlew.SetPositiveSlewRate(double slewRate, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantResistanceSlew.SetPositiveSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantResistanceSlew.SetPositiveSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantResistanceSlew.SetPositiveSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantResistanceSlew.SetPositiveSlewRateMaximumEnabled(bool maxSlewRateEnabled, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantResistanceSlew.SetPositiveSlewRateMaximumEnabled(maxSlewRateEnabled, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantResistanceSlew.SetSlewRateTrackingEnabled(bool coupleEnabled, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantResistanceSlew.SetSlewRateTrackingEnabled(coupleEnabled, channelList);
        }

        #endregion

        #region IKtEL30000OperationConstantVoltageSlew

        [DriverMethod]
        double[] IKtEL30000OperationConstantVoltageSlew.GetNegativeSlewRate(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantVoltageSlew.GetNegativeSlewRate(channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantVoltageSlew.GetNegativeSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantVoltageSlew.GetNegativeSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        bool[] IKtEL30000OperationConstantVoltageSlew.GetNegativeSlewRateMaximumEnabled(string channelList)
        {
            // Rooftop generated
            var returnVal = default(Bridge.BooleanVector);   // return value

            returnVal = CppApiOperationConstantVoltageSlew.GetNegativeSlewRateMaximumEnabled(channelList);

            return returnVal.ToArray();   // return value
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantVoltageSlew.GetPositiveSlewRate(string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantVoltageSlew.GetPositiveSlewRate(channelList);
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantVoltageSlew.GetPositiveSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantVoltageSlew.GetPositiveSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        bool[] IKtEL30000OperationConstantVoltageSlew.GetPositiveSlewRateMaximumEnabled(string channelList)
        {
            // Rooftop generated
            var returnVal = default(Bridge.BooleanVector);   // return value

            returnVal = CppApiOperationConstantVoltageSlew.GetPositiveSlewRateMaximumEnabled(channelList);

            return returnVal.ToArray();   // return value
        }

        [DriverMethod]
        double[] IKtEL30000OperationConstantVoltageSlew.GetSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList)
        {
            // Rooftop generated
            return CppApiOperationConstantVoltageSlew.GetSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        bool[] IKtEL30000OperationConstantVoltageSlew.GetSlewRateTrackingEnabled(string channelList)
        {
            // Rooftop generated
            var returnVal = default(Bridge.BooleanVector);   // return value

            returnVal = CppApiOperationConstantVoltageSlew.GetSlewRateTrackingEnabled(channelList);

            return returnVal.ToArray();   // return value
        }

        [DriverMethod]
        void IKtEL30000OperationConstantVoltageSlew.SetNegativeSlewRate(double slewRate, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantVoltageSlew.SetNegativeSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantVoltageSlew.SetNegativeSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantVoltageSlew.SetNegativeSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantVoltageSlew.SetNegativeSlewRateMaximumEnabled(bool maxSlewRateEnabled, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantVoltageSlew.SetNegativeSlewRateMaximumEnabled(maxSlewRateEnabled, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantVoltageSlew.SetPositiveSlewRate(double slewRate, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantVoltageSlew.SetPositiveSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantVoltageSlew.SetPositiveSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantVoltageSlew.SetPositiveSlewRate(slewRate, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantVoltageSlew.SetPositiveSlewRateMaximumEnabled(bool maxSlewRateEnabled, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantVoltageSlew.SetPositiveSlewRateMaximumEnabled(maxSlewRateEnabled, channelList);
        }

        [DriverMethod]
        void IKtEL30000OperationConstantVoltageSlew.SetSlewRateTrackingEnabled(bool coupleEnabled, string channelList)
        {
            // Rooftop generated
            CppApiOperationConstantVoltageSlew.SetSlewRateTrackingEnabled(coupleEnabled, channelList);
        }

        #endregion
        #region IKtEL30000Trigger

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        IKtEL30000TriggerAcquire IKtEL30000Trigger.Acquire
        {
            get
            {
                return this;
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        IKtEL30000TriggerTransient IKtEL30000Trigger.Transient
        {
            get
            {
                return this;
            }
        }

        [DriverProperty(SuppressPollInstrumentErrors = true)]
        IKtEL30000TriggerDlog IKtEL30000Trigger.Dlog
        {
            get
            {
                return this;
            }
        }

        #endregion

        [DriverMethod]
        void IKtEL30000TriggerAcquire.Immediate(string channelList)
        {
            // Rooftop generated
            CppApiTriggerAcquire.Immediate(channelList);
        }

        [DriverMethod]
        void IKtEL30000TriggerAcquire.SetCurrent(Keysight.KtEL30000.MinMaxDefMode currentValue, string channelList)
        {
            // Rooftop generated
            CppApiTriggerAcquire.SetCurrent(currentValue, channelList);
        }

        [DriverMethod]
        void IKtEL30000TriggerAcquire.SetCurrent(double currentValue, string channelList)
        {
            // Rooftop generated
            CppApiTriggerAcquire.SetCurrent(currentValue, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000TriggerAcquire.GetCurrent(string channelList)
        {
            // Rooftop generated
            return CppApiTriggerAcquire.GetCurrent(channelList);
        }

        [DriverMethod]
        double[] IKtEL30000TriggerAcquire.GetCurrent(Keysight.KtEL30000.MinMaxDefMode currentLevel, string channelList)
        {
            // Rooftop generated
            return CppApiTriggerAcquire.GetCurrent(currentLevel, channelList);
        }

        [DriverMethod]
        double[] IKtEL30000TriggerAcquire.GetVoltage(string channelList)
        {
            // Rooftop generated
            return CppApiTriggerAcquire.GetVoltage(channelList);
        }

        [DriverMethod]
        double[] IKtEL30000TriggerAcquire.GetVoltage(Keysight.KtEL30000.MinMaxDefMode levelValue, string channelList)
        {
            // Rooftop generated
            return CppApiTriggerAcquire.GetVoltage(levelValue, channelList);
        }

        [DriverMethod]
        void IKtEL30000TriggerAcquire.SetVoltage(Keysight.KtEL30000.MinMaxDefMode levelValue, string channelList)
        {
            // Rooftop generated
            CppApiTriggerAcquire.SetVoltage(levelValue, channelList);
        }

        [DriverMethod]
        void IKtEL30000TriggerAcquire.SetVoltage(double levelValues, string channelList)
        {
            // Rooftop generated
            CppApiTriggerAcquire.SetVoltage(levelValues, channelList);
        }

        [DriverMethod]
        AcquireTriggerSource[] IKtEL30000TriggerAcquire.GetTriggerSource(string channelList)
        {
            // Rooftop generated
            return CppApiTriggerAcquire.GetTriggerSource(channelList);
        }

        [DriverMethod]
        void IKtEL30000TriggerAcquire.SetTriggerSource(Keysight.KtEL30000.AcquireTriggerSource acquireTriggerSource, string channelList)
        {
            // Rooftop generated
            CppApiTriggerAcquire.SetTriggerSource(acquireTriggerSource, channelList);
        }

        [DriverMethod]
        Slope[] IKtEL30000TriggerAcquire.GetVoltageSlope(string channelList)
        {
            // Rooftop generated
            return CppApiTriggerAcquire.GetVoltageSlope(channelList);
        }

        [DriverMethod]
        void IKtEL30000TriggerAcquire.SetVoltageSlope(Keysight.KtEL30000.Slope voltageSlope, string channelList)
        {
            // Rooftop generated
            CppApiTriggerAcquire.SetVoltageSlope(voltageSlope, channelList);
        }

        [DriverMethod]
        void IKtEL30000TriggerAcquire.SetCurrentSlope(Keysight.KtEL30000.Slope currentSlope, string channelList)
        {
            // Rooftop generated
            CppApiTriggerAcquire.SetCurrentSlope(currentSlope, channelList);
        }

        [DriverMethod]
        Slope[] IKtEL30000TriggerAcquire.GetCurrentSlope(string channelList)
        {
            // Rooftop generated
            return CppApiTriggerAcquire.GetCurrentSlope(channelList);
        }


        #region IKtEL30000TriggerTransient

        [DriverMethod]
        void IKtEL30000TriggerTransient.Immediate(string channelList)
        {
            // Rooftop generated
            CppApiTriggerTransient.Immediate(channelList);
        }

        [DriverMethod]
        TransientTriggerSource[] IKtEL30000TriggerTransient.GetTriggerSource(string channelList)
        {
            // Rooftop generated
            return CppApiTriggerTransient.GetTriggerSource(channelList);
        }

        [DriverMethod]
        void IKtEL30000TriggerTransient.SetTriggerSource(Keysight.KtEL30000.TransientTriggerSource transientTriggerSource, string channelList)
        {
            // Rooftop generated
            CppApiTriggerTransient.SetTriggerSource(transientTriggerSource, channelList);
        }

        [DriverMethod]
        PrecisionTimeSpan[] IKtEL30000TriggerTransient.GetDelay(string channelList)
        {
            // Rooftop generated
            return CppApiTriggerTransient.GetDelay(channelList);
        }

        [DriverMethod]
        PrecisionTimeSpan[] IKtEL30000TriggerTransient.GetDelay(Keysight.KtEL30000.MinMaxDefMode time, string channelList)
        {
            // Rooftop generated
            return CppApiTriggerTransient.GetDelay(time, channelList);
        }

        [DriverMethod]
        void IKtEL30000TriggerTransient.SetDelay(Keysight.KtEL30000.MinMaxDefMode time, string channelList)
        {
            // Rooftop generated
            CppApiTriggerTransient.SetDelay(time, channelList);
        }

        [DriverMethod]
        void IKtEL30000TriggerTransient.SetDelay(PrecisionTimeSpan time, string channelList)
        {
            // Rooftop generated
            CppApiTriggerTransient.SetDelay(time, channelList);
        }

        #endregion

        #region IKtEL30000TriggerDlog

        [DriverMethod]
        void IKtEL30000TriggerDlog.Immediate()
        {
            // Rooftop generated
            CppApiTriggerDlog.Immediate();
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        Keysight.KtEL30000.TriggerSource IKtEL30000TriggerDlog.TriggerSource
        {
            get
            {
                return CppApiTriggerDlog.TriggerSource;      // Rooftop generated
            }
            set
            {
                CppApiTriggerDlog.TriggerSource = value;     // Rooftop generated
            }
        }

        #endregion

        #region IKtEL30000Digital

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        [NotSupported(Models.EL33133A)]
        short IKtEL30000Digital.InputData
        {
            get
            {
                return CppApiDigital.InputData;      // Rooftop generated
            }
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        [NotSupported(Models.EL33133A)]
        short IKtEL30000Digital.OutputData
        {
            get
            {
                return CppApiDigital.OutputData;      // Rooftop generated
            }
            set
            {
                CppApiDigital.OutputData = value;     // Rooftop generated
            }
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        void IKtEL30000Digital.SetDigitalPinPolarity(int pinIndex, Keysight.KtEL30000.Polarity pinPolarity)
        {
            CppApiDigital.SetDigitalPinPolarity(pinIndex, pinPolarity);
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        Keysight.KtEL30000.Polarity IKtEL30000Digital.GetDigitalPinPolarity(int pinIndex)
        {
            // Rooftop generated
            return CppApiDigital.GetDigitalPinPolarity(pinIndex);
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        Keysight.KtEL30000.DigitalPinFunctionType IKtEL30000Digital.GetDigitalPinFunction(int pinIndex)
        {
            // Rooftop generated
            return CppApiDigital.GetDigitalPinFunction(pinIndex);
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        void IKtEL30000Digital.SetDigitalPinFunction(int pinIndex, Keysight.KtEL30000.DigitalPinFunctionType pinFunction)
        {
            CppApiDigital.SetDigitalPinFunction(pinIndex, pinFunction);
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        [NotSupported(Models.EL33133A)]
        bool IKtEL30000Digital.BusTriggerOutputEnabled
        {
            get
            {
                return CppApiDigital.BusTriggerOutputEnabled;      // Rooftop generated
            }
            set
            {
                CppApiDigital.BusTriggerOutputEnabled = value;     // Rooftop generated
            }
        }

        #endregion

        #region IKtEL30000DataLog

        [DriverMethod]
        void IKtEL30000DataLog.SetCurrentDataLogEnabled(bool currentDataLogEnabled, string channelList)
        {
            // Rooftop generated
            CppApiDataLog.SetCurrentDataLogEnabled(currentDataLogEnabled, channelList);
        }

        [DriverMethod]
        void IKtEL30000DataLog.SetVoltageDataLogEnabled(bool voltageDataLogEnabled, string channelList)
        {
            // Rooftop generated
            CppApiDataLog.SetVoltageDataLogEnabled(voltageDataLogEnabled, channelList);
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        double IKtEL30000DataLog.TriggerOffset
        {
            get
            {
                return CppApiDataLog.TriggerOffset;      // Rooftop generated
            }
            set
            {
                CppApiDataLog.TriggerOffset = value;     // Rooftop generated
            }
        }

        [DriverMethod]
        void IKtEL30000DataLog.SetSampleInterval(Keysight.KtEL30000.MinMaxDefMode sampleIntervalTime)
        {
            // Rooftop generated
            CppApiDataLog.SetSampleInterval(sampleIntervalTime);
        }

        [DriverMethod]
        PrecisionTimeSpan IKtEL30000DataLog.GetSampleInterval(Keysight.KtEL30000.MinMaxDefMode sampleIntervalTime)
        {
            // Rooftop generated
            return CppApiDataLog.GetSampleInterval(sampleIntervalTime);
        }

        [DriverMethod]
        PrecisionTimeSpan IKtEL30000DataLog.GetSampleTime(Keysight.KtEL30000.MinMaxDefMode sampleTime)
        {
            // Rooftop generated
            return CppApiDataLog.GetSampleTime(sampleTime);
        }

        [DriverMethod]
        void IKtEL30000DataLog.SetSampleTime(Keysight.KtEL30000.MinMaxDefMode sampleTime)
        {
            // Rooftop generated
            CppApiDataLog.SetSampleTime(sampleTime);
        }

        [DriverMethod]
        bool[] IKtEL30000DataLog.GetCurrentDataLogEnabled(string channelList)
        {
            // Rooftop generated
            var returnVal = default(Bridge.BooleanVector);   // return value

            returnVal = CppApiDataLog.GetCurrentDataLogEnabled(channelList);

            return returnVal.ToArray();   // return value
        }

        [DriverMethod]
        bool[] IKtEL30000DataLog.GetVoltageDataLogEnabled(string channelList)
        {
            // Rooftop generated
            var returnVal = default(Bridge.BooleanVector);   // return value

            returnVal = CppApiDataLog.GetVoltageDataLogEnabled(channelList);

            return returnVal.ToArray();   // return value
        }

        [DriverMethod]
        void IKtEL30000DataLog.Initiate(string filename)
        {
            // Rooftop generated
            CppApiDataLog.Initiate(filename);
        }

        [DriverMethod]
        void IKtEL30000DataLog.Abort()
        {
            // Rooftop generated
            CppApiDataLog.Abort();
        }

        [DriverMethod]
        double[] IKtEL30000DataLog.Fetch(int numberOfLoggedData, string channelList)
        {
            // Rooftop generated
            return CppApiDataLog.Fetch(numberOfLoggedData, channelList);
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        PrecisionTimeSpan IKtEL30000DataLog.SampleInterval
        {
            get
            {
                return CppApiDataLog.SampleInterval;      // Rooftop generated
            }
            set
            {
                CppApiDataLog.SampleInterval = value;     // Rooftop generated
            }
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        PrecisionTimeSpan IKtEL30000DataLog.SampleTime
        {
            get
            {
                return CppApiDataLog.SampleTime;      // Rooftop generated
            }
            set
            {
                CppApiDataLog.SampleTime = value;     // Rooftop generated
            }
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        bool IKtEL30000DataLog.MinMaxDataLogEnabled
        {
            get
            {
                return CppApiDataLog.MinMaxDataLogEnabled;      // Rooftop generated
            }
            set
            {
                CppApiDataLog.MinMaxDataLogEnabled = value;     // Rooftop generated
            }
        }

        #endregion

        #region IKtEL30000Sweep

        [DriverMethod]
        int[] IKtEL30000Sweep.GetPoints(Keysight.KtEL30000.MinMaxDefMode sweepPoints, string channelList)
        {
            // Rooftop generated
            return CppApiSweep.GetPoints(sweepPoints, channelList);
        }

        [DriverMethod]
        int[] IKtEL30000Sweep.GetPoints(string channelList)
        {
            // Rooftop generated
            return CppApiSweep.GetPoints(channelList);
        }

        [DriverMethod]
        void IKtEL30000Sweep.SetPoints(Keysight.KtEL30000.MinMaxDefMode sweepPoints, string channelList)
        {
            // Rooftop generated
            CppApiSweep.SetPoints(sweepPoints, channelList);
        }

        [DriverMethod]
        void IKtEL30000Sweep.SetPoints(int sweepPoints, string channelList)
        {
            // Rooftop generated
            CppApiSweep.SetPoints(sweepPoints, channelList);
        }

        [DriverMethod]
        int[] IKtEL30000Sweep.GetOffsetPoints(string channelList)
        {
            // Rooftop generated
            return CppApiSweep.GetOffsetPoints(channelList);
        }

        [DriverMethod]
        int[] IKtEL30000Sweep.GetOffsetPoints(Keysight.KtEL30000.MinMaxDefMode sweepOffsetPoints, string channelList)
        {
            // Rooftop generated
            return CppApiSweep.GetOffsetPoints(sweepOffsetPoints, channelList);
        }

        [DriverMethod]
        void IKtEL30000Sweep.SetOffsetPoints(Keysight.KtEL30000.MinMaxDefMode sweepOffsetPoints, string channelList)
        {
            // Rooftop generated
            CppApiSweep.SetOffsetPoints(sweepOffsetPoints, channelList);
        }

        [DriverMethod]
        void IKtEL30000Sweep.SetOffsetPoints(int sweepOffsetPoints, string channelList)
        {
            // Rooftop generated
            CppApiSweep.SetOffsetPoints(sweepOffsetPoints, channelList);
        }

        [DriverMethod]
        void IKtEL30000Sweep.SetTimeInterval(PrecisionTimeSpan sweepTimeInterval, string channelList)
        {
            // Rooftop generated
            CppApiSweep.SetTimeInterval(sweepTimeInterval, channelList);
        }

        [DriverMethod]
        void IKtEL30000Sweep.SetTimeInterval(Keysight.KtEL30000.MinMaxDefMode sweepTimeInterval, string channelList)
        {
            // Rooftop generated
            CppApiSweep.SetTimeInterval(sweepTimeInterval, channelList);
        }

        [DriverMethod]
        PrecisionTimeSpan[] IKtEL30000Sweep.GetTimeInterval(Keysight.KtEL30000.MinMaxDefMode sweepTimeInterval, string channelList)
        {
            // Rooftop generated
            return CppApiSweep.GetTimeInterval(sweepTimeInterval, channelList);
        }

        [DriverMethod]
        PrecisionTimeSpan[] IKtEL30000Sweep.GetTimeInterval(string channelList)
        {
            // Rooftop generated
            return CppApiSweep.GetTimeInterval(channelList);
        }

        #endregion
        #region IKtEL30000Memory

        #endregion

        #region IKtEL30000Memory

        [DriverMethod]
        void IKtEL30000Memory.ExportFile(string filename)
        {
            // Rooftop generated
            CppApiMemory.ExportFile(filename);
        }

        #endregion
        #region IKtEL30000IOControl

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        [NotSupported(Models.EL33133A)]
        string IKtEL30000IOControl.ChannelCouple
        {
            get
            {
                return CppApiIOControl.ChannelCouple;      // Rooftop generated
            }
            set
            {
                CppApiIOControl.ChannelCouple = value;     // Rooftop generated
            }
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        PrecisionTimeSpan[] IKtEL30000IOControl.GetRiseTimeDelay(string channelList)
        {
            // Rooftop generated
            return CppApiIOControl.GetRiseTimeDelay(channelList);
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        PrecisionTimeSpan[] IKtEL30000IOControl.GetRiseTimeDelay(Keysight.KtEL30000.MinMaxDefMode riseTimeDelay, string channelList)
        {
            // Rooftop generated
            return CppApiIOControl.GetRiseTimeDelay(riseTimeDelay, channelList);
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        void IKtEL30000IOControl.SetRiseTimeDelay(Keysight.KtEL30000.MinMaxDefMode riseTimeDelay, string channelList)
        {
            // Rooftop generated
            CppApiIOControl.SetRiseTimeDelay(riseTimeDelay, channelList);
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        void IKtEL30000IOControl.SetRiseTimeDelay(PrecisionTimeSpan riseTimeDelay, string channelList)
        {
            // Rooftop generated
            CppApiIOControl.SetRiseTimeDelay(riseTimeDelay, channelList);
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        void IKtEL30000IOControl.SetFallTimeDelay(Keysight.KtEL30000.MinMaxDefMode fallTimeDelay, string channelList)
        {
            // Rooftop generated
            CppApiIOControl.SetFallTimeDelay(fallTimeDelay, channelList);
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        PrecisionTimeSpan[] IKtEL30000IOControl.GetFallTimeDelay(Keysight.KtEL30000.MinMaxDefMode fallTimeDelay, string channelList)
        {
            // Rooftop generated
            return CppApiIOControl.GetFallTimeDelay(fallTimeDelay, channelList);
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        PrecisionTimeSpan[] IKtEL30000IOControl.GetFallTimeDelay(string channelList)
        {
            // Rooftop generated
            return CppApiIOControl.GetFallTimeDelay(channelList);
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        void IKtEL30000IOControl.SetFallTimeDelay(PrecisionTimeSpan fallTimeDelay, string channelList)
        {
            // Rooftop generated
            CppApiIOControl.SetFallTimeDelay(fallTimeDelay, channelList);
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        void IKtEL30000IOControl.SetEnabled(bool ioEnabled, string channelList)
        {
            // Rooftop generated
            CppApiIOControl.SetEnabled(ioEnabled, channelList);
        }

        [DriverMethod]
        [NotSupported(Models.EL33133A)]
        bool[] IKtEL30000IOControl.GetEnabled(string channelList)
        {

            // Rooftop generated
            var returnVal = default(Bridge.BooleanVector);   // return value

            returnVal = CppApiIOControl.GetEnabled(channelList);

            return returnVal.ToArray();   // return value
        }

        [DriverProperty]
        [Simulation(SimulationMode.Manual)]
        [NotSupported(Models.EL33133A)]
        Keysight.KtEL30000.InhibitMode IKtEL30000IOControl.InhibitMode
        {
            get
            {
                return CppApiIOControl.InhibitMode;      // Rooftop generated
            }
            set
            {
                CppApiIOControl.InhibitMode = value;     // Rooftop generated
            }
        }

        #endregion
    }
}
