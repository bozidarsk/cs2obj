using System;

namespace System.Runtime.InteropServices 
{
	[System.AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public sealed class ExternAttribute : Attribute
	{
		public CallingConvention CallingConvention { set; get; }
		public CharSet CharSet { set; get; }
	}
}
