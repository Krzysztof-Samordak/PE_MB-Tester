using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Resources;
using System.Text;
using MindWorks.Nimbus;
using Ivi.Driver;


namespace Keysight.KtEL30000
{
    public interface IKtEL30000
    {
        IKtEL30000System System { get; }
        IKtEL30000Status Status { get; }
        IKtEL30000Trigger Trigger { get; }
        IKtEL30000Memory Memory { get; }
        IKtEL30000Transient Transient { get; }
        IKtEL30000Operation Operation { get; }
        IKtEL30000Calibration Calibration { get; }
        IKtEL30000Display Display { get; }
        IKtEL30000Acquisition Acquisition { get; }
        IKtEL30000Measurement Measurement { get; }
        IKtEL30000List List { get; }
        IKtEL30000Digital Digital { get; }
        IKtEL30000DataLog DataLog { get; }
        IKtEL30000Sweep Sweep { get; }
        IKtEL30000IOControl IOControl { get; }
    }

    public interface IKtEL30000System
    {
        void RecallState(int state);
        void SaveState(int state);
        string SerialNumber { get; }
        void ClearIO();
        byte[] ReadBytes();
        string ReadString();
        void WaitForOperationComplete(TimeSpan maxTime);
        void WriteBytes(byte[] buffer);
        void WriteString(string data);
        PrecisionTimeSpan IOTimeout { get; set; }
        IKtEL30000SystemTracing Tracing { get; }
        Keysight.KtEL30000.PowerOnStateMode PowerOnStateMode { get; set; }
        bool BeepEnabled { get; set; }
        void SetBeep();
    }
    public interface IKtEL30000Status
    {
        void Clear();
        void Preset();
        StatusByteFlags SerialPoll();
        event EventHandler<ServiceRequestEventArgs> ServiceRequestEvent;
        void DisableServiceRequestEvents();
        Keysight.KtEL30000.StatusByteFlags ReadStatusByteRegister();
        Keysight.KtEL30000.StatusByteFlags ServiceRequestEnableRegister { get; set; }
        void EnableServiceRequestEvents();
        IKtEL30000StatusOperation Operation { get; }
        IKtEL30000StatusQuestionable Questionable { get; }
        IKtEL30000StatusStandardEvent StandardEvent { get; }
    }
    public interface IKtEL30000SystemTracing
    {
        void Flush();
        void Write(string message);
        bool Enabled { get; set; }
        string FileName { get; set; }
        Keysight.KtEL30000.TracingLevel Level { get; set; }
    }
    public interface IKtEL30000StatusOperation
    {
        Keysight.KtEL30000.StatusOperationFlags ReadConditionRegister();
        Keysight.KtEL30000.StatusOperationFlags EnableRegister { get; set; }
        Keysight.KtEL30000.StatusOperationFlags NegativeTransitionFilter { get; set; }
        Keysight.KtEL30000.StatusOperationFlags PositiveTransitionFilter { get; set; }
        StatusOperationFlags ReadEventRegister();
    }
    public interface IKtEL30000StatusQuestionable
    {
        StatusQuestionableFlags PositiveTransitionFilter { get; set; }
        StatusQuestionableFlags NegativeTransitionFilter { get; set; }
        StatusQuestionableFlags EnableRegister { get; set; }
        StatusQuestionableFlags ReadEventRegister(); StatusQuestionableFlags ReadConditionRegister();
    }
    public interface IKtEL30000StatusStandardEvent
    {
        Keysight.KtEL30000.StatusStandardEventFlags ReadEventRegister();
        Keysight.KtEL30000.StatusStandardEventFlags EnableRegister { get; set; }
    }

    public interface IKtEL30000Trigger
    {
        IKtEL30000TriggerAcquire Acquire { get; }
        IKtEL30000TriggerTransient Transient { get; }
        IKtEL30000TriggerDlog Dlog { get; }
    }

    public interface IKtEL30000Memory
    {
        void ExportFile(string filename);
    }

