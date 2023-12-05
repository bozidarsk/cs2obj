using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq;

// using Mono.Cecil;
// using Mono.Cecil.Cil;

public static class Program 
{
	public static Assembly MainAssembly { private set; get; } = null;
	public static string[] IgnoredAssemblies { private set; get; } = { "mscorlib" };
	public static Dictionary<uint, MemberInfo> MetadataTokens { private set; get; } = new Dictionary<uint, MemberInfo>();

	private static bool TryGetAttribute(MemberInfo member, string type, out object attribute, Assembly assemly = null) 
	{
		object[] attrs = member.GetCustomAttributes((assemly ?? MainAssembly).GetType("System.Runtime.InteropServices.ExternAttribute"), false);
		attribute = (attrs.Length > 0) ? attrs[0] : null;
		return attrs.Length > 0;
	}

	private static void Translate(Assembly assembly) 
	{
		foreach (AssemblyName referece in assembly.GetReferencedAssemblies()) 
		{
			if (Array.IndexOf(IgnoredAssemblies, referece.Name) >= 0) { continue; }
			Translate(Assembly.Load(referece.Name));
		}

		foreach (Type type in assembly.GetTypes()) 
		{
			MetadataTokens.Add((uint)type.MetadataToken, type);

			foreach (MemberInfo member in type.GetMembers()) 
			{
				if (!MetadataTokens.ContainsKey((uint)member.MetadataToken) && !MetadataTokens.ContainsValue(member)) 
				{ MetadataTokens.Add((uint)member.MetadataToken, member); }
			}
		}

		foreach (var x in MetadataTokens) { Console.WriteLine(x); }

		foreach (Type type in assembly.GetTypes()) 
		{
			foreach (MethodInfo method in type.GetMethods()) 
			{
				Console.WriteLine(Convert.ToString(method) + ":");

				if (method.IsDefined(MainAssembly.GetType("System.Runtime.InteropServices.ExternAttribute"), false)) 
				{ continue; }

				foreach (Instruction ins in Convert.ToInstructions(method)) { Console.WriteLine(ins); }
			}
		}
	}

	private static int Main(string[] args) 
	{
		Process process = Process.Start("mcs", "-out:test/out.exe test/test0.cs");
		process.WaitForExit();
		if (process.ExitCode != 0) { return 1; }
		Console.WriteLine(new string('-', 100));

		MainAssembly = Assembly.LoadFile("/home/bozidarsk/Projects/cs2obj/test/out.exe");
		Translate(MainAssembly);

		return 0;
	}
}
