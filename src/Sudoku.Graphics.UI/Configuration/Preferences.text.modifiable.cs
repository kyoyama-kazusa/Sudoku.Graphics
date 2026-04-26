namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the modifiable font name.
	/// </summary>
	public Inherited<string> ModifiableFontName { get; set; } = Inherited<string>.FromPropertyName(nameof(DefaultFontName));

	/// <summary>
	/// Indicates the modifiable font size scale (related to cell size).
	/// </summary>
	public Inherited<Scale> ModifiableFontSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultFontSizeScale));

	/// <summary>
	/// Indicates modifiable text color.
	/// </summary>
	public Inherited<SerializableColor> ModifiableFontColor { get; set; } = Inherited<SerializableColor>.FromValue(SKColors.Blue);

	/// <summary>
	/// Indicates modifiable font slant.
	/// </summary>
	public Inherited<SKFontStyleSlant> ModifiableFontSlant { get; set; } = Inherited<SKFontStyleSlant>.FromPropertyName(nameof(DefaultFontSlant));

	/// <summary>
	/// Indicates modifiable font width.
	/// </summary>
	public Inherited<SKFontStyleWidth> ModifiableFontWidth { get; set; } = Inherited<SKFontStyleWidth>.FromPropertyName(nameof(DefaultFontWidth));

	/// <summary>
	/// Indicates modifiable font weight.
	/// </summary>
	public Inherited<SKFontStyleWeight> ModifiableFontWeight { get; set; } = Inherited<SKFontStyleWeight>.FromPropertyName(nameof(DefaultFontWeight));
}
