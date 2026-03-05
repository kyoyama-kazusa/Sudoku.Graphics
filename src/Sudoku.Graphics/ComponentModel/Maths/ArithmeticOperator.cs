namespace Sudoku.ComponentModel.Maths;

/// <summary>
/// Represents an arithmetic operator.
/// </summary>
public enum ArithmeticOperator
{
	/// <summary>
	/// Represents placeholder of this type.
	/// </summary>
	None = 0,

	/// <summary>
	/// Indicates add operator (+).
	/// </summary>
	Add,

	/// <summary>
	/// Indicates subtract operator (-).
	/// </summary>
	Subtract,

	/// <summary>
	/// Indicates multiply operator (&#215;).
	/// </summary>
	Multiply_Cross,

	/// <summary>
	/// Indicates multiply operator (&#183;).
	/// </summary>
	Multiply_Dot,

	/// <summary>
	/// Indicates multiply operator (*).
	/// </summary>
	Multiply_Asterisk,

	/// <summary>
	/// Indicates division operator (&#247;).
	/// </summary>
	Division_DivisionSign,

	/// <summary>
	/// Indicates division operator (/).
	/// </summary>
	Division_Slash,

	/// <summary>
	/// Indicates division operator (:).
	/// </summary>
	Division_Ratio,

	/// <summary>
	/// Indicates modulo operator (%).
	/// </summary>
	Modulo,
}
