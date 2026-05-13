namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the cell pair kropki size scale.
	/// </summary>
	public Inherited<Scale> CellPairKropkiSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellPairKropkiDefaultSizeScale));

	/// <summary>
	/// Indicates the cell pair kropki stroke width scale.
	/// </summary>
	public Inherited<Scale> CellPairKropkiStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellPairKropkiDefaultStrokeWidthScale));

	/// <summary>
	/// Indicates the cell pair kropki stroke color.
	/// </summary>
	public Inherited<SerializableColor> CellPairKropkiStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellPairKropkiDefaultStrokeColor));

	/// <summary>
	/// Indicates the cell pair kropki fill color.
	/// </summary>
	public Inherited<SerializableColor> CellPairKropkiFillColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellPairKropkiDefaultFillColor));
}
