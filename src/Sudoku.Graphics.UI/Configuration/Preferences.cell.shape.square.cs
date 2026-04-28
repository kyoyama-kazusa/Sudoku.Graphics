namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the cell square size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellSquareSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeSizeScale));

	/// <summary>
	/// Indicates the cell square stroke width scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellSquareStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeStrokeWidthScale));

	/// <summary>
	/// Indicates the cell square stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellSquareStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellShapeStrokeColor));

	/// <summary>
	/// Indicates the cell square fill color.
	/// </summary>
	public Inherited<SerializableColor> CellSquareFillColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellShapeFillColor));
}
