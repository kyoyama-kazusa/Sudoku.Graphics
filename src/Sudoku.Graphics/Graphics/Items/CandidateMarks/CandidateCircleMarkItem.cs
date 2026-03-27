namespace Sudoku.Graphics.Items.CandidateMarks;

/// <summary>
/// Represents a candidate circle mark item.
/// </summary>
public sealed record CandidateCircleMarkItem : CandidateMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CandidateMark_Circle;

	/// <inheritdoc/>
	public override required Scale SizeScale { get; init; }

	/// <summary>
	/// Indicates stroke width scale.
	/// </summary>
	public required Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public override required SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public override required SerializableColor FillColor { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawCircleTo(
			CandidatePosition,
			SizeScale,
			StrokeColor,
			StrokeWidthScale,
			FillColor,
			canvas.Templates[TemplateIndex].Mapper
		);
}