    public interface IKtEL30000Transient
    {
        void SetMode(Keysight.KtEL30000.TransientMode transientMode, string channelList);
        TransientMode[] GetMode(string channelList);
        void SetFrequency(Keysight.KtEL30000.MinMaxDefMode frequency, string channelList);
        double[] GetFrequency(Keysight.KtEL30000.MinMaxDefMode frequency, string channelList);
        double[] GetFrequency(string channelList);
        void SetFrequency(double frequency, string channelList);
        double[] GetDutyCycle(Keysight.KtEL30000.MinMaxDefMode dutyCycle, string channelList);
        double[] GetDutyCycle(string channelList);
        void SetDutyCycle(Keysight.KtEL30000.MinMaxDefMode dutyCycle, string channelList);
        void SetDutyCycle(double dutyCycle, string channelList);
        int[] GetCount(Keysight.KtEL30000.MinMaxDefMode count, string channelList);
        int[] GetCount(string channelList);
        void SetCount(Keysight.KtEL30000.MinMaxDefMode count, string channelList);
        void SetCount(int count, string channelList);
        void SetTransientCurrentLevel(Keysight.KtEL30000.MinMaxDefMode transientCurrentLevel, string channelList);
        double[] GetTransientCurrentLevel(Keysight.KtEL30000.MinMaxDefMode transientCurrentLevel, string channelList);
        double[] GetTransientCurrentLevel(string channelList);
        void SetTransientCurrentLevel(double transientCurrentLevel, string channelList);
        double[] GetTransientPowerLevel(string channelList);
        double[] GetTransientPowerLevel(Keysight.KtEL30000.MinMaxDefMode transientPowerLevel, string channelList);
        void SetTransientPowerLevel(double transientPowerLevel, string channelList);
        void SetTransientPowerLevel(Keysight.KtEL30000.MinMaxDefMode transientPowerLevel, string channelList);
        void SetTransientVoltageLevel(double transientVoltageLevel, string channelList);
        void SetTransientVoltageLevel(Keysight.KtEL30000.MinMaxDefMode transientVoltageLevel, string channelList);
        double[] GetTransientVoltageLevel(string channelList);
        double[] GetTransientVoltageLevel(Keysight.KtEL30000.MinMaxDefMode transientVoltageLevel, string channelList);
        double[] GetTransientResistanceLevel(Keysight.KtEL30000.MinMaxDefMode transientResistanceLevel, string channelList);
        double[] GetTransientResistanceLevel(string channelList);
        void SetTransientResistanceLevel(double transientResistanceLevel, string channelList);
        void SetTransientResistanceLevel(Keysight.KtEL30000.MinMaxDefMode transientResistanceLevel, string channelList);
        PrecisionTimeSpan[] GetPulseWidth(string channelList);
        PrecisionTimeSpan[] GetPulseWidth(Keysight.KtEL30000.MinMaxDefMode pulseWidth, string channelList);
        void SetPulseWidth(PrecisionTimeSpan pulseWidth, string channelList);
        void SetPulseWidth(Keysight.KtEL30000.MinMaxDefMode pulseWidth, string channelList);
        void Initiate(string channelList);
        void InitiateContinuousTriggerEnabled(bool initiateContinuousEnabled, string channelList);
        bool[] IsInitiateContinuousTriggerEnabled(string channelList);
        void Abort(string channelList);
    }

    public interface IKtEL30000Operation
    {
        IKtEL30000OperationConstantCurrent ConstantCurrent { get; }
        IKtEL30000OperationConstantVoltage ConstantVoltage { get; }
        IKtEL30000OperationConstantResistance ConstantResistance { get; }
        void ConfigureMode(Keysight.KtEL30000.OperationMode mode, string channelList);
        OperationMode[] GetConfigurationMode(string channelList);
        IKtEL30000OperationConstantPower ConstantPower { get; }
        Keysight.KtEL30000.PairMode PairMode { get; set; }
        void ClearProtection(string channelList);
        bool InputShortEnabled { get; set; }
    }

    public interface IKtEL30000OperationConstantCurrent
    {
        double[] GetCurrent(string channelList);
        void SetTriggerLevel(Keysight.KtEL30000.MinMaxDefMode triggerLevel, string channelList);
        double[] GetTriggerLevel(Keysight.KtEL30000.MinMaxDefMode triggerLevel, string channelList);
        double[] GetTriggerLevel(string channelList);
        void SetTriggerLevel(double triggerLevel, string channelList);
        IKtEL30000OperationConstantCurrentProtection Protection { get; }
        void SetMode(Keysight.KtEL30000.CurrentMode mode, string channelList);
        CurrentMode[] GetMode(string channelList);
        double[] GetCurrent(Keysight.KtEL30000.MinMaxDefMode currentLevel, string channelList);
        void SetCurrent(Keysight.KtEL30000.MinMaxDefMode currentValue, string channelList);
        void SetCurrent(double currentValue, string channelList);
        void SetCurrentRange(Keysight.KtEL30000.MinMaxDefMode range, string channelList);
        double[] GetCurrentRange(Keysight.KtEL30000.MinMaxDefMode range, string channelList);
        double[] GetCurrentRange(string channelList);
        void SetCurrentRange(double range, string channelList);
        IKtEL30000OperationConstantCurrentSlew Slew { get; }
    }

