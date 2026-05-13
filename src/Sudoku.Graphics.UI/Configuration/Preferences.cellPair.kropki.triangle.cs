namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the cell pair kropki size scale.
	/// </summary>
	public Inherited<Scale> CellPairKropkiTriangleSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellPairKropkiDefaultSizeScale));

	/// <summary>
	/// Indicates the cell pair kropki stroke width scale.
	/// </summary>
	public Inherited<Scale> CellPairKropkiTriangleStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellPairKropkiDefaultStrokeWidthScale));

	/// <summary>
	/// Indicates the cell pair kropki stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellPairKropkiTriangleStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellPairKropkiDefaultStrokeColor));

	/// <summary>
	/// Indicates the cell pair kropki fill color.
	/// </summary>
	public Inherited<SerializableColor> CellPairKropkiTriangleFillColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellPairKropkiDefaultFillColor));
}
