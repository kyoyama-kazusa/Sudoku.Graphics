namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	public Inherited<Scale> CellMoonPhaseSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeSizeScale));

	public Inherited<Scale> CellMoonPhaseStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeStrokeWidthScale));

	public Inherited<SerializableColor> CellMoonPhaseStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellShapeStrokeColor));

	public Inherited<SerializableColor> CellMoonPhaseFillColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultShapeFillColor));
}
