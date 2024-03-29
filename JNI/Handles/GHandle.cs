﻿namespace JNI;

/// <summary>
/// Typedefining <see cref="EnvHandle"/>. Indicates that handle is global.
/// </summary>
public unsafe class GHandle : EnvHandle, IDisposable
{
    public GHandle(nint localAddr, nint globalAddr)
    {
        this.localAddr = localAddr;
        this.globalAddr = globalAddr;
    }

    nint localAddr;
    nint globalAddr;

    public override nint Addr { get => globalAddr; set => globalAddr = value; }

    public override Env Env => Env.ThreadEnv;

    /// <summary>
    /// Creates new instance from local addr
    /// </summary>
    public static GHandle Create(nint localAddr)
    {
        var globalAddr = Env.ThreadNativeEnv->NewGlobalRef(localAddr);
        return new(localAddr, globalAddr);
    }

    public void Dispose()
    {
        var e = Native;
        e->DeleteGlobalRef(globalAddr);
        e->DeleteLocalRef(localAddr);
    }
}