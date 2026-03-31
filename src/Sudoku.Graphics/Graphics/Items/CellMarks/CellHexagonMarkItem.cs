namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell hexagon mark item.
/// </summary>
public sealed record CellHexagonMarkItem : CellMarkItem, IItem_OrientationProperty<Orientation2>
{
	/// <inheritdoc/>
	public required Orientation2 Orientation { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_Hexagon;


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		canvas.BackingCanvas.DrawPolygonToCell(
			Cell,
			6,
			SizeScale,
			StrokeWidthScale,
			StrokeColor,
			FillColor,
			mapper,
			Orientation switch
			{
				Orientation2.Horizontal => 30,
				Orientation2.Vertical => 0,
				_ => throw new InvalidOperationException($"{nameof(Orientation)} is not defined or invalid.")
			}
		);
	}
}
