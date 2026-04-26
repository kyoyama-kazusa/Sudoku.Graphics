namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the given font name.
	/// </summary>
	public Inherited<string> GivenFontName { get; set; } = Inherited<string>.FromPropertyName(nameof(DefaultFontName));

	/// <summary>
	/// Indicates the given font size scale (related to cell size).
	/// </summary>
	public Inherited<Scale> GivenFontSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultFontSizeScale));

	/// <summary>
	/// Indicates given text color.
	/// </summary>
	public Inherited<SerializableColor> GivenFontColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultFontColor));

	/// <summary>
	/// Indicates given font slant.
	/// </summary>
	public Inherited<SKFontStyleSlant> GivenFontSlant { get; set; } = Inherited<SKFontStyleSlant>.FromPropertyName(nameof(DefaultFontSlant));

	/// <summary>
	/// Indicates given font width.
	/// </summary>
	public Inherited<SKFontStyleWidth> GivenFontWidth { get; set; } = Inherited<SKFontStyleWidth>.FromPropertyName(nameof(DefaultFontWidth));

	/// <summary>
	/// Indicates given font weight.
	/// </summary>
	public Inherited<SKFontStyleWeight> GivenFontWeight { get; set; } = Inherited<SKFontStyleWeight>.FromPropertyName(nameof(DefaultFontWeight));
}
