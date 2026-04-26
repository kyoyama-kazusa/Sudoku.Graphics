namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates phantom dice corner radius scale.
	/// </summary>
	public Inherited<Scale> PhantomDiceCornerRadiusScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultCornerRadiusScale));

	/// <summary>
	/// Indicates phantom dice stroke thickness scale.
	/// </summary>
	public Inherited<Scale> PhantomDiceStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultThinLineWidth));

	/// <summary>
	/// Indicates the phantom dice size scale, related to cell size.
	/// </summary>
	public Inherited<Scale> PhantomDiceSizeScale { get; set; } = Inherited<Scale>.FromValue(0.2M);

	/// <summary>
	/// Indicates phantom dice line color.
	/// </summary>
	public Inherited<SerializableColor> PhantomDiceLineColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultMiscellaneousLineColor));

	/// <summary>
	/// Indicates phantom dice fill color.
	/// </summary>
	public Inherited<SerializableColor> PhantomDiceFillColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultFillColor));
}
