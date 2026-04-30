namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates cell battenburg uniform corner radius.
	/// </summary>
	public Inherited<Scale> CellBattenburgUniformCornerRadiusScale { get; set; } = Inherited<Scale>.FromValue(0.25M);

	/// <summary>
	/// Indicates the cell battenburg size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellBattenburgSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeSizeScale));

	/// <summary>
	/// Indicates the cell battenburg stroke width scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellBattenburgStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeStrokeWidthScale));

	/// <summary>
	/// Indicates the cell battenburg stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellBattenburgStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellShapeStrokeColor));

	/// <summary>
	/// Indicates the color 1 of cell battenburg.
	/// </summary>
	public Inherited<SerializableColor> CellBattenburgColor1 { get; set; } = Inherited<SerializableColor>.FromValue(SKColors.White);

	/// <summary>
	/// Indicates the color 2 of cell battenburg.
	/// </summary>
	public Inherited<SerializableColor> CellBattenburgColor2 { get; set; } = Inherited<SerializableColor>.FromValue(SKColors.LightGray);
}
