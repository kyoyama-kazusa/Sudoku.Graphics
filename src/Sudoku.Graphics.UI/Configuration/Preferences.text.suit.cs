namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the suit font name.
	/// </summary>
	public Inherited<string> SuitFontName { get; set; } = Inherited<string>.FromValue("Arial");

	/// <summary>
	/// Indicates the suit font size scale (related to cell size).
	/// </summary>
	public Inherited<Scale> SuitFontSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultFontSizeScale));

	/// <summary>
	/// Indicates suit text color.
	/// </summary>
	public Inherited<SerializableColor> SuitFontColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(QuestionFontColor));

	/// <summary>
	/// Indicates suit font slant.
	/// </summary>
	public Inherited<SKFontStyleSlant> SuitFontSlant { get; set; } = Inherited<SKFontStyleSlant>.FromPropertyName(nameof(DefaultFontSlant));

	/// <summary>
	/// Indicates suit font width.
	/// </summary>
	public Inherited<SKFontStyleWidth> SuitFontWidth { get; set; } = Inherited<SKFontStyleWidth>.FromPropertyName(nameof(DefaultFontWidth));

	/// <summary>
	/// Indicates suit font weight.
	/// </summary>
	public Inherited<SKFontStyleWeight> SuitFontWeight { get; set; } = Inherited<SKFontStyleWeight>.FromPropertyName(nameof(DefaultFontWeight));
}
