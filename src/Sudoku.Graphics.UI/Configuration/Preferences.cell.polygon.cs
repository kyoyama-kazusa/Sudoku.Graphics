namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	public Inherited<Scale> CellPolygonSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeSizeScale));

	public Inherited<Scale> CellPolygonConcaveInnerScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeSizeScale));

	public Inherited<Scale> CellPolygonStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeStrokeWidthScale));

	public Inherited<SerializableColor> CellPolygonStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellShapeStrokeColor));

	public Inherited<SerializableColor> CellPolygonFillColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultShapeFillColor));
}
