namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the given font name.
	/// </summary>
	public Inherited<string> ExclamationFontName { get; set; } = Inherited<string>.FromPropertyName(nameof(QuestionFontName));

	/// <summary>
	/// Indicates the given font size scale (related to cell size).
	/// </summary>
	public Inherited<Scale> ExclamationFontSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultFontSizeScale));

	/// <summary>
	/// Indicates given text color.
	/// </summary>
	public Inherited<SerializableColor> ExclamationFontColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(QuestionFontColor));

	/// <summary>
	/// Indicates given font slant.
	/// </summary>
	public Inherited<SKFontStyleSlant> ExclamationFontSlant { get; set; } = Inherited<SKFontStyleSlant>.FromPropertyName(nameof(DefaultFontSlant));

	/// <summary>
	/// Indicates given font width.
	/// </summary>
	public Inherited<SKFontStyleWidth> ExclamationFontWidth { get; set; } = Inherited<SKFontStyleWidth>.FromPropertyName(nameof(DefaultFontWidth));

	/// <summary>
	/// Indicates given font weight.
	/// </summary>
	public Inherited<SKFontStyleWeight> ExclamationFontWeight { get; set; } = Inherited<SKFontStyleWeight>.FromPropertyName(nameof(DefaultFontWeight));
}
