using LineDashSequenceI = System.Text.Json.Inherited<Sudoku.Graphics.LineDashSequence>;
using ScaleI = System.Text.Json.Inherited<Sudoku.Graphics.Scale>;
using SerializableColorI = System.Text.Json.Inherited<Sudoku.Graphics.SerializableColor>;
using SKFontStyleSlantI = System.Text.Json.Inherited<SkiaSharp.SKFontStyleSlant>;
using SKFontStyleWeightI = System.Text.Json.Inherited<SkiaSharp.SKFontStyleWeight>;
using SKFontStyleWidthI = System.Text.Json.Inherited<SkiaSharp.SKFontStyleWidth>;
using StringI = System.Text.Json.Inherited<string>;

namespace Sudoku.Graphics.UI.Configuration;

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
	/// Indicates given font width.
	/// </summary>
	public SKFontStyleWidthI GivenFontWidth { get; set; } = SKFontStyleWidthI.FromValue(SKFontStyleWidth.Normal);

	/// <summary>
	/// Indicates given font slant.
	/// </summary>
	public SKFontStyleSlantI GivenFontSlant { get; set; } = SKFontStyleSlantI.FromValue(SKFontStyleSlant.Upright);

	/// <summary>
	/// Indicates given font weight.
	/// </summary>
	public SKFontStyleWeightI GivenFontWeight { get; set; } = SKFontStyleWeightI.FromValue(SKFontStyleWeight.Medium);

	/// <summary>
	/// Indicates thick line dash sequence used by rendering templates.
	/// </summary>
	public LineDashSequenceI Template_ThickLineDashSequence { get; set; } = LineDashSequenceI.FromValue([]);

	/// <summary>
	/// Indicates thin line dash sequence used by rendering templates.
	/// </summary>
	public LineDashSequenceI Template_ThinLineDashSequence { get; set; } = LineDashSequenceI.FromPropertyName(nameof(Template_ThickLineDashSequence));
}
