// using System;
// using System.Collections.Generic;
// using System.Reflection;
// using System.Linq;

// using Mono.Cecil;
// using Mono.Cecil.Cil;

// public static class CIL 
// {
// 	private static Stack<ValueType> typeStack = new Stack<ValueType>();

// 	private enum ValueType 
// 	{
// 		Float,
// 		Double,
// 		DWORD,
// 		QWORD,
// 	}

// 	public static string MethodToLabel(MethodDefinition method) 
// 	{
// 		TypeDefinition type = method.DeclaringType;

// 		string label = (TypeToString(type) + "_"+ method.Name)
// 			.Replace(".", "_")
// 			.Replace("`", "_")
// 			.Replace("[", "L")
// 			.Replace("]", "R")
// 			.Replace(",", "C")
// 		;

// 		foreach (ParameterDefinition param in method.Parameters) 
// 		{
// 			label += "_" + TypeToString(param.ParameterType)
// 				.Replace(".", "_")
// 				.Replace("`", "_")
// 				.Replace("[", "L")
// 				.Replace("]", "R")
// 				.Replace(",", "C")
// 			;
// 		}

// 		return label;
// 	}

// 	private static string TypeToString(TypeReference type) 
// 	{
// 		switch (type.ToString()) 
// 		{
// 			case "System.Object":
// 				return "object";
// 			case "System.Char":
// 				return "char";
// 			case "System.String":
// 				return "string";
// 			case "System.Double":
// 				return "double";
// 			case "System.Single":
// 				return "float";
// 			case "System.UInt64":
// 				return "uint64";
// 			case "System.Int64":
// 				return "int64";
// 			case "System.UInt32":
// 				return "uint32";
// 			case "System.Int32":
// 				return "int32";
// 			case "System.UInt16":
// 				return "uint16";
// 			case "System.Int16":
// 				return "int16";
// 			case "System.Byte":
// 				return "uint8";
// 			case "System.SByte":
// 				return "int8";
// 			default:
// 				return type.ToString();
// 		}
// 	}

// 	private static ValueType TypeToValueType(TypeReference type) 
// 	{
// 		switch (type.ToString()) 
// 		{
// 			case "System.Single":
// 				return ValueType.Float;
// 			case "System.Double":
// 				return ValueType.Double;
// 			case "System.UInt64":
// 			case "System.Int64":
// 				return ValueType.QWORD;
// 			case "System.Char":
// 			case "System.UInt32":
// 			case "System.Int32":
// 			case "System.UInt16":
// 			case "System.Int16":
// 			case "System.Byte":
// 			case "System.SByte":
// 				return ValueType.DWORD;
// 			default:
// 				return ValueType.QWORD;
// 		}
// 	}

// 	private static int ValueTypeToSize(ValueType value) 
// 	{
// 		switch (value) 
// 		{
// 			case ValueType.Double:
// 			case ValueType.QWORD:
// 				return 8;
// 			case ValueType.Float:
// 			case ValueType.DWORD:
// 				return 4;
// 			default:
// 				throw new ArgumentException();
// 		}
// 	}

// 	public static IEnumerable<string> Translate(Instruction instruction) 
// 	{
// 		MethodInfo method = typeof(CIL).GetMethod("Translate_" + instruction.OpCode.Name.Replace(".", "_"));

// 		if (method == null) { throw new NotImplementedException($"Translation for instruction '{instruction.OpCode}' not implemented."); }

// 		return (IEnumerable<string>)method.Invoke(null, new object[] { instruction.Operand });
// 	}

// 	private static IEnumerable<string> Translate_add(object operand) 
// 	{
// 		ValueType value0 = typeStack.Pop();
// 		ValueType value1 = typeStack.Pop();
// 		if (value0 != value1) { throw new ArgumentException("add: value0 and value1 must be from the same type"); }

// 		Stack<string> asm = new Stack<string>();
// 		typeStack.Push(value0);
		
// 		switch (value0) 
// 		{
// 			case ValueType.Float:
// 				asm.Push("pop ebx");
// 				asm.Push("movd xmm1, ebx");
// 				asm.Push("pop eax");
// 				asm.Push("movd xmm0, eax");
// 				asm.Push("addss xmm0, xmm1");
// 				asm.Push("movd eax, xmm0");
// 				asm.Push("push eax");
// 				break;
// 			case ValueType.Double:
// 				asm.Push("pop rbx");
// 				asm.Push("movq xmm1, rbx");
// 				asm.Push("pop rax");
// 				asm.Push("movq xmm0, rax");
// 				asm.Push("addsd xmm0, xmm1");
// 				asm.Push("movq rax, xmm0");
// 				asm.Push("push rax");
// 				break;
// 			case ValueType.DWORD:
// 				asm.Push("pop ebx");
// 				asm.Push("pop eax");
// 				asm.Push("add ebx, eax");
// 				asm.Push("push ebx");
// 				break;
// 			case ValueType.QWORD:
// 				asm.Push("pop rbx");
// 				asm.Push("pop rax");
// 				asm.Push("add rbx, rax");
// 				asm.Push("push rbx");
// 				break;
// 			default:
// 				throw new ArgumentException("add: default case");
// 		}

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_add_ovf(object operand) 
// 	{
// 		ValueType value0 = typeStack.Pop();
// 		ValueType value1 = typeStack.Pop();
// 		if (value0 != value1) { throw new ArgumentException("add.ovf: value0 and value1 must be from the same type"); }

