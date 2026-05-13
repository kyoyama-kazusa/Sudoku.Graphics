namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the cell pair battenburg uniform corner radius.
	/// </summary>
	public Inherited<Scale> CellPairBattenburgUniformCornerRadiusScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellBattenburgUniformCornerRadiusScale));

	/// <summary>
	/// Indicates the cell pair battenburg size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellPairBattenburgSizeScale { get; set; } = Inherited<Scale>.FromValue(0.35M);

	/// <summary>
	/// Indicates the cell pair battenburg stroke width scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellPairBattenburgStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellBattenburgStrokeWidthScale));

	/// <summary>
	/// Indicates the cell pair battenburg stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellPairBattenburgStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellBattenburgStrokeColor));

	/// <summary>
	/// Indicates the color 1 of cell pair battenburg.
	/// </summary>
	public Inherited<SerializableColor> CellPairBattenburgColor1 { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellBattenburgColor1));

	/// <summary>
	/// Indicates the color 2 of cell pair battenburg.
	/// </summary>
	public Inherited<SerializableColor> CellPairBattenburgColor2 { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellBattenburgColor2));
}
