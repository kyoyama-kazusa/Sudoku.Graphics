namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell circle mark item.
/// </summary>
public sealed record CellCircleMarkItem : CellShapeMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_Circle;


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawCircleTo(
			Cell,
			SizeScale,
			StrokeColor,
			StrokeWidthScale,
			FillColor,
			canvas.Templates[TemplateIndex].Mapper
		);
}
