namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates cell polygon size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellPolygonSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeSizeScale));

	/// <summary>
	/// Indicates cell polygon concave inner scale.
	/// </summary>
	public Inherited<Scale> CellPolygonConcaveInnerScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeSizeScale));

	/// <summary>
	/// Indicates cell polygon stroke width scale.
	/// </summary>
	public Inherited<Scale> CellPolygonStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeStrokeWidthScale));

	/// <summary>
	/// Indicates cell polygon stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellPolygonStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellShapeStrokeColor));

	/// <summary>
	/// Indicates cell polygon fill color.
	/// </summary>
	public Inherited<SerializableColor> CellPolygonFillColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultShapeFillColor));
}
