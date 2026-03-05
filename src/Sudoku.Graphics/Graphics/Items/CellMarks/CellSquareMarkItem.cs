namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell square mark item.
/// </summary>
public sealed class CellSquareMarkItem : CellMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_Square;

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CellSquareMarkItem);


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		canvas.BackingCanvas.DrawSquareToCell(
			Cell,
			SizeScale,
			StrokeColor,
			StrokeWidthScale,
			FillColor,
			CornerRadiusScale,
			mapper
		);
	}
}
