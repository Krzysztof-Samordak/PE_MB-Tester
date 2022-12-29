//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.10
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace Keysight.KtEL30000.Bridge {
[System.CodeDom.Compiler.GeneratedCode("swig","3.0")]

internal class KtEL30000Sweep : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnBase;

  internal KtEL30000Sweep(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwnBase = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(KtEL30000Sweep obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~KtEL30000Sweep() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnBase) {
          swigCMemOwnBase = false;
          KtEL30000CppApiPINVOKE.delete_KtEL30000Sweep(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

    public System.Int32[] GetPoints(Keysight.KtEL30000.MinMaxDefMode sweepPoints, string channelList)
    {
        var ret = default(System.Int32[]);
        this.GetPoints(sweepPoints, channelList, ref ret);
        return ret;
    }
    public System.Int32[] GetPoints(string channelList)
    {
        var ret = default(System.Int32[]);
        this.GetPoints(channelList, ref ret);
        return ret;
    }
    public System.Int32[] GetOffsetPoints(string channelList)
    {
        var ret = default(System.Int32[]);
        this.GetOffsetPoints(channelList, ref ret);
        return ret;
    }
    public System.Int32[] GetOffsetPoints(Keysight.KtEL30000.MinMaxDefMode sweepOffsetPoints, string channelList)
    {
        var ret = default(System.Int32[]);
        this.GetOffsetPoints(sweepOffsetPoints, channelList, ref ret);
        return ret;
    }
    public Ivi.Driver.PrecisionTimeSpan[] GetTimeInterval(string channelList)
    {
        var ret = default(Ivi.Driver.PrecisionTimeSpan[]);
        this.GetTimeInterval(channelList, out ret);
        return ret;
    }
    public Ivi.Driver.PrecisionTimeSpan[] GetTimeInterval(Keysight.KtEL30000.MinMaxDefMode sweepTimeInterval, string channelList)
    {
        var ret = default(Ivi.Driver.PrecisionTimeSpan[]);
        this.GetTimeInterval(sweepTimeInterval, channelList, out ret);
        return ret;
    }

  public void GetPoints(Keysight.KtEL30000.MinMaxDefMode sweepPoints, string channelList, ref System.Int32[] INOUT) {
    var marshaler_INOUT = new Keysight.ApiCoreLibraries.BlittableArrayMarshaler<System.Int32>();
    var marshalBuffer_INOUT = new Keysight.ApiCoreLibraries.MarshalBuffer(marshaler_INOUT.BufferSize);
    marshaler_INOUT.CSharpToBytes(INOUT, marshalBuffer_INOUT);
    try {
      KtEL30000CppApiPINVOKE.KtEL30000Sweep_GetPoints__SWIG_0(swigCPtr, (int)sweepPoints, channelList, marshalBuffer_INOUT.PinnedBytes);
      if (KtEL30000CppApiPINVOKE.SWIGPendingException.Pending) throw KtEL30000CppApiPINVOKE.SWIGPendingException.Retrieve();
    } finally {
    marshalBuffer_INOUT.ResetOffset();
    marshaler_INOUT.BytesToCSharp(marshalBuffer_INOUT, ref INOUT);
    marshalBuffer_INOUT.Dispose();
    }
  }

  public void GetPoints(string channelList, ref System.Int32[] INOUT) {
    var marshaler_INOUT = new Keysight.ApiCoreLibraries.BlittableArrayMarshaler<System.Int32>();
    var marshalBuffer_INOUT = new Keysight.ApiCoreLibraries.MarshalBuffer(marshaler_INOUT.BufferSize);
    marshaler_INOUT.CSharpToBytes(INOUT, marshalBuffer_INOUT);
    try {
      KtEL30000CppApiPINVOKE.KtEL30000Sweep_GetPoints__SWIG_1(swigCPtr, channelList, marshalBuffer_INOUT.PinnedBytes);
      if (KtEL30000CppApiPINVOKE.SWIGPendingException.Pending) throw KtEL30000CppApiPINVOKE.SWIGPendingException.Retrieve();
    } finally {
    marshalBuffer_INOUT.ResetOffset();
    marshaler_INOUT.BytesToCSharp(marshalBuffer_INOUT, ref INOUT);
    marshalBuffer_INOUT.Dispose();
    }
  }

  public void SetPoints(Keysight.KtEL30000.MinMaxDefMode sweepPoints, string channelList) {
    KtEL30000CppApiPINVOKE.KtEL30000Sweep_SetPoints__SWIG_0(swigCPtr, (int)sweepPoints, channelList);
    if (KtEL30000CppApiPINVOKE.SWIGPendingException.Pending) throw KtEL30000CppApiPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetPoints(int sweepPoints, string channelList) {
    KtEL30000CppApiPINVOKE.KtEL30000Sweep_SetPoints__SWIG_1(swigCPtr, sweepPoints, channelList);
    if (KtEL30000CppApiPINVOKE.SWIGPendingException.Pending) throw KtEL30000CppApiPINVOKE.SWIGPendingException.Retrieve();
  }

  public void GetOffsetPoints(string channelList, ref System.Int32[] INOUT) {
    var marshaler_INOUT = new Keysight.ApiCoreLibraries.BlittableArrayMarshaler<System.Int32>();
    var marshalBuffer_INOUT = new Keysight.ApiCoreLibraries.MarshalBuffer(marshaler_INOUT.BufferSize);
    marshaler_INOUT.CSharpToBytes(INOUT, marshalBuffer_INOUT);
    try {
      KtEL30000CppApiPINVOKE.KtEL30000Sweep_GetOffsetPoints__SWIG_0(swigCPtr, channelList, marshalBuffer_INOUT.PinnedBytes);
      if (KtEL30000CppApiPINVOKE.SWIGPendingException.Pending) throw KtEL30000CppApiPINVOKE.SWIGPendingException.Retrieve();
    } finally {
    marshalBuffer_INOUT.ResetOffset();
    marshaler_INOUT.BytesToCSharp(marshalBuffer_INOUT, ref INOUT);
    marshalBuffer_INOUT.Dispose();
    }
  }

  public void GetOffsetPoints(Keysight.KtEL30000.MinMaxDefMode sweepOffsetPoints, string channelList, ref System.Int32[] INOUT) {
    var marshaler_INOUT = new Keysight.ApiCoreLibraries.BlittableArrayMarshaler<System.Int32>();
    var marshalBuffer_INOUT = new Keysight.ApiCoreLibraries.MarshalBuffer(marshaler_INOUT.BufferSize);
    marshaler_INOUT.CSharpToBytes(INOUT, marshalBuffer_INOUT);
    try {
      KtEL30000CppApiPINVOKE.KtEL30000Sweep_GetOffsetPoints__SWIG_1(swigCPtr, (int)sweepOffsetPoints, channelList, marshalBuffer_INOUT.PinnedBytes);
      if (KtEL30000CppApiPINVOKE.SWIGPendingException.Pending) throw KtEL30000CppApiPINVOKE.SWIGPendingException.Retrieve();
    } finally {
    marshalBuffer_INOUT.ResetOffset();
    marshaler_INOUT.BytesToCSharp(marshalBuffer_INOUT, ref INOUT);
    marshalBuffer_INOUT.Dispose();
    }
  }

  public void SetOffsetPoints(Keysight.KtEL30000.MinMaxDefMode sweepOffsetPoints, string channelList) {
    KtEL30000CppApiPINVOKE.KtEL30000Sweep_SetOffsetPoints__SWIG_0(swigCPtr, (int)sweepOffsetPoints, channelList);
    if (KtEL30000CppApiPINVOKE.SWIGPendingException.Pending) throw KtEL30000CppApiPINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetOffsetPoints(int sweepOffsetPoints, string channelList) {
    KtEL30000CppApiPINVOKE.KtEL30000Sweep_SetOffsetPoints__SWIG_1(swigCPtr, sweepOffsetPoints, channelList);
    if (KtEL30000CppApiPINVOKE.SWIGPendingException.Pending) throw KtEL30000CppApiPINVOKE.SWIGPendingException.Retrieve();
  }

  public void GetTimeInterval(string channelList, out Ivi.Driver.PrecisionTimeSpan[] OUTPUT) {
    var marshaler_OUTPUT = new Keysight.KtEL30000.NonBlittableArrayMarshaler<Ivi.Driver.PrecisionTimeSpan , Keysight.ApiCoreLibraries.PrecisionTimeSpanMarshaler>();
    var marshalBuffer_OUTPUT = new Keysight.ApiCoreLibraries.MarshalBuffer(marshaler_OUTPUT.BufferSize);

    try {
      KtEL30000CppApiPINVOKE.KtEL30000Sweep_GetTimeInterval__SWIG_0(swigCPtr, channelList, marshalBuffer_OUTPUT.PinnedBytes);
      if (KtEL30000CppApiPINVOKE.SWIGPendingException.Pending) throw KtEL30000CppApiPINVOKE.SWIGPendingException.Retrieve();
    } finally {
    OUTPUT = default(Ivi.Driver.PrecisionTimeSpan[]);
    marshalBuffer_OUTPUT.ResetOffset();
    marshaler_OUTPUT.BytesToCSharp(marshalBuffer_OUTPUT, ref OUTPUT);
    marshalBuffer_OUTPUT.Dispose();
    }
  }

  public void GetTimeInterval(Keysight.KtEL30000.MinMaxDefMode sweepTimeInterval, string channelList, out Ivi.Driver.PrecisionTimeSpan[] OUTPUT) {
    var marshaler_OUTPUT = new Keysight.KtEL30000.NonBlittableArrayMarshaler<Ivi.Driver.PrecisionTimeSpan , Keysight.ApiCoreLibraries.PrecisionTimeSpanMarshaler>();
    var marshalBuffer_OUTPUT = new Keysight.ApiCoreLibraries.MarshalBuffer(marshaler_OUTPUT.BufferSize);

    try {
      KtEL30000CppApiPINVOKE.KtEL30000Sweep_GetTimeInterval__SWIG_1(swigCPtr, (int)sweepTimeInterval, channelList, marshalBuffer_OUTPUT.PinnedBytes);
      if (KtEL30000CppApiPINVOKE.SWIGPendingException.Pending) throw KtEL30000CppApiPINVOKE.SWIGPendingException.Retrieve();
    } finally {
    OUTPUT = default(Ivi.Driver.PrecisionTimeSpan[]);
    marshalBuffer_OUTPUT.ResetOffset();
    marshaler_OUTPUT.BytesToCSharp(marshalBuffer_OUTPUT, ref OUTPUT);
    marshalBuffer_OUTPUT.Dispose();
    }
  }

  public void SetTimeInterval(Ivi.Driver.PrecisionTimeSpan sweepTimeInterval, string channelList) {
    var marshaler_sweepTimeInterval = new Keysight.ApiCoreLibraries.PrecisionTimeSpanMarshaler();
    var marshalBuffer_sweepTimeInterval = new Keysight.ApiCoreLibraries.MarshalBuffer(marshaler_sweepTimeInterval.BufferSize);
    marshaler_sweepTimeInterval.CSharpToBytes(sweepTimeInterval, marshalBuffer_sweepTimeInterval);
    try {
      KtEL30000CppApiPINVOKE.KtEL30000Sweep_SetTimeInterval__SWIG_0(swigCPtr, marshalBuffer_sweepTimeInterval.PinnedBytes, channelList);
      if (KtEL30000CppApiPINVOKE.SWIGPendingException.Pending) throw KtEL30000CppApiPINVOKE.SWIGPendingException.Retrieve();
    } finally {
    marshalBuffer_sweepTimeInterval.Dispose();
    }
  }

  public void SetTimeInterval(Keysight.KtEL30000.MinMaxDefMode sweepTimeInterval, string channelList) {
    KtEL30000CppApiPINVOKE.KtEL30000Sweep_SetTimeInterval__SWIG_1(swigCPtr, (int)sweepTimeInterval, channelList);
    if (KtEL30000CppApiPINVOKE.SWIGPendingException.Pending) throw KtEL30000CppApiPINVOKE.SWIGPendingException.Retrieve();
  }

}

}
