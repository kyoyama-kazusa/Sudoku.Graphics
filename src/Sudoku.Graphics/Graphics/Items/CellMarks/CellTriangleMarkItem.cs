namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell triangle mark item.
/// </summary>
public sealed record CellTriangleMarkItem : CellMarkItem, IItem_DirectionProperty<Direction8>
{
	/// <inheritdoc/>
	public required Direction8 Direction { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_Triangle;


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawTriangleToCell(
			Cell,
			Direction,
			SizeScale,
			StrokeColor,
			StrokeWidthScale,
			FillColor,
			canvas.Templates[TemplateIndex].Mapper
		);
}