    public interface IKtEL30000OperationConstantVoltage
    {
        double[] GetVoltage(string channelList);
        double[] GetVoltage(Keysight.KtEL30000.MinMaxDefMode levelValue, string channelList);
        VoltageMode[] GetMode(string channelList);
        double[] GetTriggerLevel(string channelList);
        double[] GetTriggerLevel(Keysight.KtEL30000.MinMaxDefMode levelValue, string channelList);
        void SetVoltage(double voltageValue, string channelList);
        void SetVoltage(Keysight.KtEL30000.MinMaxDefMode voltageValue, string channelList);
        void SetMode(Keysight.KtEL30000.VoltageMode mode, string channelList);
        void SetTriggerLevel(Keysight.KtEL30000.MinMaxDefMode triggerLevel, string channelList);
        void SetTriggerLevel(double triggerLevel, string channelList);
        double[] GetCurrentLimit(Keysight.KtEL30000.MinMaxDefMode currentLimit, string channelList);
        void SetCurrentLimit(Keysight.KtEL30000.MinMaxDefMode currentLimit, string channelList);
        void SetCurrentLimit(double currentLimit, string channelList);
        double[] GetCurrentLimit(string channelList);
        IKtEL30000OperationConstantVoltageSlew Slew { get; }
        void SetVoltageRange(double range, string channelList);
        void SetVoltageRange(Keysight.KtEL30000.MinMaxDefMode range, string channelList);
        double[] GetVoltageRange(Keysight.KtEL30000.MinMaxDefMode range, string channelList);
        double[] GetVoltageRange(string channelList);
        SenseMode[] GetVoltageSensingMode(string channelList);
        void SetVoltageSensingMode(Keysight.KtEL30000.SenseMode senseMode, string channelList);
        void SetInhibitVoltageTurnOnLevel(Keysight.KtEL30000.MinMaxDefMode inhibitVoltageTurnOnLevel, string channelList);
        double[] GetInhibitVoltageTurnOnLevel(Keysight.KtEL30000.MinMaxDefMode inhibitVoltageTurnOnLevel, string channelList);
        void SetInhibitVoltageTurnOnLevel(double inhibitVoltageTurnOnLevel, string channelList);
        double[] GetInhibitVoltageTurnOnLevel(string channelList);
        void SetInhibitVoltageTurnOnMode(Keysight.KtEL30000.InhibitMode inhibitVoltageTurnOnMode, string channelList);
        InhibitMode[] GetInhibitVoltageTurnOnMode(string channelList);
    }

    public interface IKtEL30000OperationConstantResistance
    {
        double[] GetResistance(string channelList);
        double[] GetResistance(Keysight.KtEL30000.MinMaxDefMode levelValue, string channelList);
        ResistanceMode[] GetMode(string channelList);
        double[] GetTriggerLevel(string channelList);
        double[] GetTriggerLevel(Keysight.KtEL30000.MinMaxDefMode triggerLevel, string channelList);
        void SetResistance(Keysight.KtEL30000.MinMaxDefMode resistanceValue, string channelList);
        void SetResistance(double resistanceValue, string channelList);
        void SetTriggerLevel(double triggerLevel, string channelList);
        void SetTriggerLevel(Keysight.KtEL30000.MinMaxDefMode triggerLevel, string channelList);
        IKtEL30000OperationConstantResistanceSlew Slew { get; }
        double[] GetResistanceRange(string channelList);
        double[] GetResistanceRange(Keysight.KtEL30000.MinMaxDefMode range, string channelList);
        void SetResistanceRange(double range, string channelList);
        void SetResistanceRange(Keysight.KtEL30000.MinMaxDefMode range, string channelList);
        void SetMode(Keysight.KtEL30000.ResistanceMode mode, string channelList);
    }

