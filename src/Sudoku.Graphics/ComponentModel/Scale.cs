namespace Sudoku.ComponentModel;

/// <summary>
/// Represents a relative-size value (scaling)
/// that supports both <see cref="float"/> and <see cref="decimal"/> literal initialization.
/// </summary>
/// <param name="value">The fact value of a <see cref="float"/>.</param>
/// <exception cref="ArgumentOutOfRangeException">
/// Throws when the argument <paramref name="value"/> is greater than 1 or less than 0.
/// </exception>
[JsonConverter(typeof(Converter))]
public readonly struct Scale(decimal value) :
	IComparable<Scale>,
	IComparisonOperators<Scale, Scale, bool>,
	IEquatable<Scale>,
	IEqualityOperators<Scale, Scale, bool>
{
	/// <summary>
	/// Indicates the value.
	/// </summary>
	public decimal Value { get; } = value is >= 0 and <= 1 ? value : throw new ArgumentOutOfRangeException(nameof(value));


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] object? obj) => obj is Scale comparer && Equals(comparer);

	/// <inheritdoc/>
	public bool Equals(Scale other) => Value == other.Value;

	/// <summary>
	/// Measure the fact value
	/// (i.e. performs the formula <c><paramref name="value"/> * <see langword="this"/>.RatioValue</c>).
	/// </summary>
	/// <param name="value">The original value.</param>
	/// <returns>The target value measured.</returns>
	public float Measure(float value) => (float)((decimal)value * Value);

	/// <inheritdoc/>
	public int CompareTo(Scale other)
	{
		var left = (int)Math.Round(Value * 1E5M);
		var right = (int)Math.Round(other.Value * 1E5M);
		return left.CompareTo(right);
	}

	/// <inheritdoc/>
	public override int GetHashCode() => Value.GetHashCode();

	/// <inheritdoc cref="object.ToString"/>
	public override string ToString() => Value.ToString();

	/// <inheritdoc cref="double.ToString(string?)"/>
	public string ToString(string? format) => Value.ToString(format);


	/// <inheritdoc/>
	public static bool operator ==(Scale left, Scale right) => left.Equals(right);

	/// <inheritdoc/>
	public static bool operator !=(Scale left, Scale right) => !(left == right);

	/// <inheritdoc/>
	public static bool operator >(Scale left, Scale right) => left.CompareTo(right) > 0;

	/// <inheritdoc/>
	public static bool operator >=(Scale left, Scale right) => left.CompareTo(right) >= 0;

	/// <inheritdoc/>
	public static bool operator <(Scale left, Scale right) => left.CompareTo(right) < 0;

	/// <inheritdoc/>
	public static bool operator <=(Scale left, Scale right) => left.CompareTo(right) <= 0;


	/// <summary>
	/// Implicit cast from <see cref="float"/> into <see cref="Scale"/> value.
	/// </summary>
	/// <param name="value">The value.</param>
	public static implicit operator Scale(float value) => new((decimal)value);

	/// <summary>
	/// Implicit cast from <see cref="decimal"/> into <see cref="Scale"/> value.
	/// </summary>
	/// <param name="value">The value.</param>
	public static implicit operator Scale(decimal value) => new(value);


	/// <summary>
	/// Explicit cast from <see cref="Scale"/> into <see cref="float"/> value.
	/// </summary>
	/// <param name="value">The value.</param>
	public static explicit operator float(Scale value) => (float)value.Value;

	/// <summary>
	/// Explicit cast from <see cref="Scale"/> into <see cref="decimal"/> value.
	/// </summary>
	/// <param name="value">The value.</param>
	public static explicit operator decimal(Scale value) => value.Value;
}

/// <summary>
/// Represents a JSON converter for <see cref="Scale"/> instances.
/// </summary>
/// <seealso cref="Scale"/>
file sealed class Converter : JsonConverter<Scale>
{
	/// <inheritdoc/>
	public override Scale Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		reader.Read();
		return reader.GetDecimal();
	}

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, Scale value, JsonSerializerOptions options)
		=> writer.WriteNumberValue(value.Value);
}