// 		Stack<string> asm = new Stack<string>();
// 		typeStack.Push(value0);
		
// 		switch (value0) 
// 		{
// 			case ValueType.DWORD:
// 				asm.Push("pop ebx");
// 				asm.Push("pop eax");
// 				asm.Push("add ebx, eax");
// 				asm.Push("push ebx");
// 				break;
// 			case ValueType.QWORD:
// 				asm.Push("pop rbx");
// 				asm.Push("pop rax");
// 				asm.Push("add rbx, rax");
// 				asm.Push("push rbx");
// 				break;
// 			default:
// 				throw new ArgumentException("add.ovf: default case");
// 		}

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_add_ovf_un(object operand) 
// 	{
// 		ValueType value0 = typeStack.Pop();
// 		ValueType value1 = typeStack.Pop();
// 		if (value0 != value1) { throw new ArgumentException("add.ovf.un: value0 and value1 must be from the same type"); }

// 		Stack<string> asm = new Stack<string>();
// 		typeStack.Push(value0);
		
// 		switch (value0) 
// 		{
// 			case ValueType.DWORD:
// 				asm.Push("pop ebx");
// 				asm.Push("pop eax");
// 				asm.Push("add ebx, eax");
// 				asm.Push("push ebx");
// 				break;
// 			case ValueType.QWORD:
// 				asm.Push("pop rbx");
// 				asm.Push("pop rax");
// 				asm.Push("add rbx, rax");
// 				asm.Push("push rbx");
// 				break;
// 			default:
// 				throw new ArgumentException("add.ovf.un: default case");
// 		}

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_and(object operand) 
// 	{
// 		ValueType value0 = typeStack.Pop();
// 		ValueType value1 = typeStack.Pop();
// 		if (value0 != value1) { throw new ArgumentException("and: value0 and value1 must be from the same type"); }

// 		Stack<string> asm = new Stack<string>();
// 		typeStack.Push(value0);
		
// 		switch (value0) 
// 		{
// 			case ValueType.DWORD:
// 				asm.Push("pop ebx");
// 				asm.Push("pop eax");
// 				asm.Push("and ebx, eax");
// 				asm.Push("push ebx");
// 				break;
// 			case ValueType.QWORD:
// 				asm.Push("pop rbx");
// 				asm.Push("pop rax");
// 				asm.Push("and rbx, rax");
// 				asm.Push("push rbx");
// 				break;
// 			default:
// 				throw new ArgumentException("and: default case");
// 		}

// 		return asm;
// 	}

// 	// arglist

// 	private static IEnumerable<string> Translate_beq(object operand) 
// 	{
// 		ValueType value0 = typeStack.Pop();
// 		ValueType value1 = typeStack.Pop();
// 		if (value0 != value1) { throw new ArgumentException("beq: value0 and value1 must be from the same type"); }

// 		Stack<string> asm = new Stack<string>();
// 		string label = null;
		
// 		switch (value0) 
// 		{
// 			case ValueType.Float:
// 				asm.Push("pop ebx");
// 				asm.Push("movd xmm1, ebx");
// 				asm.Push("pop eax");
// 				asm.Push("movd xmm0, eax");
// 				asm.Push("comiss xmm0, xmm1");
// 				asm.Push($"je {label}");
// 				break;
// 			case ValueType.Double:
// 				asm.Push("pop rbx");
// 				asm.Push("movq xmm1, rbx");
// 				asm.Push("pop rax");
// 				asm.Push("movq xmm0, rax");
// 				asm.Push("comisd xmm0, xmm1");
// 				asm.Push($"je {label}");
// 				break;
// 			case ValueType.DWORD:
// 				asm.Push("pop ebx");
// 				asm.Push("pop eax");
// 				asm.Push("cmp ebx, eax");
// 				asm.Push($"je {label}");
// 				break;
// 			case ValueType.QWORD:
// 				asm.Push("pop rbx");
// 				asm.Push("pop rax");
// 				asm.Push("cmp rbx, rax");
// 				asm.Push($"je {label}");
// 				break;
// 			default:
// 				throw new ArgumentException("beq: default case");
// 		}

