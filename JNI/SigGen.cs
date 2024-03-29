﻿namespace JNI;
public sealed class SigGen
{
    private static string[] baseTypes = new string[]
    {
        "java/lang/Boolean",
        "java/lang/Byte",
        "java/lang/Character",
        "java/lang/Short",
        "java/lang/Integer",
        "java/lang/Long",
        "java/lang/Float",
        "java/lang/Double",
        "java/lang/Void",
    };

    public static string Arg(JType type) => Field(type);
    public static string Arg(TypeInfo info) => Field(info);
    public static string Field(TypeInfo info) => baseTypes.Contains(info.Name) ? $"{Dim(info.Dimension)}{info.Signature}" : $"{Dim(info.Dimension)}L{info.Signature};";
    public static string Field(FieldData field) => field.Signature;
    public static string Method(TypeInfo retType, Arg[] args) =>
        $"({string.Concat(args.Select(a => a.Sig))})" +
        Field(retType);

    public static string Dim(int dim) => new string('[', dim);
    public static string Type(TypeInfo info) => baseTypes.Contains(info.Name) ? info.Signature : $"L{info.Signature};";
}