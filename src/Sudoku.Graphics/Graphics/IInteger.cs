namespace Sudoku.Graphics;

/// <summary>
/// Represents a type that simply encapsulates an <see cref="int"/> value.
/// </summary>
/// <typeparam name="TSelf">The type itself.</typeparam>
public partial interface IInteger<TSelf> : IComparable<TSelf>, IEquatable<TSelf> where TSelf : struct, IInteger<TSelf>
{
	/// <summary>
	/// Indicates whether the value is invalid or not (i.e. value is negative).
	/// </summary>
	bool IsInvalid { get; }

	/// <summary>
	/// Indicates the value.
	/// </summary>
	int Value { get; }


	/// <summary>
	/// Implicit cast from <see cref="int"/> to <typeparamref name="TSelf"/> value.
	/// </summary>
	/// <param name="value">The value.</param>
	static abstract implicit operator int(TSelf value);

	/// <summary>
	/// Implicit cast from <typeparamref name="TSelf"/> to <see cref="int"/> value.
	/// </summary>
	/// <param name="value">The value.</param>
	static abstract implicit operator TSelf(int value);
}
