namespace Sudoku.Graphics.Items.Texts;

/// <summary>
/// Represents candidate text.
/// </summary>
public sealed record CandidateTextItem :
	TextItem,
	IItem_CandidatePositionProperty,
	IItem_ColorProperty,
	IItem_FontRelatedProperties,
	IItem_TemplateIndexProperty,
	IItem_TextProperty
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CandidateText;

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
