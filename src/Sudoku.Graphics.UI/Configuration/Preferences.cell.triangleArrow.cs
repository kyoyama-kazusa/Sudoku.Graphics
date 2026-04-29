namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the cell arrow triangle size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellArrowTriangleSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeSizeScale));

	/// <summary>
	/// Indicates the cell arrow triangle base scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellArrowTriangleBaseScale { get; set; } = Inherited<Scale>.FromValue(0.5M);

	/// <summary>
	/// Indicates the cell arrow triangle stroke width scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellArrowTriangleStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeStrokeWidthScale));

	/// <summary>
	/// Indicates the cell arrow triangle stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellArrowTriangleStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellShapeStrokeColor));

	/// <summary>
	/// Indicates the cell arrow triangle fill color.
	/// </summary>
	public Inherited<SerializableColor> CellArrowTriangleFillColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultShapeFillColor));
}
