namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the cell cross size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellCrossSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeSizeScale));

	/// <summary>
	/// Indicates the cell cross stroke width scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellCrossStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeStrokeWidthScale));

	/// <summary>
	/// Indicates the cell cross stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellCrossStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellShapeStrokeColor));
}
