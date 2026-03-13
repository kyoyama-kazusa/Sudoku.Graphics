namespace Sudoku.Graphics.Items.CandidateMarks;

/// <summary>
/// Represents a candidate circle mark item.
/// </summary>
public sealed record CandidateCircleMarkItem : CandidateMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CandidateCircleMark;

	/// <summary>
	/// Indicates stroke width scale.
	/// </summary>
	public Scale StrokeWidthScale { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		canvas.BackingCanvas.DrawCircleTo(
			CandidatePosition,
			SizeScale,
			StrokeColor,
			StrokeWidthScale,
			FillColor,
			mapper
		);
	}
}
