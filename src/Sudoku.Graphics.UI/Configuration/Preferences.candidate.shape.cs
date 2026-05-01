namespace Sudoku.Graphics.UI.Configuration;

internal partial class Preferences
{
	/// <summary>
	/// Indicates the candidate shape size scale, related to candidate size.
	/// </summary>
	public Inherited<Scale> CandidateShapeSizeScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeSizeScale));

	/// <summary>
	/// Indicates the candidate shape stroke width scale, related to candidate size.
	/// </summary>
	public Inherited<Scale> CandidateShapeStrokeWidthScale { get; set; } = Inherited<Scale>.FromPropertyName(nameof(CellShapeStrokeWidthScale));

	/// <summary>
	/// Indicates the candidate shape stroke color.
	/// </summary>
	public Inherited<SerializableColor> CandidateShapeStrokeColor { get; set; } = Inherited<SerializableColor>.FromPropertyName(nameof(CellShapeStrokeColor));
}
