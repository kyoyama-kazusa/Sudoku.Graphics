namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the exclamation font name.
	/// </summary>
	public Inherited<string> ExclamationFontName { get; set; } = Inherited<string>.FromPropertyName(nameof(QuestionFontName));

	/// <summary>
	/// Indicates the exclamation font size scale (related to cell size).
	/// </summary>
	public Inherited<Scale> ExclamationFontSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultFontSizeScale));

	/// <summary>
	/// Indicates exclamation text color.
	/// </summary>
	public Inherited<SerializableColor> ExclamationFontColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultFontIconColor));

	/// <summary>
	/// Indicates exclamation font slant.
	/// </summary>
	public Inherited<SKFontStyleSlant> ExclamationFontSlant { get; set; } = Inherited<SKFontStyleSlant>.FromPropertyName(nameof(DefaultFontSlant));

	/// <summary>
	/// Indicates exclamation font width.
	/// </summary>
	public Inherited<SKFontStyleWidth> ExclamationFontWidth { get; set; } = Inherited<SKFontStyleWidth>.FromPropertyName(nameof(DefaultFontWidth));

	/// <summary>
	/// Indicates exclamation font weight.
	/// </summary>
	public Inherited<SKFontStyleWeight> ExclamationFontWeight { get; set; } = Inherited<SKFontStyleWeight>.FromPropertyName(nameof(DefaultFontWeight));
}
