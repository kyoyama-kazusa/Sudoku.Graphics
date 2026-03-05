namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell circle mark item.
/// </summary>
public sealed class CellCircleMarkItem : CellMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_Circle;

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CellCircleMarkItem);


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		canvas.BackingCanvas.DrawCircleToCell(
			Cell,
			SizeScale,
			StrokeColor,
			StrokeWidthScale,
			FillColor,
			mapper
		);
	}
}
