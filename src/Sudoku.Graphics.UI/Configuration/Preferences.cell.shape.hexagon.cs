namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the cell hexagon size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellHexagonSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeSizeScale));

	/// <summary>
	/// Indicates the cell hexagon stroke width scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellHexagonStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeStrokeWidthScale));

	/// <summary>
	/// Indicates the cell hexagon stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellHexagonStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellShapeStrokeColor));

	/// <summary>
	/// Indicates the cell hexagon fill color.
	/// </summary>
	public Inherited<SerializableColor> CellHexagonFillColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultShapeFillColor));
}
