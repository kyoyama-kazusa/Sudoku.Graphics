namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the cell diamond size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellDiamondSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeSizeScale));

	/// <summary>
	/// Indicates the cell diamond stroke width scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellDiamondStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeStrokeWidthScale));

	/// <summary>
	/// Indicates the cell diamond stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellDiamondStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellShapeStrokeColor));

	/// <summary>
	/// Indicates the cell diamond fill color.
	/// </summary>
	public Inherited<SerializableColor> CellDiamondFillColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultShapeFillColor));
}
