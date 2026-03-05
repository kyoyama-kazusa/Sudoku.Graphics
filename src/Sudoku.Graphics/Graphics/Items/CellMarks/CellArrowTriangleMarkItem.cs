namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell arrow triangle mark item.
/// </summary>
public sealed class CellArrowTriangleMarkItem : CellMarkItem
{
	/// <summary>
	/// Indicates the arrow direction.
	/// </summary>
	public required ArrowDirection Direction { get; init; }

	/// <summary>
	/// Indicates the base scale.
	/// </summary>
	public required Scale BaseScale { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_ArrowTriangle;

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CellArrowTriangleMarkItem);


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		canvas.BackingCanvas.DrawArrowTriangleToCell(
			Cell,
			Direction,
			SizeScale,
			BaseScale,
			StrokeWidthScale,
			StrokeColor,
			FillColor,
			mapper
		);
	}
}