// 		return asm;
// 	}

// 	// beq.s

// 	// bge

// 	// bge.s

// 	// bge.un

// 	// bge.un.s

// 	// bgt

// 	// bgt.s

// 	// bgt.un

// 	// bgt.un.s

// 	// ble

// 	// ble.s

// 	// ble.un

// 	// ble.un.s

// 	// blt

// 	// blt.s

// 	// blt.un

// 	// blt.un.s

// 	// bne.un

// 	// bne.un.s

// 	// br

// 	// br.s

// 	// break

// 	// brfalse

// 	// brfalse.s

// 	// brnull

// 	// brnull.s

// 	// brzero

// 	// brzero.s

// 	// brtrue

// 	// brtrue.s

// 	// brinst

// 	// brinst.s

// 	private static IEnumerable<string> Translate_call(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		MethodDefinition method = (MethodDefinition)operand;
// 		string label = MethodToLabel(method);

// 		string[] regs = { null, null, null, null, "eax", null, null, null, "rax" };
// 		string[] args = { "rdi", "rsi", "rdx", "r8", "r9" };

// 		int sum = 0;
// 		bool subSum = false;

// 		for (int i = method.Parameters.Count - 1; i >= 0; i--) 
// 		{
// 			ValueType value = typeStack.Pop();

// 			if (i >= args.Length) 
// 			{
// 				asm.Push($"add rsp, {ValueTypeToSize(value)}");
// 				subSum = true;
// 			}
// 			else 
// 			{
// 				int size = ValueTypeToSize(value);

// 				asm.Push("xor rax, rax");
// 				asm.Push($"pop {regs[size]}");
// 				asm.Push($"mov {args[i]}, rax");

// 				sum += size;
// 			}
// 		}

// 		ValueType returnValue = TypeToValueType(method.ReturnType);

// 		if (subSum) { asm.Push($"sub rsp, {sum}"); }

// 		asm.Push($"call {label}");
// 		asm.Push($"push {regs[ValueTypeToSize(returnValue)]}");
// 		typeStack.Push(returnValue);

// 		return asm;
// 	}

// 	// calli

// 	// ceq

// 	// cgt

// 	// cgt.un

// 	// ckfinite

// 	// clt

// 	// clt.un

// 	// conv.i1

// 	// conv.i2

// 	// conv.i4

// 	// conv.i8

// 	// conv.r4

// 	// conv.r8

// 	// conv.u1

// 	// conv.u2

// 	// conv.u4

// 	// conv.u8

// 	// conv.i

// 	// conv.u

// 	// conv.r.un

// 	// conv.ovf.i1

// 	// conv.ovf.i2

// 	// conv.ovf.i4

// 	// conv.ovf.i8

// 	// conv.ovf.u1

// 	// conv.ovf.u2

// 	// conv.ovf.u4

// 	// conv.ovf.u8

// 	// conv.ovf.i

// 	// conv.ovf.u

// 	// conv.ovf.i1.un

// 	// conv.ovf.i2.un

// 	// conv.ovf.i4.un

// 	// conv.ovf.i8.un

// 	// conv.ovf.u1.un

// 	// conv.ovf.u2.un

// 	// conv.ovf.u4.un

// 	// conv.ovf.u8.un

// 	// conv.ovf.i.un

// 	// conv.ovf.u.un

// 	private static IEnumerable<string> Translate_cpblk(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Pop();
// 		typeStack.Pop();
// 		typeStack.Pop();

// 		asm.Push("pop eax");
// 		asm.Push("pop rsi");
// 		asm.Push("pop rdi");

// 		asm.Push("cmp eax, 0");
// 		asm.Push("je .loop_end");

// 		asm.Push(".loop_start");
// 		asm.Push("mov byte [rdi], byte [rsi]");
// 		asm.Push("inc rdi");
// 		asm.Push("inc rsi");
// 		asm.Push("dec eax");
// 		asm.Push("cmp eax, 0");
// 		asm.Push("jne .loop_start");
// 		asm.Push(".loop_end");

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_div(object operand) 
// 	{
// 		ValueType value0 = typeStack.Pop();
// 		ValueType value1 = typeStack.Pop();
// 		if (value0 != value1) { throw new ArgumentException("div: value0 and value1 must be from the same type"); }

// 		Stack<string> asm = new Stack<string>();
// 		typeStack.Push(value0);
		
