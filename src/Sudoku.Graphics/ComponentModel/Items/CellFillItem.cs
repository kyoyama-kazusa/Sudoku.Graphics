namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents cell fill item.
/// </summary>
public sealed class CellFillItem : FillItem
{
	/// <summary>
	/// Indicates target template index.
	/// </summary>
	public required int TemplateIndex { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellFill;

	/// <summary>
	/// Indicates the cell to be filled.
	/// </summary>
	public required Absolute Cell { get; init; }

	/// <summary>
	/// Indicates the color to be filled.
	/// </summary>
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
		var topLeft = template.Mapper.GetPoint(Cell, CellAlignment.TopLeft);
		var bottomRight = template.Mapper.GetPoint(Cell, CellAlignment.BottomRight);
		var rect = SKRect.Create(topLeft, bottomRight);
		canvas.BackingCanvas.DrawRect(rect, fillPaint);
	}
}
