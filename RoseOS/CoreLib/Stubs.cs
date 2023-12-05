using System.Runtime;

public static class Program 
{
	public static void Main() {}
}

class Stubs {
	[RuntimeExport("__fail_fast")]
	static void FailFast() { while (true) ; }

	[RuntimeExport("memset")]
	static unsafe void MemSet(byte* ptr, int c, int count) {
		for (byte* p = ptr; p < ptr + count; p++)
			*p = (byte)c;
	}
}

namespace System.Runtime.Versioning 
{
	[System.AttributeUsage(System.AttributeTargets.Assembly, AllowMultiple=false, Inherited=false)]
	public sealed class TargetFrameworkAttribute : System.Attribute
	{
		public string? FrameworkDisplayName { get; set; }
		public string FrameworkName { get; }

		public TargetFrameworkAttribute (string frameworkName) 
		{
			this.FrameworkName = frameworkName;
		}
	}
}

namespace System.Reflection 
{
	[System.AttributeUsage(System.AttributeTargets.Assembly, Inherited=false)]
	public sealed class AssemblyCompanyAttribute : System.Attribute
	{
		public string Company { get; }

		public AssemblyCompanyAttribute (string company) 
		{
			this.Company = company;
		}
	}

	[System.AttributeUsage(System.AttributeTargets.Assembly, Inherited=false)]
	public sealed class AssemblyConfigurationAttribute : System.Attribute
	{
		public string Configuration { get; }

		public AssemblyConfigurationAttribute (string configuration) 
		{
			this.Configuration = configuration;
		}
	}

	[System.AttributeUsage(System.AttributeTargets.Assembly, Inherited=false)]
	public sealed class AssemblyFileVersionAttribute : System.Attribute
	{
		public string FileVersion { get; }

		public AssemblyFileVersionAttribute (string fileVersion) 
		{
			this.FileVersion = fileVersion;
		}
	}

	[System.AttributeUsage(System.AttributeTargets.Assembly, Inherited=false)]
	public sealed class AssemblyInformationalVersionAttribute : System.Attribute
	{
		public string InformationalVersion { get; }

		public AssemblyInformationalVersionAttribute (string informationalVersion) 
		{
			this.InformationalVersion = informationalVersion;
		}
	}

	[System.AttributeUsage(System.AttributeTargets.Assembly, Inherited=false)]
	public sealed class AssemblyProductAttribute : System.Attribute
	{
		public string Product { get; }

		public AssemblyProductAttribute (string product) 
		{
			this.Product = product;
		}
	}

	[System.AttributeUsage(System.AttributeTargets.Assembly, Inherited=false)]
	public sealed class AssemblyTitleAttribute : System.Attribute
	{
		public string Title { get; }

		public AssemblyTitleAttribute (string title) 
		{
			this.Title = title;
		}
	}

	[System.AttributeUsage(System.AttributeTargets.Assembly, Inherited=false)]
	public sealed class AssemblyVersionAttribute : System.Attribute
	{
		public string Version { get; }

		public AssemblyVersionAttribute (string version) 
		{
			this.Version = version;
		}
	}
}
