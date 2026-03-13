namespace Sudoku.Graphics.Items.CandidateMarks;

/// <summary>
/// Represents a candidate cross mark item.
/// </summary>
public sealed record CandidateCrossMarkItem : CandidateMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CandidateMark_Cross;

	/// <summary>
	/// Indicates scale of stroke width.
	/// </summary>
	public required Scale StrokeWidthScale { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		canvas.BackingCanvas.DrawCrossTo(
			CandidatePosition,
			SizeScale,
			StrokeWidthScale,
			StrokeColor,
			mapper
		);
	}
}
