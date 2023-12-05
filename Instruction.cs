using System.Reflection.Emit;

public struct Instruction 
{
	public OpCode Code { private set; get; }
	public object Operand { private set; get; }

	public override string ToString() => $"{this.Code} {this.Operand}";

	public Instruction(OpCode code, object operand) 
	{
		this.Code = code;
		this.Operand = operand;
	}
}
