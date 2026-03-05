namespace Sudoku.ComponentModel.Maths;

/// <summary>
/// Represents an operator that will be used in comparison.
/// </summary>
public enum ComparisonOperator
{
	/// <summary>
	/// Represents placeholder of this type.
	/// </summary>
	None = 0,

	/// <summary>
	/// Indicates equals operator (==).
	/// </summary>
	Equals,

	/// <summary>
	/// Indicates inequals operator (!=, &lt;&gt;).
	/// </summary>
	Inequals,

	/// <summary>
	/// Indicates greater than operator (>).
	/// </summary>
	GreaterThan,

	/// <summary>
	/// Indicates greater than or equals operator (&gt;=).
	/// </summary>
	GreaterThanOrEqual,

	/// <summary>
	/// Indicates less than operator (&lt;).
	/// </summary>
	LessThan,

	/// <summary>
	/// Indicates less than or equals operator (&lt;=).
	/// </summary>
	LessThanOrEqual
}