    public interface IKtEL30000OperationConstantPower
    {
        IKtEL30000OperationConstantPowerProtection Protection { get; }
        double[] GetPower(string channelList);
        double[] GetPower(Keysight.KtEL30000.MinMaxDefMode powerLevel, string channelList);
        PowerMode[] GetMode(string channelList);
        double[] GetTriggerLevel(string channelList);
        double[] GetTriggerLevel(Keysight.KtEL30000.MinMaxDefMode triggerLevel, string channelList);
        void SetPower(double powerLevel, string channelList);
        void SetPower(Keysight.KtEL30000.MinMaxDefMode powerLevel, string channelList);
        void SetMode(Keysight.KtEL30000.PowerMode mode, string channelList);
        void SetTriggerLevel(Keysight.KtEL30000.MinMaxDefMode triggerLevel, string channelList);
        void SetTriggerLevel(double triggerLevel, string channelList);
        void SetPowerRange(double range, string channelList);
        void SetPowerRange(Keysight.KtEL30000.MinMaxDefMode range, string channelList);
        double[] GetPowerRange(string channelList);
        double[] GetPowerRange(Keysight.KtEL30000.MinMaxDefMode range, string channelList);
        IKtEL30000OperationConstantPowerSlew Slew { get; }
    }

    public interface IKtEL30000Calibration
    {
        int SecureCode { set; }
        void SetSecureCodeEnabled(bool state, string passCode);
        bool GetSecureStateEnabled();
        string Message { get; set; }
        bool AutoSaveEnabled { get; set; }
        int GetCount();
        void SetInputCurrentLowLevel(Keysight.KtEL30000.MinMaxDefMode currentLevel, string channelList);
        void SetInputCurrentLowData(double current, string channelList);
        void SetInputVoltageLowLevel(Keysight.KtEL30000.MinMaxDefMode voltageLevel, string channelList);
        void SetCurrentLimitLevel(Keysight.KtEL30000.MinMaxDefMode currentLimitLevel, string channelList);
        void SetInputVoltageLowData(double voltage, string channelList);
        void SetCurrentLimitData(double limitCurrentData, string channelList);
        void SetInputVoltageHighData(double voltage, string channelList);
        void SetInputCurrentMediumLevel(Keysight.KtEL30000.MinMaxDefMode currentLevel, string channelList);
        void SetInputCurrentHighLevel(Keysight.KtEL30000.CalibrationCurrentLevel currentLevel, string channelList);
        void SetInputCurrentMediumData(double current, string channelList);
        void SetInputCurrentHighData(double current, string channelList);
        void SetInputVoltageHighLevel(Keysight.KtEL30000.MinMaxDefMode voltageLevel, string channelList);
        void Save();
    }

    public interface IKtEL30000Display
    {
        bool Enabled { get; set; }
        string Text { get; set; }
        void ClearText();
        Keysight.KtEL30000.ViewMeter View { get; set; }
    }

    public interface IKtEL30000Acquisition
    {
        void Abort(string channelList);
        void Initiate(string channelList);
    }

    public interface IKtEL30000Measurement
    {
        double[] Fetch(Keysight.KtEL30000.MeasurementFunction measurementFunction, Keysight.KtEL30000.MeasurementType measurementType, string channelList);
        double[] Measure(Keysight.KtEL30000.MeasurementFunction measurementFunction, Keysight.KtEL30000.MeasurementType measurementType, string channelList);
        void SetVoltageMeasurementEnabled(bool voltageMeasurementEnabled, string channelList);
        bool[] GetVoltageMeasurementEnabled(string channelList);
        bool[] GetCurrentMeasurementEnabled(string channelList);
        void SetCurrentMeasurementEnabled(bool currentMeasurementEnabled, string channelList);
        double[] Fetch(Keysight.KtEL30000.MeasurementFunction measurementFunction, string channelList);
        double[] Measure(Keysight.KtEL30000.MeasurementFunction measurementFunction, string channelList);
    }

