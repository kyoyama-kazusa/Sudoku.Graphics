namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates cell pair comparison operator text font name.
	/// </summary>
	public Inherited<string> CellPairComparisonOperatorFontName { get; set; } = Inherited<string>.FromPropertyName(nameof(DefaultCellPairTextFontName));

	/// <summary>
	/// Indicates cell pair comparison operator text font slant.
	/// </summary>
	public Inherited<SKFontStyleSlant> CellPairComparisonOperatorFontSlant { get; set; } = Inherited<SKFontStyleSlant>.FromPropertyName(nameof(DefaultCellPairTextFontSlant));

	/// <summary>
	/// Indicates cell pair comparison operator text font width.
	/// </summary>
	public Inherited<SKFontStyleWidth> CellPairComparisonOperatorFontWidth { get; set; } = Inherited<SKFontStyleWidth>.FromPropertyName(nameof(DefaultCellPairTextFontWidth));

	/// <summary>
	/// Indicates cell pair comparison operator text font weight.
	/// </summary>
	public Inherited<SKFontStyleWeight> CellPairComparisonOperatorFontWeight { get; set; } = Inherited<SKFontStyleWeight>.FromPropertyName(nameof(DefaultCellPairTextFontWeight));

	/// <summary>
	/// Indicates cell pair comparison operator text font size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellPairComparisonOperatorFontSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultCellPairTextFontSizeScale));

	/// <summary>
	/// Indicates cell pair comparison operator text font color.
	/// </summary>
	public Inherited<SerializableColor> CellPairComparisonOperatorFontColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultCellPairTextFontColor));

	/// <summary>
	/// Indicates cell pair comparison operator text cover color.
	/// </summary>
	public Inherited<SerializableColor> CellPairComparisonOperatorCoverColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultCellPairTextCoverColor));

	/// <summary>
	/// Indicates cell pair comparison operator text padding.
	/// </summary>
	public Inherited<Thickness<float>> CellPairComparisonOperatorPadding { get; set; } = Inherited<Thickness<float>>.FromPropertyName(nameof(DefaultCellPairTextPadding));
}
