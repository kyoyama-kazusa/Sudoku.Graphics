namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the cell bisector line size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellBisectorLineSizeScale { get; set; } = Inherited<Scale>.FromValue(0.7M);

	/// <summary>
	/// Indicates the cell bisector line stroke width scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellBisectorLineStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultThickLineWidth));

	/// <summary>
	/// Indicates the cell bisector line stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellBisectorLineStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultThinLineColor));
}
