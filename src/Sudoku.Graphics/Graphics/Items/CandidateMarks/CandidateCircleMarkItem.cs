namespace Sudoku.Graphics.Items.CandidateMarks;

/// <summary>
/// Represents a candidate circle mark item.
/// </summary>
public sealed record CandidateCircleMarkItem : CandidateMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.Candidate_Circle;

	/// <inheritdoc/>
	public required override Scale SizeScale { get; init; }

	/// <summary>
	/// Indicates stroke width scale.
	/// </summary>
	public Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public override SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor FillColor { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawCircleTo(
			CandidatePosition,
			SizeScale,
			StrokeColor,
			StrokeWidthScale,
			FillColor,
			canvas.Mapper
		);
}
