namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the cell pair kropki size scale.
	/// </summary>
	public Inherited<Scale> CellPairKropkiDefaultSizeScale { get; set; } = Inherited<Scale>.FromValue(0.3M);

	/// <summary>
	/// Indicates the cell pair kropki stroke width scale.
	/// </summary>
	public Inherited<Scale> CellPairKropkiDefaultStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeStrokeWidthScale));

	/// <summary>
	/// Indicates the cell pair kropki corner radius scale.
	/// </summary>
	public Inherited<Scale> CellPairKropkiDefaultCornerRadiusScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultCornerRadiusScale));

	/// <summary>
	/// Indicates the cell pair kropki stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellPairKropkiDefaultStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellShapeStrokeColor));

	/// <summary>
	/// Indicates the cell pair kropki fill color.
	/// </summary>
	public Inherited<SerializableColor> CellPairKropkiDefaultFillColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultShapeFillColor));
}
