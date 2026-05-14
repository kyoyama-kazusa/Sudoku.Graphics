namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates border corner radius used by rendering templates.
	/// </summary>
	public Inherited<Scale> TemplateBorderCornerRadiusScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultBorderCornerRadiusScale));

	/// <summary>
	/// Indicates thick line width scale used by rendering templates.
	/// </summary>
	public Inherited<Scale> TemplateThickLineWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultThickLineWidthScale));

	/// <summary>
	/// Indicates thin line width scale used by rendering templates.
	/// </summary>
	public Inherited<Scale> TemplateThinLineWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(DefaultThinLineWidthScale));

	/// <summary>
	/// Indicates thick line colors used by rendering templates.
	/// </summary>
	public Inherited<SerializableColor> TemplateThickLineColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultThickLineColor));

	/// <summary>
	/// Indicates thin line colors used by rendering templates.
	/// </summary>
	public Inherited<SerializableColor> TemplateThinLineColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultThinLineColor));

	/// <summary>
	/// Indicates thick line dash sequence used by rendering templates.
	/// </summary>
	public Inherited<LineDashSequence> TemplateThickLineDashSequence { get; set; } = Inherited<LineDashSequence>.FromPropertyName(nameof(DefaultThickLineDashSequence));

	/// <summary>
	/// Indicates thin line dash sequence used by rendering templates.
	/// </summary>
	public Inherited<LineDashSequence> TemplateThinLineDashSequence { get; set; } = Inherited<LineDashSequence>.FromPropertyName(nameof(DefaultThinLineDashSequence));
}
