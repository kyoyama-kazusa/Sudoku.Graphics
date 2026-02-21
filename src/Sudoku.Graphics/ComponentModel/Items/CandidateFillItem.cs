namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents a candidate fill item.
/// </summary>
public sealed class CandidateFillItem : Item, IItem_ColorProperty, IItem_CandidatePositionProperty, IItem_TemplateIndexProperty
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
	protected override Type EqualityContract => typeof(CandidateFillItem);


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] Item? other)
		=> other is CandidateFillItem comparer && CandidatePosition == comparer.CandidatePosition && Color == comparer.Color;

	/// <inheritdoc/>
	public override int GetHashCode() => HashCode.Combine(EqualityContract, CandidatePosition, Color);

	/// <inheritdoc/>
	protected override void PrintMembers(StringBuilder builder)
	{
		builder.Append($"{nameof(CandidatePosition)} = {CandidatePosition}, ");
		builder.Append($"{nameof(Color)} = {Color}");
	}

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
