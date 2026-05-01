namespace Sudoku.Graphics.Items.CandidateMarks;

/// <summary>
/// Represents a candidate cross mark item.
/// </summary>
public sealed record CandidateCrossMarkItem : CandidateMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.Candidate_Cross;

	/// <inheritdoc/>
	public required override Scale SizeScale { get; init; }

	/// <summary>
	/// Indicates scale of stroke width.
	/// </summary>
	public required Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor StrokeColor { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawCrossTo(
			CandidatePosition,
			SizeScale,
			StrokeWidthScale,
			StrokeColor,
			canvas.Mapper
		);
}
