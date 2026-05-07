namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates cell pair arrow text font name.
	/// </summary>
	public Inherited<string> CellPairArrowFontName { get; set; } = Inherited<string>.FromValue("JetBrains Mono");

	/// <summary>
	/// Indicates cell pair arrow text font slant.
	/// </summary>
	public Inherited<SKFontStyleSlant> CellPairArrowFontSlant { get; set; } = Inherited<SKFontStyleSlant>.FromPropertyName(nameof(DefaultCellPairTextFontSlant));

	/// <summary>
	/// Indicates cell pair arrow text font width.
	/// </summary>
	public Inherited<SKFontStyleWidth> CellPairArrowFontWidth { get; set; } = Inherited<SKFontStyleWidth>.FromPropertyName(nameof(DefaultCellPairTextFontWidth));

	/// <summary>
	/// Indicates cell pair arrow text font weight.
	/// </summary>
	public Inherited<SKFontStyleWeight> CellPairArrowFontWeight { get; set; } = Inherited<SKFontStyleWeight>.FromPropertyName(nameof(DefaultCellPairTextFontWeight));

	/// <summary>
	/// Indicates cell pair arrow text font size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellPairArrowFontSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultCellPairTextFontSizeScale));

	/// <summary>
	/// Indicates cell pair arrow text font color.
	/// </summary>
	public Inherited<SerializableColor> CellPairArrowFontColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultCellPairTextFontColor));

	/// <summary>
	/// Indicates cell pair arrow text cover color.
	/// </summary>
	public Inherited<SerializableColor> CellPairArrowCoverColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultCellPairTextCoverColor));

	/// <summary>
	/// Indicates cell pair arrow text padding.
	/// </summary>
	public Inherited<Thickness<float>> CellPairArrowPadding { get; set; } = Inherited<Thickness<float>>.FromPropertyName(nameof(DefaultCellPairTextPadding));
}
