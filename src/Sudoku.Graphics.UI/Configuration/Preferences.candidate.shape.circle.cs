namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the candidate circle size scale, related to candidate size.
	/// </summary>
	public Inherited<Scale> CandidateCircleSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CandidateShapeSizeScale));

	/// <summary>
	/// Indicates the candidate circle stroke width scale, related to candidate size.
	/// </summary>
	public Inherited<Scale> CandidateCircleStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CandidateShapeStrokeWidthScale));

	/// <summary>
	/// Indicates the candidate circle stroke color.
	/// </summary>
	public Inherited<SerializableColor> CandidateCircleStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CandidateShapeStrokeColor));

	/// <summary>
	/// Indicates the candidate circle fill color.
	/// </summary>
	public Inherited<SerializableColor> CandidateCircleFillColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(DefaultShapeFillColor));
}
