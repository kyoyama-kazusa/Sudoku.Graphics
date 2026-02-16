namespace Sudoku.Graphics;

/// <summary>
/// Represents text font information.
/// </summary>
public sealed record TextFontInfo : IEqualityOperators<TextFontInfo, TextFontInfo, bool>
{
	/// <summary>
	/// Indicates the name of the font of text.
	/// </summary>
	public required string FontName { get; set; }

	/// <summary>
	/// Indicates the font size of text.
	/// </summary>
	public required Scale FontSize { get; set; }

	/// <summary>
	/// Indicates the weight of text.
	/// </summary>
	public SKFontStyleWeight FontWeight { get; set; } = SKFontStyleWeight.Medium;

	/// <summary>
	/// Indicates the width of text.
	/// </summary>
	public SKFontStyleWidth FontWidth { get; set; } = SKFontStyleWidth.Normal;
}
