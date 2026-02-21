namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents cell fill item.
/// </summary>
public sealed class CellFillItem : Item, IItem_CellProperty, IItem_ColorProperty, IItem_TemplateIndexProperty
{
	/// <inheritdoc/>
	public required int TemplateIndex { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellFill;

	/// <inheritdoc/>
	public required Absolute Cell { get; init; }

	/// <inheritdoc/>
	public required SerializableColor Color { get; init; }

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CellFillItem);


	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] Item? other)
		=> other is CellFillItem comparer && TemplateIndex == comparer.TemplateIndex
		&& Cell == comparer.Cell && Color == comparer.Color;

	/// <inheritdoc/>
	public override int GetHashCode() => HashCode.Combine(EqualityContract, TemplateIndex, Cell, Color);

	/// <inheritdoc/>
	protected override void PrintMembers(StringBuilder builder)
	{
		builder.Append($"{nameof(TemplateIndex)} = {TemplateIndex}, ");
		builder.Append($"{nameof(Cell)} = {Cell}, ");
		builder.Append($"{nameof(Color)} = {Color}");
	}

	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		using var fillPaint = new SKPaint { Style = SKPaintStyle.Fill, Color = Color };
		var template = canvas.Templates[TemplateIndex];
		var topLeft = template.Mapper.GetPoint(Cell, Alignment.TopLeft);
		var bottomRight = template.Mapper.GetPoint(Cell, Alignment.BottomRight);
		var rect = SKRect.Create(topLeft, bottomRight);
		canvas.BackingCanvas.DrawRect(rect, fillPaint);
	}
}
