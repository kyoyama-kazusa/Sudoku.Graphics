namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell square mark item.
/// </summary>
public sealed record CellSquareMarkItem : CellMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_Square;


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawSquareToCell(
			Cell,
			SizeScale,
			StrokeColor,
			StrokeWidthScale,
			FillColor,
			CornerRadiusScale,
			canvas.Templates[TemplateIndex].Mapper
		);
}
