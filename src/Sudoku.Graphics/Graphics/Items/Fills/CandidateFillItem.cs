namespace Sudoku.Graphics.Items.Fills;

/// <summary>
/// Represents a candidate fill item.
/// </summary>
public sealed record CandidateFillItem : FillItem, IItem_CandidatePositionProperty
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.Fill_Candidate;

	/// <inheritdoc/>
	public required CandidatePosition CandidatePosition { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, Color = Color };
		var mapper = canvas.Mapper;
		var topLeft = mapper.GetPoint(CandidatePosition, Alignment.TopLeft);
		var bottomRight = mapper.GetPoint(CandidatePosition, Alignment.BottomRight);
		var rect = SKRect.Create(topLeft, bottomRight);
		canvas.BackingCanvas.DrawRect(rect, fillPaint);
	}
}
