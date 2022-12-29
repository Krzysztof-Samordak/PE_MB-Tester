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

internal class WarningKtEventHandler : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  private bool swigCMemOwnBase;

  internal WarningKtEventHandler(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwnBase = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(WarningKtEventHandler obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~WarningKtEventHandler() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwnBase) {
          swigCMemOwnBase = false;
          KtEL30000CppApiPINVOKE.delete_WarningKtEventHandler(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public virtual void OnEvent(IviWarningEventArgs args) {
    KtEL30000CppApiPINVOKE.WarningKtEventHandler_OnEvent(swigCPtr, IviWarningEventArgs.getCPtr(args));
    if (KtEL30000CppApiPINVOKE.SWIGPendingException.Pending) throw KtEL30000CppApiPINVOKE.SWIGPendingException.Retrieve();
  }

  public virtual bool IsSame(WarningKtEventHandler pHandler2) {
    bool ret = KtEL30000CppApiPINVOKE.WarningKtEventHandler_IsSame(swigCPtr, WarningKtEventHandler.getCPtr(pHandler2));
    if (KtEL30000CppApiPINVOKE.SWIGPendingException.Pending) throw KtEL30000CppApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public virtual bool IsOwn() {
    bool ret = (SwigDerivedClassHasMethod("IsOwn", swigMethodTypes2) ? KtEL30000CppApiPINVOKE.WarningKtEventHandler_IsOwnSwigExplicitWarningKtEventHandler(swigCPtr) : KtEL30000CppApiPINVOKE.WarningKtEventHandler_IsOwn(swigCPtr));
    if (KtEL30000CppApiPINVOKE.SWIGPendingException.Pending) throw KtEL30000CppApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public WarningKtEventHandler() : this(KtEL30000CppApiPINVOKE.new_WarningKtEventHandler(), true) {
    if (KtEL30000CppApiPINVOKE.SWIGPendingException.Pending) throw KtEL30000CppApiPINVOKE.SWIGPendingException.Retrieve();
    SwigDirectorConnect();
  }

  private void SwigDirectorConnect() {
    if (SwigDerivedClassHasMethod("OnEvent", swigMethodTypes0))
      swigDelegate0 = new SwigDelegateWarningKtEventHandler_0(SwigDirectorOnEvent);
    if (SwigDerivedClassHasMethod("IsSame", swigMethodTypes1))
      swigDelegate1 = new SwigDelegateWarningKtEventHandler_1(SwigDirectorIsSame);
    if (SwigDerivedClassHasMethod("IsOwn", swigMethodTypes2))
      swigDelegate2 = new SwigDelegateWarningKtEventHandler_2(SwigDirectorIsOwn);
    KtEL30000CppApiPINVOKE.WarningKtEventHandler_director_connect(swigCPtr, swigDelegate0, swigDelegate1, swigDelegate2);
  }

  private bool SwigDerivedClassHasMethod(string methodName, global::System.Type[] methodTypes) {
    global::System.Reflection.MethodInfo methodInfo = this.GetType().GetMethod(methodName, global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic | global::System.Reflection.BindingFlags.Instance, null, methodTypes, null);
    bool hasDerivedMethod = methodInfo.DeclaringType.IsSubclassOf(typeof(WarningKtEventHandler));
    return hasDerivedMethod;
  }

  private void SwigDirectorOnEvent(global::System.IntPtr args) {
    OnEvent(new IviWarningEventArgs(args, false));
  }

  private bool SwigDirectorIsSame(global::System.IntPtr pHandler2) {
    return IsSame(new WarningKtEventHandler(pHandler2, false));
  }

  private bool SwigDirectorIsOwn() {
    return IsOwn();
  }

  public delegate void SwigDelegateWarningKtEventHandler_0(global::System.IntPtr args);
  public delegate bool SwigDelegateWarningKtEventHandler_1(global::System.IntPtr pHandler2);
  public delegate bool SwigDelegateWarningKtEventHandler_2();

  private SwigDelegateWarningKtEventHandler_0 swigDelegate0;
  private SwigDelegateWarningKtEventHandler_1 swigDelegate1;
  private SwigDelegateWarningKtEventHandler_2 swigDelegate2;

  private static global::System.Type[] swigMethodTypes0 = new global::System.Type[] { typeof(IviWarningEventArgs) };
  private static global::System.Type[] swigMethodTypes1 = new global::System.Type[] { typeof(WarningKtEventHandler) };
  private static global::System.Type[] swigMethodTypes2 = new global::System.Type[] {  };
}

}
