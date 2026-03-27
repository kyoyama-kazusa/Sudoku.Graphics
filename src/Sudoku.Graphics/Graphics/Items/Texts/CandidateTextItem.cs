namespace Sudoku.Graphics.Items.Texts;

/// <summary>
/// Represents candidate text.
/// </summary>
public sealed record CandidateTextItem : TextItem, IItem_CandidatePositionProperty
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.Text_Candidate;

	/// <inheritdoc/>
	public required CandidatePosition CandidatePosition { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawTextToCandidate(
			Text,
			CandidatePosition,
			FontName,
			FontSizeScale,
			FontWeight,
			FontWidth,
			FontSlant,
			Color,
			canvas.Templates[TemplateIndex].Mapper
		);
}
