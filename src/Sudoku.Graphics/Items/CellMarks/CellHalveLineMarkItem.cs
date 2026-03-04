namespace Sudoku.Items.CellMarks;

/// <summary>
/// Represents cell halve line mark item.
/// </summary>
public sealed class CellHalveLineMarkItem : CellMarkItem
{
	/// <summary>
	/// Indicates the direction.
	/// </summary>
	public required ArrowDirection Direction { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_HalveLine;

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CellHalveLineMarkItem);


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		canvas.BackingCanvas.DrawHalveLineToCell(
			Cell,
			Direction,
			SizeScale,
			StrokeColor,
			StrokeWidthScale,
			mapper
		);
	}
}
