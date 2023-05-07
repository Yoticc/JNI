﻿using JNI.Low;
using System.Runtime.InteropServices;

namespace JNI.Internal;
internal sealed unsafe class Interop
{
    [DllImport("jvm")]
    public static extern void JVM_Close();

    [DllImport("jvm")]
    public static extern long JVM_NanoTime();

    [DllImport("jvm", CallingConvention = CallingConvention.StdCall)]
    public static extern int JNI_GetCreatedJavaVMs(JVM_** jvms, int size, int* sizePtr);

    [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern IntPtr GetModuleHandle([MarshalAs(UnmanagedType.LPWStr)] string lpModuleName);

    [DllImport("kernel32", ExactSpelling = true)]
    public static extern int GetModuleHandleW(string filename);

    [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
    public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);


    [DllImport("user32", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);
    public static int MessageBox(object obj) => MessageBox(0, obj.ToString(), "", 0);
}