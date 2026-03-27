namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell border-aligned triangle mark item.
/// </summary>
public sealed record CellBorderAlignedTriangleMarkItem : CellMarkItem, IItem_DirectionProperty<Direction4>
{
	/// <summary>
	/// Indicates the direction, meaning which direction the triangle will be put in.
	/// </summary>
	public required Direction4 AlignedDirection { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_BorderAlignedTriangle;

	/// <inheritdoc/>
	Direction4 IItem_DirectionProperty<Direction4>.Direction
	{
		get => AlignedDirection;

		init => AlignedDirection = value;
	}


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawBorderAlignedTriangleToCell(
			Cell,
			AlignedDirection,
			SizeScale,
			StrokeColor,
			StrokeWidthScale,
			FillColor,
			canvas.Templates[TemplateIndex].Mapper
		);
}
