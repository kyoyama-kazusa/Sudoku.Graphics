namespace Sudoku.Concepts;

/// <summary>
/// Represents an absolute index.
/// </summary>
/// <param name="value">The value.</param>
public readonly struct Relative(int value) : IEquatable<Relative>, IEqualityOperators<Relative, Relative, bool>
{
	/// <summary>
	/// The backing value.
	/// </summary>
	private readonly int _value = value;


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] object? obj) => obj is Relative comparer && Equals(comparer);

	/// <inheritdoc/>
	public bool Equals(Relative other) => _value == other._value;

	/// <inheritdoc cref="object.GetHashCode"/>
	public override int GetHashCode() => _value;

	/// <inheritdoc cref="object.ToString"/>
	public override string ToString() => _value.ToString();


	/// <summary>
	/// Implicit cast from <see cref="int"/> to <see cref="Relative"/> value.
	/// </summary>
	/// <param name="value">The value.</param>
	public static implicit operator Relative(int value) => new(value);

	/// <summary>
	/// Implicit cast from <see cref="Relative"/> to <see cref="int"/> value.
	/// </summary>
	/// <param name="value">The value.</param>
	public static implicit operator int(Relative value) => value._value;

	/// <summary>
	/// Explicit cast from <see cref="Absolute"/> to <see cref="Relative"/> value.
	/// </summary>
	/// <param name="value">The value.</param>
	public static explicit operator Relative(Absolute value) => (int)value;

	/// <summary>
	/// Explicit cast from <see cref="Relative"/> to <see cref="Absolute"/> value.
	/// </summary>
	/// <param name="value">The value.</param>
	public static explicit operator Absolute(Relative value) => (int)value;


	/// <inheritdoc/>
	public static bool operator ==(Relative left, Relative right) => left.Equals(right);

	/// <inheritdoc/>
	public static bool operator !=(Relative left, Relative right) => !(left == right);
}
