namespace Sudoku.Graphics;

/// <summary>
/// Represents a repository-wide color instance that can be serialized and deserialized.
/// </summary>
public readonly struct SerializableColor :
	IEquatable<SerializableColor>,
	IEqualityOperators<SerializableColor, SerializableColor, bool>
{
	/// <summary>
	/// Indicates well-known colors.
	/// </summary>
	[SuppressMessage("Style", "IDE0028:Simplify collection initialization", Justification = "<Pending>")]
	private static readonly Dictionary<SKColor, string> WellknownColors = new(
		from fieldInfo in typeof(SKColors).GetFields(BindingFlags.Public | BindingFlags.Static)
		select KeyValuePair.Create((SKColor)fieldInfo.GetValue(null)!, fieldInfo.Name)
	);


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
	public void Deconstruct(out byte red, out byte green, out byte blue, out byte alpha) => ((red, green, blue), alpha) = (this, Alpha);

	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] object? obj) => obj is SerializableColor comparer && Equals(comparer);

	/// <inheritdoc/>
	public bool Equals(SerializableColor other) => _mask == other._mask;

	/// <inheritdoc cref="object.GetHashCode"/>
	public override int GetHashCode() => _mask.GetHashCode();

	/// <inheritdoc cref="object.ToString"/>
	public override string ToString() => ToString(ColorFormat.TupleRgba);

	/// <summary>
	/// Converts the current instance into <see cref="string"/> representation.
	/// </summary>
	/// <param name="format">The format.</param>
	/// <returns>The string representation.</returns>
	public string ToString(ColorFormat format)
	{
		var (r, g, b, a) = this;
		return format switch
		{
			ColorFormat.TupleRgb => (r, g, b).ToString(),
			ColorFormat.TupleArgb => (a, r, g, b).ToString(),
			ColorFormat.TupleRgba => (r, g, b, a).ToString(),
			ColorFormat.HexRgb or ColorFormat.HexRgbShort when $"{r:X2}{g:X2}{b:X2}" is var t => format switch
			{
				ColorFormat.HexRgbShort when t[0] == t[1] && t[2] == t[3] && t[4] == t[5] => $"{t[0]}{t[2]}{t[4]}",
				_ => t
			},
			ColorFormat.HexArgb or ColorFormat.HexArgbShort when $"{a:X2}{r:X2}{g:X2}{b:X2}" is var t => format switch
			{
				ColorFormat.HexArgbShort when t[0] == t[1] && t[2] == t[3] && t[4] == t[5] && t[6] == t[7] => $"{t[0]}{t[2]}{t[4]}{t[6]}",
				_ => t
			},
			ColorFormat.HexRgba or ColorFormat.HexRgbaShort when $"{r:X2}{g:X2}{b:X2}{a:X2}" is var t => format switch
			{
				ColorFormat.HexRgbaShort when t[0] == t[1] && t[2] == t[3] && t[4] == t[5] && t[6] == t[7] => $"{t[0]}{t[2]}{t[4]}{t[6]}",
				_ => t
			},
			ColorFormat.RgbFunction => $"rgb{(r, g, b)}",
			ColorFormat.RgbaFunction => $"rgba({r}, {g}, {b}, {a / 255D:0.000})",
			ColorFormat.NamedColor => WellknownColors.TryGetValue(this, out var colorName) ? colorName : ToString(ColorFormat.HexRgba),
			ColorFormat.HexInteger => $"0x{a:X2}{r:X2}{g:X2}{b:X2}",
			ColorFormat.DecimalInteger => (a << 24 | r << 16 | g << 8 | b).ToString(),
			ColorFormat.AnsiTrueColor => $"\e[38;2;{r};{g};{b}m",
			_ => throw new ArgumentOutOfRangeException(nameof(format))
		};
	}


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
