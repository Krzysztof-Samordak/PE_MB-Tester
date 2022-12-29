using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Resources;
using System.Text;
using MindWorks.Nimbus;
using Keysight.KtEL30000;

namespace Keysight.KtEL30000
{
    public enum StatusByteFlags
    {
        User0 = 1,
        User1 = 2,
        ErrorEventQueueSummary = 4,
        QuestionableSummary = 8,
        MessageAvailable = 16,
        StandardEventSummary = 32,
        RequestServiceSummary = 64,
        OperationSummary = 128,
        None = 0
    }
    public enum StatusStandardEventFlags
    {
        OperationComplete = 1,
        RequestControl = 2,
        QueryError = 4,
        DeviceDependentError = 8,
        ExecutionError = 16,
        CommandError = 32,
        UserRequest = 64,
        PowerOn = 128,
        None = 0
    }
    public enum StatusQuestionableFlags
    {
        Power = 8,
        Frequency = 32,
        Calibration = 256,
        None = 0
    }
    public enum StatusOperationFlags
    {
        CV = 1,
        CP = 8,
        OFF = 16,
        CC = 2,
        CR = 4,
        SH = 32,
        None = 0
    }
    public enum TracingLevel
    {
        Error = 70,
        Debug = 40,
        Info = 50,
        Warning = 60
    }

    public enum OperationMode
    {
        [Command("CURR")]
        ConstantCurrent = 0,
        [Command("VOLT")]
        ConstantVoltage = 1,
        [Command("RES")]
        ConstantResistance = 2,
        [Command("POW")]
        ConstantPower = 3
    }

    public enum ViewMeter
    {
        [Command("METER1")]
        Meter1 = 0,
        [Command("METER2")]
        Meter2 = 1
    }

    public enum MinMaxDefMode
    {
        [Command("MIN")]
        Minimum = 0,
        [Command("MAX")]
        Maximum = 1,
        [Command("DEF")]
        Default = 2,
        [Command("INF")]
        Infinite = 3
    }

    public enum AutoMode
    {
        [Command("AUTO")]
        Auto = 0,
        [Command("ONCE")]
        Once = 1
    }

    public enum OverCurrentProtectionDelayTimerMode
    {
        [Command("SCH")]
        SettingChange = 0,
        [Command("CCTR")]
        ConstantCurrentTransition = 1
    }

    public enum VoltageMode
    {
        Fixed = 0,
        Step = 1,
        List = 2
    }

    public enum CurrentMode
    {
        [Command("FIX")]
        Fixed = 0,
        [Command("STEP")]
        Step = 1,
        [Command("LIST")]
        List = 2
    }

    public enum PowerMode
    {
        Fixed = 0,
        Step = 1,
        List = 2
    }

    public enum ResistanceMode
    {
        Fixed = 0,
        Step = 1,
        List = 2
    }

    public enum TransientMode
    {
        [Command("CONT")]
        Continuous = 0,
        [Command("PULS")]
        Pulse = 1,
        [Command("TOGG")]
        Toggle = 2,
        [Command("LIST")]
        List = 3
    }

    public enum RangeType
    {
        [Command("LOW")]
        Low = 0,
        [Command("MED")]
        Medium = 1,
        [Command("HIGH")]
        High = 2
    }

    public enum SenseMode
    {
        [Command("INT")]
        Internal = 0,
        [Command("EXT")]
        External = 1
    }

    public enum AcquireTriggerSource
    {
        [Command("IMM")]
        Immediate = 0,
        [Command("EXT")]
        External = 1,
        [Command("BUS")]
        Bus = 2,
        [Command("CURR1")]
        CurrentChannel1 = 3,
        [Command("CURR2")]
        CurrentChannel2 = 4,
        [Command("VOLT1")]
        VoltageChannel1 = 5,
        [Command("VOLT2")]
        VoltageChannel2 = 6
    }

    public enum Slope
    {
        [Command("POS")]
        Positive = 0,
        [Command("NEG")]
        Negative = 1
    }

    public enum TransientTriggerSource
    {
        [Command("IMM")]
        Immediate = 0,
        [Command("EXT")]
        External = 1,
        [Command("BUS")]
        Bus = 2,
        [Command("PIN")]
        Pin1 = 3,
        [Command("PIN2")]
        Pin2 = 4,
        [Command("PIN3")]
        Pin3 = 5
    }

    public enum TriggerSource
    {
        [Command("IMM")]
        Immediate = 0,
        [Command("EXT")]
        External = 1,
        [Command("BUS")]
        Bus = 2
    }

    public enum ListTriggerSignalOutputMode
    {
        [Command("BOST")]
        BeginingOfStep = 0,
        [Command("EOST")]
        EndOfStep = 1
    }

    public enum InhibitMode
    {
        [Command("LATC")]
        Latching = 0,
        [Command("LIVE")]
        Live = 1,
        [Command("OFF")]
        Off = 2
    }

    public enum Polarity
    {
        [Command("POS")]
        Positive = 0,
        [Command("NEG")]
        Negative = 1
    }

    public enum DigitalPinFunctionType
    {
        [Command("DIO")]
        DigitalIO = 0,
        [Command("DINP")]
        DigitalInput = 1,
        [Command("TINP")]
        TriggerInput = 2,
        [Command("TOUT")]
        TriggerOutput = 3,
        [Command("FAUL")]
        Fault = 4,
        [Command("INH")]
        Inhibit = 5,
        [Command("ONC")]
        OnCouple = 6,
        [Command("OFFC")]
        OffCouple = 7
    }

    public enum MeasurementFunction
    {
        [Command("VOLT")]
        Voltage = 0,
        [Command("CURR")]
        Current = 1,
        Power = 2
    }

    public enum CalibrationCurrentLevel
    {
        Point1 = 0,
        Point2 = 1,
        Point3 = 2,
        Point4 = 3,
        Point5 = 4
    }

    public enum MeasurementType
    {
        [Command("ACDC")]
        RMS = 0,
        [Command("DC")]
        Average = 1,
        [Command("MAX")]
        Maximum = 2,
        [Command("MIN")]
        Minimum = 3
    }

    public enum PairMode
    {
        [Command("OFF")]
        Off = 0,
        [Command("PAR")]
        Parallel = 1
    }

    public enum PowerOnStateMode
    {
        RST = 0,
        RCL0 = 1,
        RCL1 = 2,
        RCL2 = 3,
        RCL3 = 4,
        RCL4 = 5,
        RCL5 = 6,
        RCL6 = 7,
        RCL7 = 8,
        RCL8 = 9
    }
}
