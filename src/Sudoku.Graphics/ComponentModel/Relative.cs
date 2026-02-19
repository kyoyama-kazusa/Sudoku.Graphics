namespace Sudoku.ComponentModel;

/// <summary>
/// Represents an <see cref="int"/> value that describes a relative index.
/// </summary>
/// <param name="value">The value.</param>
[JsonConverter(typeof(ValueConverter<Relative>))]
[DebuggerDisplay($$"""{{{nameof(ToString)}}(),nq}""")]
public readonly struct Relative(int value) : IInteger<Relative>
{
	/// <summary>
	/// The backing value.
	/// </summary>
	private readonly int _value = value;


	/// <inheritdoc/>
	int IInteger<Relative>.Value => _value;

	/// <inheritdoc/>
	[UnscopedRef]
	ref readonly int IInteger<Relative>.ValueRef => ref _value;


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] object? obj) => obj is Relative comparer && Equals(comparer);

	/// <inheritdoc/>
	public bool Equals(Relative other) => _value == other._value;

	/// <inheritdoc cref="object.GetHashCode"/>
	public override int GetHashCode() => _value;

	/// <inheritdoc/>
	public int CompareTo(Relative other) => _value.CompareTo(other._value);

	/// <inheritdoc cref="object.ToString"/>
	public override string ToString() => _value.ToString();


	/// <inheritdoc/>
	public static implicit operator Relative(int value) => new(value);

	/// <inheritdoc/>
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

	/// <inheritdoc/>
	public static bool operator >(Relative left, Relative right) => left.CompareTo(right) > 0;

	/// <inheritdoc/>
	public static bool operator >=(Relative left, Relative right) => left.CompareTo(right) >= 0;

	/// <inheritdoc/>
	public static bool operator <(Relative left, Relative right) => left.CompareTo(right) < 0;

	/// <inheritdoc/>
	public static bool operator <=(Relative left, Relative right) => left.CompareTo(right) <= 0;
}
