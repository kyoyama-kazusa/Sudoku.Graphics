namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell hexagon mark item.
/// </summary>
public sealed record CellHexagonMarkItem : CellMarkItem, IItem_OrientationProperty<Orientation2>
{
	/// <inheritdoc/>
	public required Orientation2 Orientation { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_Hexagon;


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		canvas.BackingCanvas.DrawHexagonToCell(
			Cell,
			SizeScale,
			StrokeColor,
			StrokeWidthScale,
			FillColor,
			Orientation,
			mapper
		);
	}
}
