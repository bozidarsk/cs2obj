using System;
using System.Runtime.InteropServices;

// namespace System.Runtime.InteropServices 
// {
// 	[System.AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
// 	public sealed class ExternAttribute : Attribute
// 	{
// 		public CallingConvention CallingConvention { set; get; }
// 		public CharSet CharSet { set; get; }
// 	}
// }

public struct Color 
{
	public byte a, r, g, b;

	public uint argb { get => ((uint)(a * 255f) << 24) + ((uint)(r * 255f) << 16) + ((uint)(g * 255f) << 8) + (uint)(b * 255f); }

	public override string ToString() => $"({a}, {r}, {g}, {b})";

	public Color(float r, float g, float b) : this(1f, r, g, b) {}
	public Color(float a, float r, float g, float b) : this((byte)(a * 255f), (byte)(r * 255f), (byte)(g * 255f), (byte)(b * 255f)) {}
	public Color(byte r, byte g, byte b) : this(255, r, g, b) {}
	public Color(byte a, byte r, byte g, byte b) 
	{
		this.a = a;
		this.r = r;
		this.g = g;
		this.b = b;
	}
}

public static class Display 
{
	// [Extern(CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi), DllImport("asm")]
	[DllImport("*")]
	public static extern void SetPixel(uint x, uint y, Color color);

	public static void DrawSquare(uint x, uint y, uint width, uint height, Color color) 
	{
		string test = color.ToString();
		char t = test[0];

		for (uint yy = y; yy <= y + height; yy++) 
		{
			for (uint xx = x; xx <= x + width; xx++) 
			{
				SetPixel(xx, yy, color);
			}
		}
	}
}

public static class Program { private static int Main() => 2; }
