namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell cross item.
/// </summary>
public sealed record CellCrossMarkItem : CellShapeMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_Cross;


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawCrossTo(
			Cell,
			SizeScale,
			StrokeWidthScale,
			StrokeColor,
			canvas.Templates[TemplateIndex].Mapper
		);
}
