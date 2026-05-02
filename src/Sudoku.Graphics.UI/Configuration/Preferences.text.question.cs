namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the question font name.
	/// </summary>
	public Inherited<string> QuestionFontName { get; set; } = Inherited<string>.FromValue("Cascadia Code");

	/// <summary>
	/// Indicates the question font size scale (related to cell size).
	/// </summary>
	public Inherited<Scale> QuestionFontSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultFontSizeScale));

	/// <summary>
	/// Indicates question text color.
	/// </summary>
	public Inherited<SerializableColor> QuestionFontColor { get; set; } = Inherited<SerializableColor>.FromValue(SKColors.Gray);

	/// <summary>
	/// Indicates question font slant.
	/// </summary>
	public Inherited<SKFontStyleSlant> QuestionFontSlant { get; set; } = Inherited<SKFontStyleSlant>.FromPropertyName(nameof(DefaultFontSlant));

	/// <summary>
	/// Indicates question font width.
	/// </summary>
	public Inherited<SKFontStyleWidth> QuestionFontWidth { get; set; } = Inherited<SKFontStyleWidth>.FromPropertyName(nameof(DefaultFontWidth));

	/// <summary>
	/// Indicates question font weight.
	/// </summary>
	public Inherited<SKFontStyleWeight> QuestionFontWeight { get; set; } = Inherited<SKFontStyleWeight>.FromPropertyName(nameof(DefaultFontWeight));
}
