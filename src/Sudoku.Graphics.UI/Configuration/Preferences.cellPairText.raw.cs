namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates cell pair raw text font name.
	/// </summary>
	public Inherited<string> CellPairRawTextFontName { get; set; } = Inherited<string>.FromPropertyName(nameof(DefaultCellPairTextFontName));

	/// <summary>
	/// Indicates cell pair raw text font slant.
	/// </summary>
	public Inherited<SKFontStyleSlant> CellPairRawTextFontSlant { get; set; } = Inherited<SKFontStyleSlant>.FromPropertyName(nameof(DefaultCellPairTextFontSlant));

	/// <summary>
	/// Indicates cell pair raw text font width.
	/// </summary>
	public Inherited<SKFontStyleWidth> CellPairRawTextFontWidth { get; set; } = Inherited<SKFontStyleWidth>.FromPropertyName(nameof(DefaultCellPairTextFontWidth));

	/// <summary>
	/// Indicates cell pair raw text font weight.
	/// </summary>
	public Inherited<SKFontStyleWeight> CellPairRawTextFontWeight { get; set; } = Inherited<SKFontStyleWeight>.FromPropertyName(nameof(DefaultCellPairTextFontWeight));

	/// <summary>
	/// Indicates cell pair raw text font size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellPairRawTextFontSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultCellPairTextFontSizeScale));

	/// <summary>
	/// Indicates cell pair raw text font color.
	/// </summary>
	public Inherited<SerializableColor> CellPairRawTextFontColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultCellPairTextFontColor));

	/// <summary>
	/// Indicates cell pair raw text cover color.
	/// </summary>
	public Inherited<SerializableColor> CellPairRawTextCoverColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultCellPairTextCoverColor));

	/// <summary>
	/// Indicates cell pair raw text padding.
	/// </summary>
	public Inherited<Thickness<float>> CellPairRawTextPadding { get; set; } = Inherited<Thickness<float>>.FromPropertyName(nameof(DefaultCellPairTextPadding));
}
