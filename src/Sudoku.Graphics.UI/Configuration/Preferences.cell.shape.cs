namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the cell circle size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellShapeSizeScale { get; set; } = Inherited<Scale>.FromValue(0.5M);

	/// <summary>
	/// Indicates the cell circle stroke width scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellShapeStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultThinLineWidth));

	/// <summary>
	/// Indicates the cell circle stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellShapeStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultThinLineColor));

	/// <summary>
	/// Indicates the cell circle fill color.
	/// </summary>
	public Inherited<SerializableColor> CellShapeFillColor { get; set; } = Inherited<SerializableColor>.FromValue(SKColors.White);
}
