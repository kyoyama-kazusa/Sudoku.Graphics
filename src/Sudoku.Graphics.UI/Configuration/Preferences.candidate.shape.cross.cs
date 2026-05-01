namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the candidate cross size scale, related to candidate size.
	/// </summary>
	public Inherited<Scale> CandidateCrossSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CandidateShapeSizeScale));

	/// <summary>
	/// Indicates the candidate cross stroke width scale, related to candidate size.
	/// </summary>
	public Inherited<Scale> CandidateCrossStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CandidateShapeStrokeWidthScale));

	/// <summary>
	/// Indicates the candidate cross stroke color.
	/// </summary>
	public Inherited<SerializableColor> CandidateCrossStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CandidateShapeStrokeColor));
}
