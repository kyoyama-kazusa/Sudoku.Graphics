namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell triangle mark item.
/// </summary>
public sealed class CellTriangleMarkItem : CellMarkItem
{
	/// <summary>
	/// Indicates the direction.
	/// </summary>
	public required Direction8 Direction { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_Triangle;

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CellTriangleMarkItem);


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		canvas.BackingCanvas.DrawTriangleToCell(
			Cell,
			Direction,
			SizeScale,
			StrokeColor,
			StrokeWidthScale,
			FillColor,
			mapper
		);
	}
}
