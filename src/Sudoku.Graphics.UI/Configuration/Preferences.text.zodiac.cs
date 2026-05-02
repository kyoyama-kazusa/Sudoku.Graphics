namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the zodiac font size scale (related to cell size).
	/// </summary>
	public Inherited<Scale> ZodiacFontSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultFontSizeScale));
}
