namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates large diamond size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellLargeDiamondSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeSizeScale));

	/// <summary>
	/// Indicates large diamond stroke width scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellLargeDiamondStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeStrokeWidthScale));

	/// <summary>
	/// Indicates large diamond stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellLargeDiamondStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellShapeStrokeColor));

	/// <summary>
	/// Indicates large diamond fill color.
	/// </summary>
	public Inherited<SerializableColor> CellLargeDiamondFillColor { get; set; } = Inherited<SerializableColor>.FromValue(SKColors.LightGray);
}
