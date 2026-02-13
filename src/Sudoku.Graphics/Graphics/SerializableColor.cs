namespace Sudoku.Graphics;

/// <summary>
/// Represents a repository-wide color instance that can be serialized and deserialized.
/// </summary>
public readonly struct SerializableColor :
	IEquatable<SerializableColor>,
	IEqualityOperators<SerializableColor, SerializableColor, bool>
{
	/// <summary>
	/// Indicates the mask.
	/// </summary>
	private readonly uint _mask;


	/// <summary>
	/// Initializes a <see cref="SerializableColor"/> instance via the specified values of RGBA.
	/// </summary>
	/// <param name="red">The red value.</param>
	/// <param name="green">The green value.</param>
	/// <param name="blue">The blue value.</param>
	/// <param name="alpha">The alpha value. By default it's 255.</param>
	[JsonConstructor]
	public SerializableColor(byte red, byte green, byte blue, byte alpha = 255) :
		this((uint)red << 24 | (uint)green << 16 | (uint)blue << 8 | alpha)
	{
	}

	/// <summary>
	/// Initializes a <see cref="SerializableColor"/> instance via the equivalent <see cref="SKColor"/> instance.
	/// </summary>
	/// <param name="color">The color.</param>
	public SerializableColor(SKColor color) : this(color.Red, color.Green, color.Blue, color.Alpha)
	{
	}

	/// <summary>
	/// Initializes a <see cref="SerializableColor"/> instance via the specified mask.
	/// </summary>
	/// <param name="mask">The mask.</param>
	private SerializableColor(uint mask) => _mask = mask;


	/// <summary>
	/// Indicates red value.
	/// </summary>
	public byte Red => (byte)(_mask >> 24 & 255);

	/// <summary>
	/// Indicates green value.
	/// </summary>
	public byte Green => (byte)(_mask >> 16 & 255);

	/// <summary>
	/// Indicates blue value.
	/// </summary>
	public byte Blue => (byte)(_mask >> 8 & 255);

	/// <summary>
	/// Indicates alpha value.
	/// </summary>
	public byte Alpha => (byte)(_mask & 255);


	/// <summary>
	/// Deconstruct instance into multiple values.
	/// </summary>
	public void Deconstruct(out byte red, out byte green, out byte blue) => (red, green, blue) = (Red, Green, Blue);

	/// <inheritdoc cref="Deconstruct(out byte, out byte, out byte)"/>
	public void Deconstruct(out byte red, out byte green, out byte blue, out byte alpha)
		=> ((red, green, blue), alpha) = (this, Alpha);

	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] object? obj) => obj is SerializableColor comparer && Equals(comparer);

	/// <inheritdoc/>
	public bool Equals(SerializableColor other) => _mask == other._mask;

	/// <inheritdoc cref="object.GetHashCode"/>
	public override int GetHashCode() => _mask.GetHashCode();

	/// <inheritdoc cref="object.ToString"/>
	public override string ToString() => (Red, Green, Blue, Alpha).ToString();


	/// <inheritdoc/>
	public static bool operator ==(SerializableColor left, SerializableColor right) => left.Equals(right);

	/// <inheritdoc/>
	public static bool operator !=(SerializableColor left, SerializableColor right) => !(left == right);


	/// <summary>
	/// Implicit cast from <see cref="SKColor"/> to <see cref="SerializableColor"/>.
	/// </summary>
	/// <param name="color">The original value.</param>
	public static implicit operator SerializableColor(SKColor color) => new(color);

	/// <summary>
	/// Implicit cast from <see cref="SerializableColor"/> to <see cref="SKColor"/>.
	/// </summary>
	/// <param name="color">The original value.</param>
	public static implicit operator SKColor(SerializableColor color) => new(color.Red, color.Green, color.Blue, color.Alpha);
}
