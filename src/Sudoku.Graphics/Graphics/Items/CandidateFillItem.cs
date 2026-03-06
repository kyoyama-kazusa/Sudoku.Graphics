namespace Sudoku.Graphics.Items;

/// <summary>
/// Represents a candidate fill item.
/// </summary>
public sealed record CandidateFillItem : Item, IItem_ColorProperty, IItem_CandidatePositionProperty, IItem_TemplateIndexProperty
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CandidateFill;

	/// <inheritdoc/>
	public required int TemplateIndex { get; init; }

	/// <inheritdoc/>
	public required CandidatePosition CandidatePosition { get; init; }

	/// <inheritdoc/>
	public required SerializableColor Color { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, Color = Color };
		var template = canvas.Templates[TemplateIndex];
		var topLeft = template.Mapper.GetPoint(CandidatePosition, Alignment.TopLeft);
		var bottomRight = template.Mapper.GetPoint(CandidatePosition, Alignment.BottomRight);
		var rect = SKRect.Create(topLeft, bottomRight);
		canvas.BackingCanvas.DrawRect(rect, fillPaint);
	}
}
