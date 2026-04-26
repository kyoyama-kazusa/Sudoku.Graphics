namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates dice corner radius scale.
	/// </summary>
	public Inherited<Scale> DiceCornerRadiusScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultCornerRadiusScale));

	/// <summary>
	/// Indicates dice stroke thickness scale.
	/// </summary>
	public Inherited<Scale> DiceStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultThinLineWidth));

	/// <summary>
	/// Indicates the dice size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> DiceSizeScale { get; set; } = Inherited<Scale>.FromValue(0.2M);

	/// <summary>
	/// Indicates dice line color.
	/// </summary>
	public Inherited<SerializableColor> DiceLineColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultMiscellaneousLineColor));

	/// <summary>
	/// Indicates dice fill color.
	/// </summary>
	public Inherited<SerializableColor> DiceFillColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultFillColor));
}
