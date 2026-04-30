namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates border-aligned triangle size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellBorderAlignedTriangleSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeSizeScale));

	/// <summary>
	/// Indicates border-aligned triangle stroke width scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellBorderAlignedTriangleStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeStrokeWidthScale));

	/// <summary>
	/// Indicates border-aligned triangle stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellBorderAlignedTriangleStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellShapeStrokeColor));

	/// <summary>
	/// Indicates border-aligned triangle fill color.
	/// </summary>
	public Inherited<SerializableColor> CellBorderAlignedTriangleFillColor { get; set; } = Inherited<SerializableColor>.FromValue(SKColors.LightGray);
}
