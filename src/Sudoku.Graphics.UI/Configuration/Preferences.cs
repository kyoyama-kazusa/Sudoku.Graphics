using LineDashSequenceI = System.Text.Json.Inherited<Sudoku.Graphics.LineDashSequence>;
using ScaleI = System.Text.Json.Inherited<Sudoku.Graphics.Scale>;
using SerializableColorI = System.Text.Json.Inherited<Sudoku.Graphics.SerializableColor>;

namespace Sudoku.Graphics.UI.Configuration;

/// <summary>
/// Represents user preferences.
/// </summary>
internal sealed class Preferences
{
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
	/// Indicates thick line dash sequence used by rendering templates.
	/// </summary>
	public LineDashSequenceI Template_ThickLineDashSequence { get; set; } = LineDashSequenceI.FromValue([]);

	/// <summary>
	/// Indicates thin line dash sequence used by rendering templates.
	/// </summary>
	public LineDashSequenceI Template_ThinLineDashSequence { get; set; } = LineDashSequenceI.FromPropertyName(nameof(Template_ThickLineDashSequence));
}
