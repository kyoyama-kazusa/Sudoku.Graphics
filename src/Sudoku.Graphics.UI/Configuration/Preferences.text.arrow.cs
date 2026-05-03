namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the cell arrow text font name.
	/// </summary>
	public Inherited<string> CellArrowTextFontName { get; set; } = Inherited<string>.FromValue("JetBrains Mono");

	/// <summary>
	/// Indicates the cell arrow text font size scale (related to cell size).
	/// </summary>
	public Inherited<Scale> CellArrowTextFontSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultFontSizeScale));

	/// <summary>
	/// Indicates cell arrow text text color.
	/// </summary>
	public Inherited<SerializableColor> CellArrowTextFontColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultFontIconColor));

	/// <summary>
	/// Indicates cell arrow text font slant.
	/// </summary>
	public Inherited<SKFontStyleSlant> CellArrowTextFontSlant { get; set; } = Inherited<SKFontStyleSlant>.FromPropertyName(nameof(DefaultFontSlant));

	/// <summary>
	/// Indicates cell arrow text font width.
	/// </summary>
	public Inherited<SKFontStyleWidth> CellArrowTextFontWidth { get; set; } = Inherited<SKFontStyleWidth>.FromPropertyName(nameof(DefaultFontWidth));

	/// <summary>
	/// Indicates cell arrow text font weight.
	/// </summary>
	public Inherited<SKFontStyleWeight> CellArrowTextFontWeight { get; set; } = Inherited<SKFontStyleWeight>.FromPropertyName(nameof(DefaultFontWeight));
}
