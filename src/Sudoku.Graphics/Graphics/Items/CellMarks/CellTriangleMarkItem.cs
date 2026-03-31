namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell triangle mark item.
/// </summary>
public sealed record CellTriangleMarkItem : CellMarkItem, IItem_DirectionProperty<Direction8>
{
	/// <inheritdoc/>
	public required Direction8 Direction { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_Triangle;


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var mapper = canvas.Templates[TemplateIndex].Mapper;
		canvas.BackingCanvas.DrawPolygonToCell(
			Cell,
			3,
			SizeScale,
			StrokeWidthScale,
			StrokeColor,
			FillColor,
			mapper,
			Direction.RotationDegrees
		);
	}
}
