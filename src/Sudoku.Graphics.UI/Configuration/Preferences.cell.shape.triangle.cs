namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the cell triangle size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellTriangleSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeSizeScale));

	/// <summary>
	/// Indicates the cell triangle stroke width scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellTriangleStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeStrokeWidthScale));

	/// <summary>
	/// Indicates the cell triangle stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellTriangleStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellShapeStrokeColor));

	/// <summary>
	/// Indicates the cell triangle fill color.
	/// </summary>
	public Inherited<SerializableColor> CellTriangleFillColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultShapeFillColor));
}
