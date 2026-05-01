namespace Sudoku.Graphics.Items.Lines;

/// <summary>
/// Represents template line item.
/// </summary>
public sealed record TemplateLineItem : LineItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.Line_Template;


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas) => canvas.Template.DrawLines(canvas.BackingCanvas);
}
