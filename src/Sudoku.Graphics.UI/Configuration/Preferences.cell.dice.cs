namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates dice corner radius scale.
	/// </summary>
	public Inherited<Scale> CellDiceCornerRadiusScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultCornerRadiusScale));

	/// <summary>
	/// Indicates dice stroke thickness scale.
	/// </summary>
	public Inherited<Scale> CellDiceStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultThinLineWidthScale));

	/// <summary>
	/// Indicates the dice size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> CellDiceSizeScale { get; set; } = Inherited<Scale>.FromValue(0.2M);

	/// <summary>
	/// Indicates dice line color.
	/// </summary>
	public Inherited<SerializableColor> CellDiceLineColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultMiscellaneousLineColor));

	/// <summary>
	/// Indicates dice fill color.
	/// </summary>
	public Inherited<SerializableColor> CellDiceFillColor { get; set; } = Inherited<SerializableColor>.FromValue(SKColors.White);
}