// 		switch (value0) 
// 		{
// 			case ValueType.Float:
// 				asm.Push("pop ebx");
// 				asm.Push("movd xmm1, ebx");
// 				asm.Push("pop eax");
// 				asm.Push("movd xmm0, eax");
// 				asm.Push("divss xmm0, xmm1");
// 				asm.Push("movd eax, xmm0");
// 				asm.Push("push eax");
// 				break;
// 			case ValueType.Double:
// 				asm.Push("pop rbx");
// 				asm.Push("movq xmm1, rbx");
// 				asm.Push("pop rax");
// 				asm.Push("movq xmm0, rax");
// 				asm.Push("divsd xmm0, xmm1");
// 				asm.Push("movq rax, xmm0");
// 				asm.Push("push rax");
// 				break;
// 			case ValueType.DWORD:
// 				asm.Push("pop ebx");
// 				asm.Push("pop eax");
// 				asm.Push("div ebx");
// 				asm.Push("push eax");
// 				break;
// 			case ValueType.QWORD:
// 				asm.Push("pop rbx");
// 				asm.Push("pop rax");
// 				asm.Push("div rbx");
// 				asm.Push("push rax");
// 				break;
// 			default:
// 				throw new ArgumentException("div: default case");
// 		}

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_div_un(object operand) 
// 	{
// 		ValueType value0 = typeStack.Pop();
// 		ValueType value1 = typeStack.Pop();
// 		if (value0 != value1) { throw new ArgumentException("div.un: value0 and value1 must be from the same type"); }

// 		Stack<string> asm = new Stack<string>();
// 		typeStack.Push(value0);
		
