namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates default cell pair text font name.
	/// </summary>
	public Inherited<string> DefaultCellPairTextFontName { get; set; } = Inherited<string>.FromValue("Consolas");

	/// <summary>
	/// Indicates default cell pair text font slant.
	/// </summary>
	public Inherited<SKFontStyleSlant> DefaultCellPairTextFontSlant { get; set; } = Inherited<SKFontStyleSlant>.FromPropertyName(nameof(DefaultFontSlant));

	/// <summary>
	/// Indicates default cell pair text font width.
	/// </summary>
	public Inherited<SKFontStyleWidth> DefaultCellPairTextFontWidth { get; set; } = Inherited<SKFontStyleWidth>.FromPropertyName(nameof(DefaultFontWidth));

	/// <summary>
	/// Indicates default cell pair text font weight.
	/// </summary>
	public Inherited<SKFontStyleWeight> DefaultCellPairTextFontWeight { get; set; } = Inherited<SKFontStyleWeight>.FromPropertyName(nameof(DefaultFontWeight));

	/// <summary>
	/// Indicates default cell pair text font size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> DefaultCellPairTextFontSizeScale { get; set; } = Inherited<Scale>.FromValue(0.4M);

	/// <summary>
	/// Indicates default cell pair text font color.
	/// </summary>
	public Inherited<SerializableColor> DefaultCellPairTextFontColor { get; set; } = Inherited<SerializableColor>.FromValue(SKColors.Black);

	/// <summary>
	/// Indicates default cell pair text cover color.
	/// </summary>
	public Inherited<SerializableColor> DefaultCellPairTextCoverColor { get; set; } = Inherited<SerializableColor>.FromValue(SKColors.White);

	/// <summary>
	/// Indicates default cell pair text padding.
	/// </summary>
	public Inherited<Thickness<float>> DefaultCellPairTextPadding { get; set; } = Inherited<Thickness<float>>.FromValue(new(6));
}
