namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the cell pair kropki size scale.
	/// </summary>
	public Inherited<Scale> CellPairKropkiSquareSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellPairKropkiDefaultSizeScale));

	/// <summary>
	/// Indicates the cell pair kropki stroke width scale.
	/// </summary>
	public Inherited<Scale> CellPairKropkiSquareStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellPairKropkiDefaultStrokeWidthScale));

	/// <summary>
	/// Indicates the cell pair kropki corner radius scale.
	/// </summary>
	public Inherited<Scale> CellPairKropkiSquareCornerRadiusScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellPairKropkiDefaultCornerRadiusScale));

	/// <summary>
	/// Indicates the cell pair kropki stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellPairKropkiSquareStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellPairKropkiDefaultStrokeColor));

	/// <summary>
	/// Indicates the cell pair kropki fill color.
	/// </summary>
	public Inherited<SerializableColor> CellPairKropkiSquareFillColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellPairKropkiDefaultFillColor));
}
