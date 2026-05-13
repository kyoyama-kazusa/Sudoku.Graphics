namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the cell pair connection line stroke width scale.
	/// </summary>
	public Inherited<Scale> CellPairConnectionLineStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultThickLineWidthScale));

	/// <summary>
	/// Indicates the cell pair connection line size scale.
	/// </summary>
	public Inherited<Scale> CellPairConnectionLineSizeScale { get; set; } = Inherited<Scale>.FromValue(0.7M);

	/// <summary>
	/// Indicates the cell pair connection line stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellPairConnectionLineStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultThickLineColor));
}
