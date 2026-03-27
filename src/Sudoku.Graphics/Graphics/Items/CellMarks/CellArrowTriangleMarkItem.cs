namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell arrow triangle mark item.
/// </summary>
public sealed record CellArrowTriangleMarkItem : CellMarkItem, IItem_DirectionProperty<Direction8>
{
	/// <inheritdoc/>
	public required Direction8 Direction { get; init; }

	/// <summary>
	/// Indicates the base scale.
	/// </summary>
	public required Scale BaseScale { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_ArrowTriangle;


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
