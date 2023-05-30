﻿using JNI.Low;
using JNI.Models;
using System.Runtime.InteropServices;

namespace JNI.Internal;
internal static unsafe class SugarExtensions
{
    public static T ToStruct<T>(this nint addr) where T : struct => Marshal.PtrToStructure<T>(addr);

    public static Arg[] ToArgs(this TypeInfo[] arr)
    {
        var ret = new Arg[arr.Length];
        for (int i = 0; i < arr.Length; i++)
            ret[i] = new Arg(arr[i]);
        return ret;
    }

    public static T* Pin<T>(this T obj) where T : unmanaged
    {
        var addr = (T*)Marshal.AllocCoTaskMem(sizeof(T));
        *addr = obj;
        return addr;
    }

    public static T* Pin<T>(this T[] arr) where T : unmanaged
    {
        var addr = (T*)Marshal.AllocCoTaskMem(sizeof(T) * arr.Length);
        fixed (T* ptr = arr)
            for (int i = 0; i < arr.Length; i++)
                addr[i] = arr[i];
        return addr;
    }

    public static NativeMethod_[] ToStructs(this NativeMethod[] arr)
    {
        var result = new NativeMethod_[arr.Length];
        for (int i = 0; i < arr.Length; i++)
            result[i] = arr[i].ToStruct();
        return result;
    }

    public static T[] ToArr<T>(this T[] arr, T* ptr) where T : unmanaged
    {
        for (int i = 0; i < arr.Length; i++)
            arr[i] = *(ptr + i);
        return arr;
    }

    public static string AsLetterHex(this nint addr, char letter) => $"0{letter}" + addr.ToInt64().ToString("X");

    public static string TransformHex(this string hex) => "0x" + (string.IsNullOrEmpty(hex) ? "0" : hex);

    public static string AsHex(this nint addr) => addr.ToInt64().ToString("X").TransformHex();

    public static string AsJavaPath(this string path) => path.Replace('.', '/');
}