    public interface IKtEL30000List
    {
        void SetCount(int count, string channelList);
        void SetCount(Keysight.KtEL30000.MinMaxDefMode count, string channelList);
        int[] GetCount(Keysight.KtEL30000.MinMaxDefMode count, string channelList);
        int[] GetCount(string channelList);
        double[] GetLevel(Keysight.KtEL30000.OperationMode mode, string channelList);
        void SetLevel(Keysight.KtEL30000.OperationMode mode, double[] levelList, string channelList);
        int[] GetNumberOfPoints(Keysight.KtEL30000.OperationMode mode, string channelList);
        void SetDwellTime(PrecisionTimeSpan[] dwellTimeList, string channelList);
        PrecisionTimeSpan[] GetDwellTime(string channelList);
        int[] GetNumberOfDwellPoints(string channelList);
        void SetTerminationModeEnabled(bool lastStepValueEnabled, string channelList);
        bool[] GetTerminationModeEnabled(string channelList);
        void SetMode(Keysight.KtEL30000.AutoMode mode, string channelList);
        AutoMode[] GetMode(string channelList);
        void SetTriggerSignalOutputEnabled(bool[] triggerSignalOutputEnabled, Keysight.KtEL30000.ListTriggerSignalOutputMode listTriggerSignalOutputMode, string channelList);
        bool[] GetTriggerSignalOutputEnabled(Keysight.KtEL30000.ListTriggerSignalOutputMode listTriggerSignalOutputMode, string channelList);
        int[] GetTriggerSignalOutputNumberOfPoint(Keysight.KtEL30000.ListTriggerSignalOutputMode listTriggerSignalOutputMode, string channelList);
    }

    public interface IKtEL30000OperationConstantCurrentProtection
    {
        void SetEnabled(bool protectionEnabled, string channelList);
        bool[] GetEnabled(string channelList);
        void SetOCPDelayTimerMode(Keysight.KtEL30000.OverCurrentProtectionDelayTimerMode mode, string channelList);
        OverCurrentProtectionDelayTimerMode[] GetOCPDelayTimerMode(string channelList);
        void SetOCPDelayTime(Keysight.KtEL30000.MinMaxDefMode time, string channelList);
        PrecisionTimeSpan[] GetOCPDelayTime(Keysight.KtEL30000.MinMaxDefMode time, string channelList);
        void SetOCPDelayTime(PrecisionTimeSpan time, string channelList);
        PrecisionTimeSpan[] GetOCPDelayTime(string channelList);
        bool IsTripped();
    }

    public interface IKtEL30000OperationConstantPowerProtection
    {
        bool[] GetEnabled(string channelList);
        PrecisionTimeSpan[] GetOPPDelayTime(string channelList);
        PrecisionTimeSpan[] GetOPPDelayTime(Keysight.KtEL30000.MinMaxDefMode delayTime, string channelList);
        bool IsTripped();
        void SetEnabled(bool enabled, string channelList);
        void SetOPPDelayTime(Keysight.KtEL30000.MinMaxDefMode delayTime, string channelList);
        void SetOPPDelayTime(PrecisionTimeSpan delayTime, string channelList);
    }

