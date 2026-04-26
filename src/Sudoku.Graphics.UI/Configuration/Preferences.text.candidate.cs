namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the candidate font name.
	/// </summary>
	public Inherited<string> CandidateFontName { get; set; } = Inherited<string>.FromPropertyName(nameof(DefaultFontName));

	/// <summary>
	/// Indicates the candidate font size scale (related to candidate size, not cell size).
	/// </summary>
	public Inherited<Scale> CandidateFontSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultFontSizeScale));

	/// <summary>
	/// Indicates candidate text color.
	/// </summary>
	public Inherited<SerializableColor> CandidateFontColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultFontColor));

	/// <summary>
	/// Indicates candidate font slant.
	/// </summary>
	public Inherited<SKFontStyleSlant> CandidateFontSlant { get; set; } = Inherited<SKFontStyleSlant>.FromPropertyName(nameof(DefaultFontSlant));

	/// <summary>
	/// Indicates candidate font width.
	/// </summary>
	public Inherited<SKFontStyleWidth> CandidateFontWidth { get; set; } = Inherited<SKFontStyleWidth>.FromPropertyName(nameof(DefaultFontWidth));

	/// <summary>
	/// Indicates candidate font weight.
	/// </summary>
	public Inherited<SKFontStyleWeight> CandidateFontWeight { get; set; } = Inherited<SKFontStyleWeight>.FromPropertyName(nameof(DefaultFontWeight));
}
