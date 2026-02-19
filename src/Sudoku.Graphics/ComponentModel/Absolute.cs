namespace Sudoku.ComponentModel;

/// <summary>
/// Represents an <see cref="int"/> value that describes an absolute index.
/// </summary>
/// <param name="value">The value.</param>
[JsonConverter(typeof(ValueConverter<Absolute>))]
[DebuggerDisplay($$"""{{{nameof(ToString)}}(),nq}""")]
public readonly struct Absolute(int value) : IInteger<Absolute>
{
	/// <summary>
	/// The backing value.
	/// </summary>
	private readonly int _value = value;


	/// <inheritdoc/>
	int IInteger<Absolute>.Value => _value;

	/// <inheritdoc/>
	[UnscopedRef]
	ref readonly int IInteger<Absolute>.ValueRef => ref _value;


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] object? obj) => obj is Absolute comparer && Equals(comparer);

	/// <inheritdoc/>
	public bool Equals(Absolute other) => _value == other._value;

	/// <inheritdoc cref="object.GetHashCode"/>
	public override int GetHashCode() => _value;

	/// <inheritdoc/>
	public int CompareTo(Absolute other) => _value.CompareTo(other._value);

	/// <inheritdoc cref="object.ToString"/>
	public override string ToString() => _value.ToString();


	/// <inheritdoc/>
	public static implicit operator Absolute(int value) => new(value);

	/// <inheritdoc/>
	public static implicit operator int(Absolute value) => value._value;


	/// <inheritdoc/>
	public static bool operator ==(Absolute left, Absolute right) => left.Equals(right);

	/// <inheritdoc/>
	public static bool operator !=(Absolute left, Absolute right) => !(left == right);

	/// <inheritdoc/>
	public static bool operator >(Absolute left, Absolute right) => left.CompareTo(right) > 0;

	/// <inheritdoc/>
	public static bool operator >=(Absolute left, Absolute right) => left.CompareTo(right) >= 0;

	/// <inheritdoc/>
	public static bool operator <(Absolute left, Absolute right) => left.CompareTo(right) < 0;

	/// <inheritdoc/>
	public static bool operator <=(Absolute left, Absolute right) => left.CompareTo(right) <= 0;
}