    public interface IKtEL30000OperationConstantCurrentSlew
    {
        void SetNegativeSlewRate(double slewRate, string channelList);
        double[] GetNegativeSlewRate(string channelList);
        void SetNegativeSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList);
        double[] GetNegativeSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList);
        double[] GetPositiveSlewRate(string channelList);
        double[] GetPositiveSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList);
        void SetPositiveSlewRate(double slewRate, string channelList);
        void SetPositiveSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList);
        void SetPositiveSlewRateMaximumEnabled(bool maxSlewRateEnabled, string channelList);
        void SetNegativeSlewRateMaximumEnabled(bool maxSlewRateEnabled, string channelList);
        bool[] GetPositiveSlewRateMaximumEnabled(string channelList);
        bool[] GetNegativeSlewRateMaximumEnabled(string channelList);
        double[] GetSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList);
        void SetSlewRateTrackingEnabled(bool coupleEnabled, string channelList);
        bool[] GetSlewRateTrackingEnabled(string channelList);
    }

    public interface IKtEL30000OperationConstantPowerSlew
    {
        double[] GetNegativeSlewRate(string channelList);
        double[] GetNegativeSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList);
        bool[] GetNegativeSlewRateMaximumEnabled(string channelList);
        double[] GetPositiveSlewRate(string channelList);
        double[] GetPositiveSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList);
        bool[] GetPositiveSlewRateMaximumEnabled(string channelList);
        double[] GetSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList);
        bool[] GetSlewRateTrackingEnabled(string channelList);
        void SetNegativeSlewRate(double slewRate, string channelList);
        void SetNegativeSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList);
        void SetNegativeSlewRateMaximumEnabled(bool maxSlewRateEnabled, string channelList);
        void SetPositiveSlewRate(double slewRate, string channelList);
        void SetPositiveSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList);
        void SetPositiveSlewRateMaximumEnabled(bool maxSlewRateEnabled, string channelList);
        void SetSlewRateTrackingEnabled(bool coupleEnabled, string channelList);
    }

    public interface IKtEL30000OperationConstantResistanceSlew
    {
        double[] GetNegativeSlewRate(string channelList);
        double[] GetNegativeSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList);
        bool[] GetNegativeSlewRateMaximumEnabled(string channelList);
        double[] GetPositiveSlewRate(string channelList);
        double[] GetPositiveSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList);
        bool[] GetPositiveSlewRateMaximumEnabled(string channelList);
        double[] GetSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList);
        bool[] GetSlewRateTrackingEnabled(string channelList);
        void SetNegativeSlewRate(double slewRate, string channelList);
        void SetNegativeSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList);
        void SetNegativeSlewRateMaximumEnabled(bool maxSlewRateEnabled, string channelList);
        void SetPositiveSlewRate(double slewRate, string channelList);
        void SetPositiveSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList);
        void SetPositiveSlewRateMaximumEnabled(bool maxSlewRateEnabled, string channelList);
        void SetSlewRateTrackingEnabled(bool coupleEnabled, string channelList);
    }

    public interface IKtEL30000OperationConstantVoltageSlew
    {
        double[] GetNegativeSlewRate(string channelList);
        double[] GetNegativeSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList);
        bool[] GetNegativeSlewRateMaximumEnabled(string channelList);
        double[] GetPositiveSlewRate(string channelList);
        double[] GetPositiveSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList);
        bool[] GetPositiveSlewRateMaximumEnabled(string channelList);
        double[] GetSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList);
        bool[] GetSlewRateTrackingEnabled(string channelList);
        void SetNegativeSlewRate(double slewRate, string channelList);
        void SetNegativeSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList);
        void SetNegativeSlewRateMaximumEnabled(bool maxSlewRateEnabled, string channelList);
        void SetPositiveSlewRate(double slewRate, string channelList);
        void SetPositiveSlewRate(Keysight.KtEL30000.MinMaxDefMode slewRate, string channelList);
        void SetPositiveSlewRateMaximumEnabled(bool maxSlewRateEnabled, string channelList);
        void SetSlewRateTrackingEnabled(bool coupleEnabled, string channelList);
    }

    public interface IKtEL30000TriggerAcquire
    {
        void Immediate(string channelList);
        void SetCurrent(Keysight.KtEL30000.MinMaxDefMode currentValue, string channelList);
        void SetCurrent(double currentValue, string channelList);
        double[] GetCurrent(string channelList);
        double[] GetCurrent(Keysight.KtEL30000.MinMaxDefMode currentLevel, string channelList);
        double[] GetVoltage(string channelList);
        double[] GetVoltage(Keysight.KtEL30000.MinMaxDefMode levelValue, string channelList);
        void SetVoltage(Keysight.KtEL30000.MinMaxDefMode levelValue, string channelList);
        void SetVoltage(double levelValues, string channelList);
        AcquireTriggerSource[] GetTriggerSource(string channelList);
        void SetTriggerSource(Keysight.KtEL30000.AcquireTriggerSource acquireTriggerSource, string channelList);
        Slope[] GetVoltageSlope(string channelList);
        void SetVoltageSlope(Keysight.KtEL30000.Slope voltageSlope, string channelList);
        void SetCurrentSlope(Keysight.KtEL30000.Slope currentSlope, string channelList);
        Slope[] GetCurrentSlope(string channelList);
    }

    public interface IKtEL30000TriggerTransient
    {
        void Immediate(string channelList);
        TransientTriggerSource[] GetTriggerSource(string channelList);
        void SetTriggerSource(Keysight.KtEL30000.TransientTriggerSource transientTriggerSource, string channelList);
        PrecisionTimeSpan[] GetDelay(string channelList);
        PrecisionTimeSpan[] GetDelay(Keysight.KtEL30000.MinMaxDefMode time, string channelList);
        void SetDelay(Keysight.KtEL30000.MinMaxDefMode time, string channelList);
        void SetDelay(PrecisionTimeSpan time, string channelList);
    }

    public interface IKtEL30000TriggerDlog
    {
        void Immediate();

        Keysight.KtEL30000.TriggerSource TriggerSource { get; set; }
    }

    public interface IKtEL30000Digital
    {
        short InputData { get; }
        short OutputData { get; set; }
        void SetDigitalPinPolarity(int pinIndex, Keysight.KtEL30000.Polarity pinPolarity);
        Keysight.KtEL30000.Polarity GetDigitalPinPolarity(int pinIndex);
        Keysight.KtEL30000.DigitalPinFunctionType GetDigitalPinFunction(int pinIndex);
        void SetDigitalPinFunction(int pinIndex, Keysight.KtEL30000.DigitalPinFunctionType pinFunction);
        bool BusTriggerOutputEnabled { get; set; }
    }

    public interface IKtEL30000DataLog
    {
        void SetCurrentDataLogEnabled(bool currentDataLogEnabled, string channelList);
        void SetVoltageDataLogEnabled(bool voltageDataLogEnabled, string channelList);
        double TriggerOffset { get; set; }

        void SetSampleInterval(Keysight.KtEL30000.MinMaxDefMode sampleIntervalTime);
        PrecisionTimeSpan GetSampleInterval(Keysight.KtEL30000.MinMaxDefMode sampleIntervalTime);
        PrecisionTimeSpan GetSampleTime(Keysight.KtEL30000.MinMaxDefMode sampleTime);
        void SetSampleTime(Keysight.KtEL30000.MinMaxDefMode sampleTime);
        bool[] GetCurrentDataLogEnabled(string channelList);
        bool[] GetVoltageDataLogEnabled(string channelList);
        void Initiate(string filename);
        void Abort();
        double[] Fetch(int numberOfLoggedData, string channelList);
        PrecisionTimeSpan SampleInterval { get; set; }
        PrecisionTimeSpan SampleTime { get; set; }
        bool MinMaxDataLogEnabled { get; set; }
    }

    public interface IKtEL30000Sweep
    {
        int[] GetPoints(Keysight.KtEL30000.MinMaxDefMode sweepPoints, string channelList);
        int[] GetPoints(string channelList);
        void SetPoints(Keysight.KtEL30000.MinMaxDefMode sweepPoints, string channelList);
        void SetPoints(int sweepPoints, string channelList);
        int[] GetOffsetPoints(string channelList);
        int[] GetOffsetPoints(Keysight.KtEL30000.MinMaxDefMode sweepOffsetPoints, string channelList);
        void SetOffsetPoints(Keysight.KtEL30000.MinMaxDefMode sweepOffsetPoints, string channelList);
        void SetOffsetPoints(int sweepOffsetPoints, string channelList);
        PrecisionTimeSpan[] GetTimeInterval(string channelList);
        PrecisionTimeSpan[] GetTimeInterval(Keysight.KtEL30000.MinMaxDefMode sweepTimeInterval, string channelList);
        void SetTimeInterval(Keysight.KtEL30000.MinMaxDefMode sweepTimeInterval, string channelList);
        void SetTimeInterval(PrecisionTimeSpan sweepTimeInterval, string channelList);
    }

    public interface IKtEL30000IOControl
    {
        string ChannelCouple { get; set; }
        PrecisionTimeSpan[] GetRiseTimeDelay(string channelList);
        PrecisionTimeSpan[] GetRiseTimeDelay(Keysight.KtEL30000.MinMaxDefMode riseTimeDelay, string channelList);
        void SetRiseTimeDelay(Keysight.KtEL30000.MinMaxDefMode riseTimeDelay, string channelList);
        void SetRiseTimeDelay(PrecisionTimeSpan riseTimeDelay, string channelList);
        void SetFallTimeDelay(PrecisionTimeSpan fallTimeDelay, string channelList);
        PrecisionTimeSpan[] GetFallTimeDelay(string channelList);
        PrecisionTimeSpan[] GetFallTimeDelay(Keysight.KtEL30000.MinMaxDefMode fallTimeDelay, string channelList);
        void SetFallTimeDelay(Keysight.KtEL30000.MinMaxDefMode fallTimeDelay, string channelList);
        void SetEnabled(bool ioEnabled, string channelList);
        bool[] GetEnabled(string channelList);
        Keysight.KtEL30000.InhibitMode InhibitMode { get; set; }
    }
}
