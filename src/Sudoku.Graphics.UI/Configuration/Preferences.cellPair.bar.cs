namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the cell pair bar corner radius scale.
	/// </summary>
	public Inherited<Scale> CellPairBarCornerRadiusScale { get; set; } = Inherited<Scale>.FromValue(1M);

	/// <summary>
	/// Indicates the cell pair bar short side scale.
	/// </summary>
	public Inherited<Scale> CellPairBarShortSideScale { get; set; } = Inherited<Scale>.FromValue(0.15M);

	/// <summary>
	/// Indicates the cell pair bar long side scale.
	/// </summary>
	public Inherited<Scale> CellPairBarLongSideScale { get; set; } = Inherited<Scale>.FromValue(0.8M);

	/// <summary>
	/// Indicates the cell pair bar stroke width scale.
	/// </summary>
	public Inherited<Scale> CellPairBarStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultThinLineWidthScale));

	/// <summary>
	/// Indicates the cell pair bar fill color.
	/// </summary>
	public Inherited<SerializableColor> CellPairBarFillColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultShapeFillColor));

	/// <summary>
	/// Indicates the cell pair bar stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellPairBarStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultMiscellaneousLineColor));
}
