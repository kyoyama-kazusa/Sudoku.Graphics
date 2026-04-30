namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates border-aligned arrow padding scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellBorderAlignedArrowPaddingScale { get; set; } = Inherited<Scale>.FromValue(0.1M);

	/// <summary>
	/// Indicates border-aligned arrow size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellBorderAlignedArrowSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeSizeScale));

	/// <summary>
	/// Indicates border-aligned arrow stroke width scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellBorderAlignedArrowStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeStrokeWidthScale));

	/// <summary>
	/// Indicates border-aligned arrow stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellBorderAlignedArrowStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellShapeStrokeColor));

	/// <summary>
	/// Indicates border-aligned arrow fill color.
	/// </summary>
	public Inherited<SerializableColor> CellBorderAlignedArrowFillColor { get; set; } = Inherited<SerializableColor>.FromValue(SKColors.DimGray);
}
