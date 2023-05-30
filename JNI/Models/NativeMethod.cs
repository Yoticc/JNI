﻿using JNI.Internal;
using JNI.Low;
using JNI.Utils;

namespace JNI.Models;
public sealed unsafe class NativeMethod
{
    public NativeMethod(string name, void* funcPtr, TypeInfo retType, params Arg[] args)
    {
        Name = name;
        Sig = SigGen.Method(retType, args);
        FuncPtr = funcPtr;
    }

    public NativeMethod(string name, void* funcPtr, TypeInfo retType, params TypeInfo[] args) : this(name, funcPtr, retType, args.ToArgs()) { }

    public NativeMethod(string name, nint funcAddr, TypeInfo retType, params Arg[] args) : this(name, funcAddr.ToPointer(), retType, args) { }
    
    public NativeMethod(string name, nint funcAddr, TypeInfo retType, params TypeInfo[] args) : this(name, funcAddr.ToPointer(), retType, args.ToArgs()) { }

    public string Name { get; init; }
    public string Sig { get; init; }
    public void* FuncPtr { get; init; }

    public NativeMethod_ ToStruct()
    {
        var nameCo = new CoMem(Name);
        nameCo.MarkAsDisposed();
        var sigCo = new CoMem(Sig);
        sigCo.MarkAsDisposed();
        return new(nameCo.Ptr, sigCo.Ptr, FuncPtr);
    }
}