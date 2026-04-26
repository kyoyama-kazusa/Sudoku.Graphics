namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the given font name.
	/// </summary>
	public Inherited<string> DefaultFontName { get; set; } = Inherited<string>.FromValue("Arial");

	/// <summary>
	/// Indicates border corner radius used by rendering templates.
	/// </summary>
	public Inherited<Scale> DefaultBorderCornerRadius { get; set; } = Inherited<Scale>.FromValue(0.25M);

	/// <summary>
	/// Indicates thick line width scale used by rendering templates.
	/// </summary>
	public Inherited<Scale> DefaultThickLineWidth { get; set; } = Inherited<Scale>.FromValue(0.06M);

	/// <summary>
	/// Indicates thin line width scale used by rendering templates.
	/// </summary>
	public Inherited<Scale> DefaultThinLineWidth { get; set; } = Inherited<Scale>.FromValue(0.0225M);

	/// <summary>
	/// Indicates the default corner radius scale.
	/// </summary>
	public Inherited<Scale> DefaultCornerRadiusScale { get; set; } = Inherited<Scale>.FromValue(0.25M);

	/// <summary>
	/// Indicates the given font size scale (related to cell size).
	/// </summary>
	public Inherited<Scale> DefaultFontSizeScale { get; set; } = Inherited<Scale>.FromValue(0.8M);

	/// <summary>
	/// Indicates default fill color.
	/// </summary>
	public Inherited<SerializableColor> DefaultFillColor { get; set; } = Inherited<SerializableColor>.FromValue(SKColors.Black);

	/// <summary>
	/// Indicates thick line colors.
	/// </summary>
	public Inherited<SerializableColor> DefaultThickLineColor { get; set; } = Inherited<SerializableColor>.FromValue(SKColors.Black);

	/// <summary>
	/// Indicates thin line colors.
	/// </summary>
	public Inherited<SerializableColor> DefaultThinLineColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultThickLineColor));

	/// <summary>
	/// Indicates thin line colors.
	/// </summary>
	public Inherited<SerializableColor> DefaultMiscellaneousLineColor { get; set; } = Inherited<SerializableColor>.FromValue(SKColors.Black);

	/// <summary>
	/// Indicates given text color.
	/// </summary>
	public Inherited<SerializableColor> DefaultFontColor { get; set; } = Inherited<SerializableColor>.FromValue(SKColors.Black);

	/// <summary>
	/// Indicates given font width.
	/// </summary>
	public Inherited<SKFontStyleWidth> DefaultFontWidth { get; set; } = Inherited<SKFontStyleWidth>.FromValue(SKFontStyleWidth.Normal);

	/// <summary>
	/// Indicates given font slant.
	/// </summary>
	public Inherited<SKFontStyleSlant> DefaultFontSlant { get; set; } = Inherited<SKFontStyleSlant>.FromValue(SKFontStyleSlant.Upright);

	/// <summary>
	/// Indicates given font weight.
	/// </summary>
	public Inherited<SKFontStyleWeight> DefaultFontWeight { get; set; } = Inherited<SKFontStyleWeight>.FromValue(SKFontStyleWeight.Medium);

	/// <summary>
	/// Indicates thin line dash sequence.
	/// </summary>
	public Inherited<LineDashSequence> DefaultThickLineDashSequence { get; set; } = Inherited<LineDashSequence>.FromValue([]);

	/// <summary>
	/// Indicates thin line dash sequence.
	/// </summary>
	public Inherited<LineDashSequence> DefaultThinLineDashSequence { get; set; } = Inherited<LineDashSequence>.FromPropertyName(nameof(DefaultThickLineDashSequence));
}
