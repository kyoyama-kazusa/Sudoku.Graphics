namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents cell border-aligned arrow mark item.
/// </summary>
public sealed record CellBorderAlignedArrowMarkItem : CellMarkItem, IItem_DirectionProperty<Direction4>
{
	/// <summary>
	/// Indicates arrows padding scale.
	/// </summary>
	public required Scale PaddingScale { get; init; }

	/// <inheritdoc/>
	public required Direction4 Direction { get; init; }

	/// <summary>
	/// Indicates the rotation direction.
	/// </summary>
	public required RotationDirection RotationDirection { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_BorderAlignedArrow;


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var originalDirection = Direction switch
		{
			Direction4.Up => Direction4.Left,
			Direction4.Down => Direction4.Right,
			Direction4.Left => Direction4.Down,
			Direction4.Right => Direction4.Up,
			_ => throw new NotSupportedException($"Invalid direction '{Direction}'.")
		};
		canvas.BackingCanvas.DrawArrowToCell(
			Cell,
			Direction.AsDirection8(),
			.15M,
			.1M,
			.025M,
			SizeScale, // ShaftHeightScale
			StrokeWidthScale,
			StrokeColor,
			FillColor,
			RotationDirection == RotationDirection.Clockwise ? originalDirection : originalDirection.Reversed,
			PaddingScale,
			canvas.Templates[TemplateIndex].Mapper
		);
	}
}
