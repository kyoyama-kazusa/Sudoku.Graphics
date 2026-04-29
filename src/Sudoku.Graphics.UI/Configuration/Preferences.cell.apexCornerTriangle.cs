namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the cell apex corner triangle size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellApexCornerTriangleSizeScale { get; set; } = Inherited<Scale>.FromValue(0.25M);

	/// <summary>
	/// Indicates the cell apex corner triangle padding scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellApexCornerTrianglePaddingScale { get; set; } = Inherited<Scale>.FromValue(0.2M);

	/// <summary>
	/// Indicates the cell apex corner triangle stroke width scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellApexCornerTriangleStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeStrokeWidthScale));

	/// <summary>
	/// Indicates the cell apex corner triangle stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellApexCornerTriangleStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellShapeStrokeColor));

	/// <summary>
	/// Indicates the cell apex corner triangle fill color.
	/// </summary>
	public Inherited<SerializableColor> CellApexCornerTriangleFillColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultShapeFillColor));
}
