namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the cell circle size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellCircleSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeSizeScale));

	/// <summary>
	/// Indicates the cell circle stroke width scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellCircleStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeStrokeWidthScale));

	/// <summary>
	/// Indicates the cell circle stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellCircleStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellShapeStrokeColor));

	/// <summary>
	/// Indicates the cell circle fill color.
	/// </summary>
	public Inherited<SerializableColor> CellCircleFillColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellShapeFillColor));
}
