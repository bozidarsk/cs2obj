using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using System.Linq;

public static class Convert 
{
	private static readonly IEnumerable<OpCode> OpCodes = typeof(OpCodes).GetFields(BindingFlags.Public | BindingFlags.Static).Select(x => (OpCode)x.GetValue(null));

	public static int ToSize(OperandType operandType) 
	{
		switch (operandType) 
		{
			case OperandType.InlineI8:
			case OperandType.InlineR:
				return 8;
			case OperandType.InlineBrTarget:
			case OperandType.InlineField:
			case OperandType.InlineI:
			case OperandType.InlineMethod:
			case OperandType.InlineSig:
			case OperandType.InlineString:
			case OperandType.InlineSwitch:
			case OperandType.InlineTok:
			case OperandType.InlineType:
			case OperandType.ShortInlineR:
				return 4;
			case OperandType.InlineVar:
				return 2;
			case OperandType.ShortInlineBrTarget:
			case OperandType.ShortInlineI:
			case OperandType.ShortInlineVar:
				return 1;
			case OperandType.InlineNone:
				return 0;
		}

		throw new ArgumentOutOfRangeException($"Invalid OperandType '{operandType}'.");
	}

	public static string ToString(MethodInfo method) 
	{
		string label = 
			ToString(method.ReturnType) + 
			"_" + ToString(method.ReflectedType) + 
			"_" + method.Name
		;

		foreach (ParameterInfo param in method.GetParameters()) { label += "_" + ToString(param.ParameterType); }

		return label;
	}

	public static string ToString(Type type) 
	{
		if ((new Dictionary<Type, string>() 
		{
			{ typeof(void), "void" },
			{ typeof(object), "object" },
			{ typeof(string), "string" },
			{ typeof(char), "char" },
			{ typeof(bool), "bool" },
			{ typeof(double), "double" },
			{ typeof(float), "float" },
			{ typeof(ulong), "ulong" },
			{ typeof(long), "long" },
			{ typeof(uint), "uint" },
			{ typeof(int), "int" },
			{ typeof(ushort), "ushort" },
			{ typeof(short), "short" },
			{ typeof(byte), "byte" },
			{ typeof(sbyte), "sbyte" },
		}).TryGetValue(type, out string label)) { return label; }

		return type.FullName
			.Replace(".", "_")
		;
	}

	public static IEnumerable<Instruction> ToInstructions(MethodInfo method) 
	{
		if (method.GetMethodBody() == null) { return new Instruction[] {}; }

		byte[] bytes = method.GetMethodBody().GetILAsByteArray();
		List<Instruction> instructions = new List<Instruction>(bytes.Length);

		for (int i = 0; i < bytes.Length; i++) 
		{
			ushort instruction = bytes[i];
			ulong operand = 0;

			if (bytes[i] == 0xfe) 
			{
				instruction <<= 8;
				instruction |= bytes[++i];
			}

			OpCode code = OpCodes.First(x => instruction == (ushort)x.Value);

			int opt = ToSize(code.OperandType);
			for (int t = 0; t < opt; t++) 
			{
				operand |= (ulong)bytes[++i] << (t * 8);
				// operand |= bytes[++i];
				// operand <<= 8;
			}

			switch (code.OperandType) 
			{
				case OperandType.InlineField:
				case OperandType.InlineMethod:
				case OperandType.InlineType:
					try{
					instructions.Add(new Instruction(code, Program.MetadataTokens[(uint)operand]));
					}catch(Exception e) 
					{
						Console.WriteLine(code);
						Console.WriteLine(ToSize(code.OperandType));
						Console.WriteLine(operand);


						throw e;}
					break;
				default:
					instructions.Add(new Instruction(code, operand));
					break;
			}
		}

		return instructions;
	}
}
