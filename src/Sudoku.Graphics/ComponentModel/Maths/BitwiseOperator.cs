namespace Sudoku.ComponentModel.Maths;

/// <summary>
/// Represents bitwise operator.
/// </summary>
public enum BitwiseOperator
{
	/// <summary>
	/// Indicates placeholder.
	/// </summary>
	None = 0,

	/// <summary>
	/// Indicates and operator (&amp;).
	/// </summary>
	And,

	/// <summary>
	/// Indicates or operator (|).
	/// </summary>
	Or,

	/// <summary>
	/// Indicates not operator (~).
	/// </summary>
	Not,

	/// <summary>
	/// Indicates exclusive or operator (^).
	/// </summary>
	ExclusiveOr
}
