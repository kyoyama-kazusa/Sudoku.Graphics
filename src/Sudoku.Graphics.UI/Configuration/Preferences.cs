namespace Sudoku.Graphics.UI.Configuration;

using LineDashSequenceI = Inherited<LineDashSequence>;
using ScaleI = Inherited<Scale>;
using SerializableColorI = Inherited<SerializableColor>;
using SKFontStyleSlantI = Inherited<SKFontStyleSlant>;
using SKFontStyleWeightI = Inherited<SKFontStyleWeight>;
using SKFontStyleWidthI = Inherited<SKFontStyleWidth>;
using StringI = Inherited<string>;

/// <summary>
/// Represents user preferences.
/// </summary>
internal sealed class Preferences
{
	/// <summary>
	/// Indicates the given font name.
	/// </summary>
	public StringI GivenFontName { get; set; } = StringI.FromValue("Arial");

	/// <summary>
	/// Indicates the modifiable font name.
	/// </summary>
	public StringI ModifiableFontName { get; set; } = StringI.FromPropertyName(nameof(GivenFontName));

	/// <summary>
	/// Indicates border corner radius used by rendering templates.
	/// </summary>
	public ScaleI Template_BorderCornerRadius { get; set; } = ScaleI.FromValue(0.25M);

	/// <summary>
	/// Indicates thick line width scale used by rendering templates.
	/// </summary>
	public ScaleI Template_ThickLineWidth { get; set; } = ScaleI.FromValue(0.06M);

	/// <summary>
	/// Indicates thin line width scale used by rendering templates.
	/// </summary>
	public ScaleI Template_ThinLineWidth { get; set; } = ScaleI.FromValue(0.0225M);

	/// <summary>
	/// Indicates the given font size scale (related to cell size).
	/// </summary>
	public ScaleI GivenFontSizeScale { get; set; } = ScaleI.FromValue(0.8M);

	/// <summary>
	/// Indicates the modifiable font size scale (related to cell size).
	/// </summary>
	public ScaleI ModifiableFontSizeScale { get; set; } = ScaleI.FromPropertyName(nameof(GivenFontSizeScale));

	/// <summary>
	/// Indicates background fill color.
	/// </summary>
	public SerializableColorI BackgroundFillColor { get; set; } = SerializableColorI.FromValue(SKColors.White);

	/// <summary>
	/// Indicates thick line colors used by rendering templates.
	/// </summary>
	public SerializableColorI Template_ThickLineColor { get; set; } = SerializableColorI.FromValue(SKColors.Black);

	/// <summary>
	/// Indicates thin line colors used by rendering templates.
	/// </summary>
	public SerializableColorI Template_ThinLineColor { get; set; } = SerializableColorI.FromPropertyName(nameof(Template_ThickLineColor));

	/// <summary>
	/// Indicates given text color.
	/// </summary>
	public SerializableColorI GivenTextColor { get; set; } = SerializableColorI.FromValue(SKColors.Black);

	/// <summary>
	/// Indicates modifiable text color.
	/// </summary>
	public SerializableColorI ModifiableTextColor { get; set; } = SerializableColorI.FromValue(SKColors.Blue);

	/// <summary>
	/// Indicates given font width.
	/// </summary>
	public SKFontStyleWidthI GivenFontWidth { get; set; } = SKFontStyleWidthI.FromValue(SKFontStyleWidth.Normal);

	/// <summary>
	/// Indicates modifiable font width.
	/// </summary>
	public SKFontStyleWidthI ModifiableFontWidth { get; set; } = SKFontStyleWidthI.FromPropertyName(nameof(GivenFontWidth));

	/// <summary>
	/// Indicates given font slant.
	/// </summary>
	public SKFontStyleSlantI GivenFontSlant { get; set; } = SKFontStyleSlantI.FromValue(SKFontStyleSlant.Upright);

	/// <summary>
	/// Indicates modifiable font slant.
	/// </summary>
	public SKFontStyleSlantI ModifiableFontSlant { get; set; } = SKFontStyleSlantI.FromPropertyName(nameof(GivenFontSlant));

	/// <summary>
	/// Indicates given font weight.
	/// </summary>
	public SKFontStyleWeightI GivenFontWeight { get; set; } = SKFontStyleWeightI.FromValue(SKFontStyleWeight.Medium);

	/// <summary>
	/// Indicates modifiable font weight.
	/// </summary>
	public SKFontStyleWeightI ModifiableFontWeight { get; set; } = SKFontStyleWeightI.FromPropertyName(nameof(GivenFontWeight));

	/// <summary>
	/// Indicates thick line dash sequence used by rendering templates.
	/// </summary>
	public LineDashSequenceI Template_ThickLineDashSequence { get; set; } = LineDashSequenceI.FromValue([]);

	/// <summary>
	/// Indicates thin line dash sequence used by rendering templates.
	/// </summary>
	public LineDashSequenceI Template_ThinLineDashSequence { get; set; } = LineDashSequenceI.FromPropertyName(nameof(Template_ThickLineDashSequence));
}
