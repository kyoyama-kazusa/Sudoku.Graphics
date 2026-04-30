namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates loop segment line size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellLoopSegmentLineSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeSizeScale));

	/// <summary>
	/// Indicates loop segment line stroke width scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellLoopSegmentLineStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeStrokeWidthScale));

	/// <summary>
	/// Indicates loop segment line corner radius scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellLoopSegmentLineCornerRadiusScale { get; set; } = Inherited<Scale>.FromValue(0.25M);

	/// <summary>
	/// Indicates loop segment line stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellLoopSegmentLineStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellShapeStrokeColor));
}
