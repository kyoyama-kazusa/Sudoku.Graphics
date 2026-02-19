namespace Sudoku.ComponentModel;

/// <summary>
/// Represents a color format.
/// </summary>
public enum ColorFormat
{
	/// <summary>
	/// Format <c>(Red, Green, Blue)</c>.
	/// </summary>
	TupleRgb,

	/// <summary>
	/// Format <c>(Alpha, Red, Green, Blue)</c>.
	/// </summary>
	TupleArgb,

	/// <summary>
	/// Format <c>(Red, Green, Blue, Alpha)</c>.
	/// </summary>
	TupleRgba,

	/// <summary>
	/// Format <c>#RRGGBB</c>.
	/// </summary>
	HexRgb,

	/// <summary>
	/// Format <c>#RGB</c>.
	/// </summary>
	HexRgbShort,

	/// <summary>
	/// Format <c>#AARRGGBB</c> (.NET style).
	/// </summary>
	HexArgb,

	/// <summary>
	/// Format <c>#ARGB</c>.
	/// </summary>
	HexArgbShort,

	/// <summary>
	/// Format <c>#RRGGBBAA</c> (CSS Level 4).
	/// </summary>
	HexRgba,

	/// <summary>
	/// Format <c>#RGBA</c>.
	/// </summary>
	HexRgbaShort,

	/// <summary>
	/// Format <c>rgb(Red, Green, Blue)</c>.
	/// </summary>
	RgbFunction,

	/// <summary>
	/// Format <c>rgba(Red, Green, Blue, Alpha (float))</c>.
	/// </summary>
	RgbaFunction,

	/// <summary>
	/// Named color format.
	/// </summary>
	NamedColor,

	/// <summary>
	/// Format <c>0xAARRGGBB</c>.
	/// </summary>
	HexInteger,

	/// <summary>
	/// Decimal 32-bit ARGB.
	/// </summary>
	DecimalInteger,

	/// <summary>
	/// Format <c>\e[38;2;255;0;0m</c>.
	/// </summary>
	AnsiTrueColor
}
