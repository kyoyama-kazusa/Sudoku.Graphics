namespace Sudoku.ComponentModel;

/// <summary>
/// Represents a repository-wide color instance that can be serialized and deserialized.
/// </summary>
[JsonConverter(typeof(Converter))]
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
	/// Indicates well-known colors, in reversed lookup.
	/// </summary>
	[SuppressMessage("Style", "IDE0028:Simplify collection initialization", Justification = "<Pending>")]
	private static readonly Dictionary<string, SKColor> WellKnownColorsReversed = new(
		from fieldInfo in typeof(SKColors).GetFields(BindingFlags.Public | BindingFlags.Static)
		select KeyValuePair.Create(fieldInfo.Name, (SKColor)fieldInfo.GetValue(null)!)
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
			ColorFormat.HexRgb or ColorFormat.HexRgbShort when $"#{r:X2}{g:X2}{b:X2}" is var t => format switch
			{
				ColorFormat.HexRgbShort when t[1] == t[2] && t[3] == t[4] && t[5] == t[6] => $"#{t[1]}{t[3]}{t[5]}",
				_ => t
			},
			ColorFormat.HexArgb or ColorFormat.HexArgbShort when $"#{a:X2}{r:X2}{g:X2}{b:X2}" is var t => format switch
			{
				ColorFormat.HexArgbShort when t[1] == t[2] && t[3] == t[4] && t[5] == t[6] && t[7] == t[8] => $"#{t[1]}{t[3]}{t[5]}{t[7]}",
				_ => t
			},
			ColorFormat.HexRgba or ColorFormat.HexRgbaShort when $"#{r:X2}{g:X2}{b:X2}{a:X2}" is var t => format switch
			{
				ColorFormat.HexRgbaShort when t[1] == t[2] && t[3] == t[4] && t[5] == t[6] && t[7] == t[8] => $"#{t[1]}{t[3]}{t[5]}{t[7]}",
				_ => t
			},
			ColorFormat.RgbFunction => $"rgb{(r, g, b)}",
			ColorFormat.RgbaFunction => $"rgba({r}, {g}, {b}, {a / 255D:0.000})",
			ColorFormat.NamedColor => WellknownColors.TryGetValue(this, out var colorName) ? colorName : ToString(ColorFormat.HexRgba),
			ColorFormat.HexInteger => $"0x{a:X2}{r:X2}{g:X2}{b:X2}",
			ColorFormat.DecimalInteger => ((uint)a << 24 | (uint)r << 16 | (uint)g << 8 | b).ToString(),
			ColorFormat.AnsiTrueColor => $"\e[38;2;{r};{g};{b}m",
			_ => throw new ArgumentOutOfRangeException(nameof(format))
		};
	}


	/// <inheritdoc cref="IParsable{TSelf}.TryParse(string?, IFormatProvider?, out TSelf)"/>
	public static bool TryParse([NotNullWhen(true)] string? s, out SerializableColor result)
		=> TryParse(s, ColorFormat.HexRgba, out result);

	/// <inheritdoc cref="IParsable{TSelf}.TryParse(string?, IFormatProvider?, out TSelf)"/>
	public static bool TryParse([NotNullWhen(true)] string? s, ColorFormat format, out SerializableColor result)
	{
		if (s is null)
		{
			goto ReturnFalse;
		}

		try
		{
			result = Parse(s, format);
			return true;
		}
		catch (FormatException)
		{
		}

	ReturnFalse:
		result = default;
		return false;
	}

	/// <inheritdoc cref="IParsable{TSelf}.Parse(string, IFormatProvider?)"/>
	public static SerializableColor Parse(string s) => Parse(s, ColorFormat.HexRgba);

	/// <inheritdoc cref="IParsable{TSelf}.Parse(string, IFormatProvider?)"/>
	public static SerializableColor Parse(string s, ColorFormat format)
	{
		return (format, s) switch
		{
			(ColorFormat.TupleRgb, ['(', .. var p, ')'])
				when splitBy(p, ',') is [var r, var g, var b]
				&& int.TryParse(r, out var red) && red is >= 0 and < 256
				&& int.TryParse(g, out var green) && green is >= 0 and < 256
				&& int.TryParse(b, out var blue) && blue is >= 0 and < 256
				=> new((byte)red, (byte)green, (byte)blue),
			(ColorFormat.TupleArgb, ['(', .. var p, ')'])
				when splitBy(p, ',') is [var a, var r, var g, var b]
				&& int.TryParse(a, out var alpha) && alpha is >= 0 and < 256
				&& int.TryParse(r, out var red) && red is >= 0 and < 256
				&& int.TryParse(g, out var green) && green is >= 0 and < 256
				&& int.TryParse(b, out var blue) && blue is >= 0 and < 256
				=> new((byte)red, (byte)green, (byte)blue, (byte)alpha),
			(ColorFormat.TupleRgba, ['(', .. var p, ')'])
				when splitBy(p, ',') is [var r, var g, var b, var a]
				&& int.TryParse(a, out var alpha) && alpha is >= 0 and < 256
				&& int.TryParse(r, out var red) && red is >= 0 and < 256
				&& int.TryParse(g, out var green) && green is >= 0 and < 256
				&& int.TryParse(b, out var blue) && blue is >= 0 and < 256
				=> new((byte)red, (byte)green, (byte)blue, (byte)alpha),
			(ColorFormat.HexRgb or ColorFormat.HexRgbShort, ['#', .. var p]) => p switch
			{
				{ Length: 3 } => rgb(p[0], p[0], p[1], p[1], p[2], p[2]),
				{ Length: 6 } => rgb(s),
				_ => throw ex()
			},
			(ColorFormat.HexArgb or ColorFormat.HexArgbShort, ['#', .. var p]) => p switch
			{
				{ Length: 4 } => argb(p[0], p[0], p[1], p[1], p[2], p[2], p[3], p[3]),
				{ Length: 8 } => argb(p),
				_ => throw ex()
			},
			(ColorFormat.HexRgba or ColorFormat.HexRgbaShort, ['#', .. var p]) => p switch
			{
				{ Length: 4 } => argb(p[3], p[3], p[0], p[0], p[1], p[1], p[2], p[2]),
				{ Length: 8 } => argb(p[6], p[7], p[0], p[1], p[2], p[3], p[4], p[5]),
				_ => throw ex()
			},
			(ColorFormat.RgbFunction, ['R' or 'r', 'G' or 'g', 'B' or 'b', '(', .. var p, ')'])
				when splitBy(p, ',') is [var r, var g, var b]
				&& int.TryParse(r, out var red) && red is >= 0 and < 256
				&& int.TryParse(g, out var green) && green is >= 0 and < 256
				&& int.TryParse(b, out var blue) && blue is >= 0 and < 256
				=> new((byte)red, (byte)green, (byte)blue),
			(ColorFormat.RgbaFunction, ['R' or 'r', 'G' or 'g', 'B' or 'b', 'A' or 'a', '(', .. var p, ')'])
				when splitBy(p, ',') is [var r, var g, var b, var a]
				&& int.TryParse(a, out var alpha) && alpha is >= 0 and < 256
				&& int.TryParse(r, out var red) && red is >= 0 and < 256
				&& int.TryParse(g, out var green) && green is >= 0 and < 256
				&& int.TryParse(b, out var blue) && blue is >= 0 and < 256
				=> new((byte)red, (byte)green, (byte)blue, (byte)alpha),
			(ColorFormat.NamedColor, _) when WellKnownColorsReversed.TryGetValue(s, out var color) => color,
			(ColorFormat.HexInteger, ['0', 'X' or 'x', .. var p]) when Convert.ToUInt32(p, 16) is var mask
				=> new((byte)(mask >> 16 & 255), (byte)(mask >> 8 & 255), (byte)(mask & 255), (byte)(mask >> 24 & 255)),
			(ColorFormat.DecimalInteger, _) when uint.TryParse(s, out var mask)
				=> new((byte)(mask >> 16 & 255), (byte)(mask >> 8 & 255), (byte)(mask & 255), (byte)(mask >> 24 & 255)),
			(ColorFormat.AnsiTrueColor, _)
				when splitBy(s["\e[38;2;".Length..^1], ';') is [var r, var g, var b]
				&& int.TryParse(r, out var red) && red is >= 0 and < 256
				&& int.TryParse(g, out var green) && green is >= 0 and < 256
				&& int.TryParse(b, out var blue) && blue is >= 0 and < 256
				=> new((byte)red, (byte)green, (byte)blue),
			_ => throw ex()
		};


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		static string[] splitBy(string p, char c) => p.Split(c, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

		static SerializableColor rgb(params ReadOnlySpan<char> s)
		{
			var rr = $"{s[0]}{s[1]}";
			var gg = $"{s[2]}{s[3]}";
			var bb = $"{s[4]}{s[5]}";
			return new(Convert.ToByte(rr, 16), Convert.ToByte(gg, 16), Convert.ToByte(bb, 16));
		}

		static SerializableColor argb(params ReadOnlySpan<char> s)
		{
			var aa = $"{s[0]}{s[1]}";
			var rr = $"{s[2]}{s[3]}";
			var gg = $"{s[4]}{s[5]}";
			var bb = $"{s[6]}{s[7]}";
			return new(Convert.ToByte(rr, 16), Convert.ToByte(gg, 16), Convert.ToByte(bb, 16), Convert.ToByte(aa, 16));
		}

		static FormatException ex() => new("The specified format cannot be recognized.");
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

/// <summary>
/// Represents JSON converter of <see cref="SerializableColor"/>.
/// </summary>
/// <seealso cref="SerializableColor"/>
file sealed class Converter : JsonConverter<SerializableColor>
{
	/// <inheritdoc/>
	public override SerializableColor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		=> SerializableColor.Parse(reader.GetString()!, ColorFormat.HexRgba);

	/// <inheritdoc/>
	public override void Write(Utf8JsonWriter writer, SerializableColor value, JsonSerializerOptions options)
		=> writer.WriteStringValue(value.ToString(ColorFormat.HexRgba));
}
