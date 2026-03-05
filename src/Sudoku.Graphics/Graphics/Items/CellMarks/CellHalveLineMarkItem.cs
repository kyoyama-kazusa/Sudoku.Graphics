namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents cell halve line mark item.
/// </summary>
public sealed class CellHalveLineMarkItem : CellMarkItem
{
	/// <summary>
	/// Indicates the orientation.
	/// </summary>
	public required Orientation4 Orientation { get; init; }

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
			Orientation,
			SizeScale,
			StrokeColor,
			StrokeWidthScale,
			mapper
		);
	}
}
