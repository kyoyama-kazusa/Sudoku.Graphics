namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates moon phase size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellMoonPhaseSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeSizeScale));

	/// <summary>
	/// Indicates moon phase stroke width scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellMoonPhaseStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeStrokeWidthScale));

	/// <summary>
	/// Indicates moon phase stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellMoonPhaseStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellShapeStrokeColor));

	/// <summary>
	/// Indicates moon phase fill color.
	/// </summary>
	public Inherited<SerializableColor> CellMoonPhaseFillColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultShapeFillColor));
}
