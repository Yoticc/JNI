﻿namespace JNI;
public unsafe abstract class JType : JClass
{
    public JType(EnvHandle handle, string name, string sig, int dim = 0) : base(handle) => Info = new TypeInfo(name, sig, dim);
    public JType(EnvHandle handle, TypeInfo info) : base(handle) => Info = info;
    public JType(EnvHandle handle, string nameAndSig, int dim = 0) : this(handle, nameAndSig, nameAndSig, dim) { }

    public TypeInfo Info;

    public static implicit operator Arg(JType type) => new Arg(type);
    public static implicit operator TypeInfo(JType type) => type.Info;
}

public unsafe class LJType : JType
{
    public LJType(LHandle handle, TypeInfo info) : base(handle, info) { }
    public LJType(LHandle handle, string nameAndSig, int dim = 0) : base(handle, nameAndSig, dim) { }
    public LJType(LHandle handle, string name, string sig, int dim = 0) : base(handle, name, sig, dim) { }
}

public unsafe class GJType : JType
{
    public GJType(GHandle handle, TypeInfo info) : base(handle, info) { }
    public GJType(GHandle handle, string nameAndSig, int dim = 0) : base(handle, nameAndSig, dim) { }
    public GJType(GHandle handle, string name, string sig, int dim = 0) : base(handle, name, sig, dim) { }
}