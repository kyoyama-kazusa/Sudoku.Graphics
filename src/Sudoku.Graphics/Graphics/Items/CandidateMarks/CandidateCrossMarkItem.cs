namespace Sudoku.Graphics.Items.CandidateMarks;

/// <summary>
/// Represents a candidate cross mark item.
/// </summary>
public sealed record CandidateCrossMarkItem : CandidateMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.Candidate_Cross;

	/// <inheritdoc/>
	public override required Scale SizeScale { get; init; }

	/// <summary>
	/// Indicates scale of stroke width.
	/// </summary>
	public required Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public override required SerializableColor StrokeColor { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawCrossTo(
			CandidatePosition,
			SizeScale,
			StrokeWidthScale,
			StrokeColor,
			canvas.Templates[TemplateIndex].Mapper
		);
}
