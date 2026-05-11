namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the cell pair bridge lines circle diameter scale.
	/// </summary>
	public Inherited<Scale> CellPairBridgeLinesCircleDiameterScale { get; set; } = Inherited<Scale>.FromValue(0.9M);

	/// <summary>
	/// Indicates the cell pair bridge lines max gap scale.
	/// </summary>
	public Inherited<Scale> CellPairBridgeLinesMaxGapScale { get; set; } = Inherited<Scale>.FromValue(0.2M);

	/// <summary>
	/// Indicates the cell pair bridge lines stroke width scale.
	/// </summary>
	public Inherited<Scale> CellPairBridgeLinesStrokeWidthScale { get; set; } = Inherited<Scale>.FromValue(0.06M);

	/// <summary>
	/// Indicates the cell pair bridge lines circle fill color.
	/// </summary>
	public Inherited<SerializableColor> CellPairBridgeLinesCircleFillColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultShapeFillColor));

	/// <summary>
	/// Indicates the cell pair bridge lines stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellPairBridgeLinesStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultThickLineColor));
}
