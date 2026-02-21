namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents a candidate fill item.
/// </summary>
public sealed class CandidateFillItem : FillItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CandidateFill;

	/// <summary>
	/// Indicates template index to draw.
	/// </summary>
	public required int TemplateIndex { get; init; }

	/// <summary>
	/// Indicates candidate position.
	/// </summary>
	public required CandidatePosition CandidatePosition { get; init; }

	/// <summary>
	/// Indicates the color to fill.
	/// </summary>
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