// 		switch (value0) 
// 		{
// 			case ValueType.DWORD:
// 				asm.Push("pop ebx");
// 				asm.Push("pop eax");
// 				asm.Push("div ebx");
// 				asm.Push("push eax");
// 				break;
// 			case ValueType.QWORD:
// 				asm.Push("pop rbx");
// 				asm.Push("pop rax");
// 				asm.Push("div rbx");
// 				asm.Push("push rax");
// 				break;
// 			default:
// 				throw new ArgumentException("div.un: default case");
// 		}

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_dup(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		ValueType value = typeStack.Pop();
// 		typeStack.Push(value);
// 		typeStack.Push(value);

// 		string[] regs = { null, null, null, null, "eax", null, null, null, "rax" };
// 		string reg = regs[ValueTypeToSize(value)];

// 		asm.Push($"pop {reg}");
// 		asm.Push($"push {reg}");
// 		asm.Push($"push {reg}");

// 		return asm;
// 	}

// 	// endfilter

// 	// endfinaly

// 	private static IEnumerable<string> Translate_initblk(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Pop();
// 		typeStack.Pop();
// 		typeStack.Pop();

// 		asm.Push("pop eax");
// 		asm.Push("pop ebx");
// 		asm.Push("pop rdi");

// 		asm.Push("cmp eax, 0");
// 		asm.Push("je .loop_end");

// 		asm.Push(".loop_start");
// 		asm.Push("mov byte [rdi], bl");
// 		asm.Push("inc rdi");
// 		asm.Push("dec eax");
// 		asm.Push("cmp eax, 0");
// 		asm.Push("jne .loop_start");
// 		asm.Push(".loop_end");

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_jmp(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		MethodDefinition method = (MethodDefinition)operand;
// 		string label = MethodToLabel(method);

// 		asm.Push($"jmp {label}");

// 		return asm;
// 	}

// 	// ldarg

// 	// ldarg.s

// 	// ldarg.0

// 	// ldarg.1

// 	// ldarg.2

// 	// ldarg.3

// 	// ldarga

// 	// ldarga.s

// 	private static IEnumerable<string> Translate_ldc_i4(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Push(ValueType.DWORD);

// 		asm.Push($"mov eax, {(int)operand}");
// 		asm.Push("push eax");

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_ldc_i8(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Push(ValueType.QWORD);

// 		asm.Push($"mov rax, {(long)operand}");
// 		asm.Push("push rax");

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_ldc_r4(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Push(ValueType.Float);

// 		asm.Push($"mov eax, {(float)operand}");
// 		asm.Push("push eax");

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_ldc_r8(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Push(ValueType.Double);

// 		asm.Push($"mov rax, {(double)operand}");
// 		asm.Push("push rax");

// 		return asm;
// 	}

// 	// ldc.i4.0

// 	// ldc.i4.1

// 	// ldc.i4.2

// 	// ldc.i4.3

// 	// ldc.i4.4

// 	// ldc.i4.5

// 	// ldc.i4.6

// 	// ldc.i4.7

// 	// ldc.i4.8

// 	private static IEnumerable<string> Translate_ldc_i4_m1(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Push(ValueType.DWORD);

// 		asm.Push($"mov eax, -1");
// 		asm.Push("push eax");

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_ldc_i4_s(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Push(ValueType.DWORD);

// 		asm.Push($"mov eax, {(sbyte)operand}");
// 		asm.Push("push eax");

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_ldftn(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Push(ValueType.QWORD);

// 		MethodDefinition method = (MethodDefinition)operand;
// 		string label = MethodToLabel(method);

// 		asm.Push($"push {label}");

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_ldind_ref(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Pop();
// 		typeStack.Push(ValueType.QWORD);

// 		asm.Push("pop rsi");
// 		asm.Push("mov rax, qword [rsi]");
// 		asm.Push("push rax");

// 		return asm;
// 	}

// 	// ldind.i

// 	private static IEnumerable<string> Translate_ldind_i1(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Pop();
// 		typeStack.Push(ValueType.DWORD);

// 		asm.Push("pop rsi");
// 		asm.Push("xor rax, rax");
// 		asm.Push("mov al, byte [rsi]");
// 		asm.Push("push eax");

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_ldind_i2(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Pop();
// 		typeStack.Push(ValueType.DWORD);

// 		asm.Push("pop rsi");
// 		asm.Push("xor rax, rax");
// 		asm.Push("mov ax, word [rsi]");
// 		asm.Push("push eax");

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_ldind_i4(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Pop();
// 		typeStack.Push(ValueType.DWORD);

// 		asm.Push("pop rsi");
// 		asm.Push("mov eax, dword [rsi]");
// 		asm.Push("push eax");

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_ldind_i8(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Pop();
// 		typeStack.Push(ValueType.QWORD);

// 		asm.Push("pop rsi");
// 		asm.Push("mov rax, qword [rsi]");
// 		asm.Push("push rax");

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_ldind_u1(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Pop();
// 		typeStack.Push(ValueType.DWORD);

// 		asm.Push("pop rsi");
// 		asm.Push("xor rax, rax");
// 		asm.Push("mov al, byte [rsi]");
// 		asm.Push("push eax");

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_ldind_u2(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Pop();
// 		typeStack.Push(ValueType.DWORD);

// 		asm.Push("pop rsi");
// 		asm.Push("xor rax, rax");
// 		asm.Push("mov ax, word [rsi]");
// 		asm.Push("push eax");

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_ldind_u4(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Pop();
// 		typeStack.Push(ValueType.DWORD);

// 		asm.Push("pop rsi");
// 		asm.Push("mov eax, dword [rsi]");
// 		asm.Push("push eax");

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_ldind_u8(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Pop();
// 		typeStack.Push(ValueType.QWORD);

// 		asm.Push("pop rsi");
// 		asm.Push("mov rax, qword [rsi]");
// 		asm.Push("push rax");

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_ldind_r4(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Pop();
// 		typeStack.Push(ValueType.Float);

// 		asm.Push("pop rsi");
// 		asm.Push("mov eax, dword [rsi]");
// 		asm.Push("push eax");

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_ldind_r8(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Pop();
// 		typeStack.Push(ValueType.Double);

// 		asm.Push("pop rsi");
// 		asm.Push("mov rax, qword [rsi]");
// 		asm.Push("push rax");

// 		return asm;
// 	}

// 	// ldloc

// 	// ldloc.s

// 	// ldloc.0

// 	// ldloc.1

// 	// ldloc.2

// 	// ldloc.3

// 	// ldloca

// 	// ldloca.s

// 	private static IEnumerable<string> Translate_ldnull(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Push(ValueType.QWORD);

// 		asm.Push("xor rax, rax");
// 		asm.Push("push rax");

// 		return asm;
// 	}

// 	// leave

// 	// leave.s

// 	// localloc

// 	private static IEnumerable<string> Translate_mul(object operand) 
// 	{
// 		ValueType value0 = typeStack.Pop();
// 		ValueType value1 = typeStack.Pop();
// 		if (value0 != value1) { throw new ArgumentException("mul: value0 and value1 must be from the same type"); }

// 		Stack<string> asm = new Stack<string>();
// 		typeStack.Push(value0);
		
// 		switch (value0) 
// 		{
// 			case ValueType.Float:
// 				asm.Push("pop ebx");
// 				asm.Push("movd xmm1, ebx");
// 				asm.Push("pop eax");
// 				asm.Push("movd xmm0, eax");
// 				asm.Push("mulss xmm0, xmm1");
// 				asm.Push("movd eax, xmm0");
// 				asm.Push("push eax");
// 				break;
// 			case ValueType.Double:
// 				asm.Push("pop rbx");
// 				asm.Push("movq xmm1, rbx");
// 				asm.Push("pop rax");
// 				asm.Push("movq xmm0, rax");
// 				asm.Push("mulsd xmm0, xmm1");
// 				asm.Push("movq rax, xmm0");
// 				asm.Push("push rax");
// 				break;
// 			case ValueType.DWORD:
// 				asm.Push("pop ebx");
// 				asm.Push("pop eax");
// 				asm.Push("mul ebx");
// 				asm.Push("push eax");
// 				break;
// 			case ValueType.QWORD:
// 				asm.Push("pop rbx");
// 				asm.Push("pop rax");
// 				asm.Push("mul rbx");
// 				asm.Push("push rax");
// 				break;
// 			default:
// 				throw new ArgumentException("mul: default case");
// 		}

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_mul_ovf(object operand) 
// 	{
// 		ValueType value0 = typeStack.Pop();
// 		ValueType value1 = typeStack.Pop();
// 		if (value0 != value1) { throw new ArgumentException("mul.ovf: value0 and value1 must be from the same type"); }

// 		Stack<string> asm = new Stack<string>();
// 		typeStack.Push(value0);
		
// 		switch (value0) 
// 		{
// 			case ValueType.DWORD:
// 				asm.Push("pop ebx");
// 				asm.Push("pop eax");
// 				asm.Push("mul ebx");
// 				asm.Push("push eax");
// 				break;
// 			case ValueType.QWORD:
// 				asm.Push("pop rbx");
// 				asm.Push("pop rax");
// 				asm.Push("mul rbx");
// 				asm.Push("push rax");
// 				break;
// 			default:
// 				throw new ArgumentException("mul.ovf: default case");
// 		}

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_mul_ovf_un(object operand) 
// 	{
// 		ValueType value0 = typeStack.Pop();
// 		ValueType value1 = typeStack.Pop();
// 		if (value0 != value1) { throw new ArgumentException("mul.ovf.un: value0 and value1 must be from the same type"); }

// 		Stack<string> asm = new Stack<string>();
// 		typeStack.Push(value0);
		
// 		switch (value0) 
// 		{
// 			case ValueType.DWORD:
// 				asm.Push("pop ebx");
// 				asm.Push("pop eax");
// 				asm.Push("mul ebx");
// 				asm.Push("push eax");
// 				break;
// 			case ValueType.QWORD:
// 				asm.Push("pop rbx");
// 				asm.Push("pop rax");
// 				asm.Push("mul rbx");
// 				asm.Push("push rax");
// 				break;
// 			default:
// 				throw new ArgumentException("mul.ovf.un: default case");
// 		}

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_neg(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		ValueType value = typeStack.Pop();
// 		typeStack.Push(value);
		
// 		switch (value) 
// 		{
// 			case ValueType.Float:
// 				asm.Push("mov ebx, -1");
// 				asm.Push("movd xmm1, ebx");
// 				asm.Push("pop eax");
// 				asm.Push("movd xmm0, eax");
// 				asm.Push("mulss xmm0, xmm1");
// 				asm.Push("movd eax, xmm0");
// 				asm.Push("push eax");
// 				break;
// 			case ValueType.Double:
// 				asm.Push("mov rbx, -1");
// 				asm.Push("movq xmm1, rbx");
// 				asm.Push("pop rax");
// 				asm.Push("movq xmm0, rax");
// 				asm.Push("mulsd xmm0, xmm1");
// 				asm.Push("movq rax, xmm0");
// 				asm.Push("push rax");
// 				break;
// 			case ValueType.DWORD:
// 				asm.Push("mov ebx, -1");
// 				asm.Push("pop eax");
// 				asm.Push("mul ebx");
// 				asm.Push("push eax");
// 				break;
// 			case ValueType.QWORD:
// 				asm.Push("mov rbx, -1");
// 				asm.Push("pop rax");
// 				asm.Push("mul rbx");
// 				asm.Push("push rax");
// 				break;
// 			default:
// 				throw new ArgumentException("neg: default case");
// 		}

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_nop(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		asm.Push("nop");

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_not(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		ValueType value = typeStack.Pop();
// 		typeStack.Push(value);
		
// 		switch (value) 
// 		{
// 			case ValueType.DWORD:
// 				asm.Push("pop eax");
// 				asm.Push("not eax");
// 				asm.Push("push ebx");
// 				break;
// 			case ValueType.QWORD:
// 				asm.Push("pop rax");
// 				asm.Push("not rax");
// 				asm.Push("push rbx");
// 				break;
// 			default:
// 				throw new ArgumentException("not: default case");
// 		}

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_or(object operor) 
// 	{
// 		ValueType value0 = typeStack.Pop();
// 		ValueType value1 = typeStack.Pop();
// 		if (value0 != value1) { throw new ArgumentException("or: value0 and value1 must be from the same type"); }

// 		Stack<string> asm = new Stack<string>();
// 		typeStack.Push(value0);
		
// 		switch (value0) 
// 		{
// 			case ValueType.DWORD:
// 				asm.Push("pop ebx");
// 				asm.Push("pop eax");
// 				asm.Push("xor ebx, eax");
// 				asm.Push("push ebx");
// 				break;
// 			case ValueType.QWORD:
// 				asm.Push("pop rbx");
// 				asm.Push("pop rax");
// 				asm.Push("xor rbx, rax");
// 				asm.Push("push rbx");
// 				break;
// 			default:
// 				throw new ArgumentException("or: default case");
// 		}

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_pop(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		asm.Push($"add rsp, {ValueTypeToSize(typeStack.Pop())}");

// 		return asm;
// 	}

// 	// rem

// 	// rem.un

// 	private static IEnumerable<string> Translate_ret(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		string[] regs = { null, null, null, null, "eax", null, null, null, "rax" };

// 		ValueType value = typeStack.Pop();
// 		typeStack.Push(value);
// 		string reg = regs[ValueTypeToSize(value)];

// 		asm.Push($"pop {reg}");
// 		asm.Push($"push {reg}");
// 		asm.Push("ret");

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_shl(object operor) 
// 	{
// 		ValueType value0 = typeStack.Pop();
// 		ValueType value1 = typeStack.Pop();
// 		if (value0 != value1) { throw new ArgumentException("shl: value0 and value1 must be from the same type"); }

// 		Stack<string> asm = new Stack<string>();
// 		typeStack.Push(value0);
		
// 		switch (value0) 
// 		{
// 			case ValueType.DWORD:
// 				asm.Push("pop ebx");
// 				asm.Push("pop eax");
// 				asm.Push("shl eax, ebx");
// 				asm.Push("push eax");
// 				break;
// 			case ValueType.QWORD:
// 				asm.Push("pop rbx");
// 				asm.Push("pop rax");
// 				asm.Push("shl rax, rbx");
// 				asm.Push("push rax");
// 				break;
// 			default:
// 				throw new ArgumentException("shl: default case");
// 		}

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_shr(object operor) 
// 	{
// 		ValueType value0 = typeStack.Pop();
// 		ValueType value1 = typeStack.Pop();
// 		if (value0 != value1) { throw new ArgumentException("shr: value0 and value1 must be from the same type"); }

// 		Stack<string> asm = new Stack<string>();
// 		typeStack.Push(value0);
		
// 		switch (value0) 
// 		{
// 			case ValueType.DWORD:
// 				asm.Push("pop ebx");
// 				asm.Push("pop eax");
// 				asm.Push("shr eax, ebx");
// 				asm.Push("push eax");
// 				break;
// 			case ValueType.QWORD:
// 				asm.Push("pop rbx");
// 				asm.Push("pop rax");
// 				asm.Push("shr rax, rbx");
// 				asm.Push("push rax");
// 				break;
// 			default:
// 				throw new ArgumentException("shr: default case");
// 		}

// 		return asm;
// 	}

// 	// shr.u

// 	// starg

// 	// starg.s

// 	// stind.ref

// 	// stind.i

// 	private static IEnumerable<string> Translate_stind_i1(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Pop();
// 		typeStack.Pop();

// 		asm.Push("pop eax");
// 		asm.Push("pop rsi");
// 		asm.Push("mov byte [rsi], al");

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_stind_i2(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Pop();
// 		typeStack.Pop();

// 		asm.Push("pop eax");
// 		asm.Push("pop rsi");
// 		asm.Push("mov word [rsi], ax");

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_stind_i4(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Pop();
// 		typeStack.Pop();

// 		asm.Push("pop eax");
// 		asm.Push("pop rsi");
// 		asm.Push("mov dword [rsi], eax");

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_stind_i8(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Pop();
// 		typeStack.Pop();

// 		asm.Push("pop rax");
// 		asm.Push("pop rsi");
// 		asm.Push("mov qword [rsi], rax");

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_stind_r4(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Pop();
// 		typeStack.Pop();

// 		asm.Push("pop eax");
// 		asm.Push("pop rsi");
// 		asm.Push("mov dword [rsi], eax");

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_stind_r8(object operand) 
// 	{
// 		Stack<string> asm = new Stack<string>();

// 		typeStack.Pop();
// 		typeStack.Pop();

// 		asm.Push("pop rax");
// 		asm.Push("pop rsi");
// 		asm.Push("mov qword [rsi], rax");

// 		return asm;
// 	}

// 	// stloc

// 	// stloc.s

// 	// stloc.0

// 	// stloc.1

// 	// stloc.2

// 	// stloc.3

// 	private static IEnumerable<string> Translate_sub(object operand) 
// 	{
// 		ValueType value0 = typeStack.Pop();
// 		ValueType value1 = typeStack.Pop();
// 		if (value0 != value1) { throw new ArgumentException("sub: value0 and value1 must be from the same type"); }

// 		Stack<string> asm = new Stack<string>();
// 		typeStack.Push(value0);
		
// 		switch (value0) 
// 		{
// 			case ValueType.Float:
// 				asm.Push("pop ebx");
// 				asm.Push("movd xmm1, ebx");
// 				asm.Push("pop eax");
// 				asm.Push("movd xmm0, eax");
// 				asm.Push("subss xmm0, xmm1");
// 				asm.Push("movd eax, xmm0");
// 				asm.Push("push eax");
// 				break;
// 			case ValueType.Double:
// 				asm.Push("pop rbx");
// 				asm.Push("movq xmm1, rbx");
// 				asm.Push("pop rax");
// 				asm.Push("movq xmm0, rax");
// 				asm.Push("subsd xmm0, xmm1");
// 				asm.Push("movq rax, xmm0");
// 				asm.Push("push rax");
// 				break;
// 			case ValueType.DWORD:
// 				asm.Push("pop ebx");
// 				asm.Push("pop eax");
// 				asm.Push("sub ebx, eax");
// 				asm.Push("push ebx");
// 				break;
// 			case ValueType.QWORD:
// 				asm.Push("pop rbx");
// 				asm.Push("pop rax");
// 				asm.Push("sub rbx, rax");
// 				asm.Push("push rbx");
// 				break;
// 			default:
// 				throw new ArgumentException("sub: default case");
// 		}

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_sub_ovf(object operand) 
// 	{
// 		ValueType value0 = typeStack.Pop();
// 		ValueType value1 = typeStack.Pop();
// 		if (value0 != value1) { throw new ArgumentException("sub.ovf: value0 and value1 must be from the same type"); }

// 		Stack<string> asm = new Stack<string>();
// 		typeStack.Push(value0);
		
// 		switch (value0) 
// 		{
// 			case ValueType.DWORD:
// 				asm.Push("pop ebx");
// 				asm.Push("pop eax");
// 				asm.Push("sub ebx, eax");
// 				asm.Push("push ebx");
// 				break;
// 			case ValueType.QWORD:
// 				asm.Push("pop rbx");
// 				asm.Push("pop rax");
// 				asm.Push("sub rbx, rax");
// 				asm.Push("push rbx");
// 				break;
// 			default:
// 				throw new ArgumentException("sub.ovf: default case");
// 		}

// 		return asm;
// 	}

// 	private static IEnumerable<string> Translate_sub_ovf_un(object operand) 
// 	{
// 		ValueType value0 = typeStack.Pop();
// 		ValueType value1 = typeStack.Pop();
// 		if (value0 != value1) { throw new ArgumentException("sub.ovf.un: value0 and value1 must be from the same type"); }

// 		Stack<string> asm = new Stack<string>();
// 		typeStack.Push(value0);
		
// 		switch (value0) 
// 		{
// 			case ValueType.DWORD:
// 				asm.Push("pop ebx");
// 				asm.Push("pop eax");
// 				asm.Push("sub ebx, eax");
// 				asm.Push("push ebx");
// 				break;
// 			case ValueType.QWORD:
// 				asm.Push("pop rbx");
// 				asm.Push("pop rax");
// 				asm.Push("sub rbx, rax");
// 				asm.Push("push rbx");
// 				break;
// 			default:
// 				throw new ArgumentException("sub.ovf.un: default case");
// 		}

// 		return asm;
// 	}

// 	// switch

// 	private static IEnumerable<string> Translate_xor(object operor) 
// 	{
// 		ValueType value0 = typeStack.Pop();
// 		ValueType value1 = typeStack.Pop();
// 		if (value0 != value1) { throw new ArgumentException("xor: value0 and value1 must be from the same type"); }

// 		Stack<string> asm = new Stack<string>();
// 		typeStack.Push(value0);
		
// 		switch (value0) 
// 		{
// 			case ValueType.DWORD:
// 				asm.Push("pop ebx");
// 				asm.Push("pop eax");
// 				asm.Push("xor ebx, eax");
// 				asm.Push("push ebx");
// 				break;
// 			case ValueType.QWORD:
// 				asm.Push("pop rbx");
// 				asm.Push("pop rax");
// 				asm.Push("xor rbx, rax");
// 				asm.Push("push rbx");
// 				break;
// 			default:
// 				throw new ArgumentException("xor: default case");
// 		}

// 		return asm;
// 	}
// }
