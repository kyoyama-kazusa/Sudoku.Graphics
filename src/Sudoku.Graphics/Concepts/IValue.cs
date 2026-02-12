namespace Sudoku.Concepts;

/// <summary>
/// Represents a type that simply encapsulates an <see cref="int"/> value.
/// </summary>
/// <typeparam name="TSelf">The type itself.</typeparam>
public interface IValue<TSelf> where TSelf : struct, IValue<TSelf>, allows ref struct
{
	/// <summary>
	/// Indicates the value.
	/// </summary>
	public int Value { get; }

	/// <summary>
	/// Indicates the reference to field.
	/// </summary>
	[UnscopedRef]
	public ref readonly int ValueRef { get; }


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
