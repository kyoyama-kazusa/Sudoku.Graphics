namespace Sudoku.Graphics.Items.Fills;

/// <summary>
/// Represents cell fill item.
/// </summary>
public sealed record CellFillItem : FillItem, IItem_CellProperty, IItem_TemplateIndexProperty
{
	/// <inheritdoc/>
	public required int TemplateIndex { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellFill;

	/// <inheritdoc/>
	public required Absolute Cell { get; init; }


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
