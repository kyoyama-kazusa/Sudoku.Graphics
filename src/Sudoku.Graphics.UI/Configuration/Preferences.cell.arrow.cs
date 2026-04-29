namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the cell arrow size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellArrowSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeSizeScale));

	/// <summary>
	/// Indicates the cell arrow stroke width scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellArrowStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeStrokeWidthScale));

	/// <summary>
	/// Indicates the cell arrow triangle width scale.
	/// </summary>
	public Inherited<Scale> CellArrowTriangleWidthScale { get; set; } = Inherited<Scale>.FromValue(0.75M);

	/// <summary>
	/// Indicates the cell arrow triangle height scale.
	/// </summary>
	public Inherited<Scale> CellArrowTriangleHeightScale { get; set; } = Inherited<Scale>.FromValue(0.375M);

	/// <summary>
	/// Indicates the cell arrow shaft width scale.
	/// </summary>
	public Inherited<Scale> CellArrowShaftWidthScale { get; set; } = Inherited<Scale>.FromValue(0.375M);

	/// <summary>
	/// Indicates the cell arrow shaft height scale.
	/// </summary>
	public Inherited<Scale> CellArrowShaftHeightScale { get; set; } = Inherited<Scale>.FromValue(0.375M);

	/// <summary>
	/// Indicates the cell arrow stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellArrowStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellShapeStrokeColor));

	/// <summary>
	/// Indicates the cell arrow fill color.
	/// </summary>
	public Inherited<SerializableColor> CellArrowFillColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultShapeFillColor));
}
