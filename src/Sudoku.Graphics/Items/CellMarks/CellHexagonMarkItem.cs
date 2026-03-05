namespace Sudoku.Items.CellMarks;

/// <summary>
/// Represents a cell hexagon mark item.
/// </summary>
public sealed class CellHexagonMarkItem : CellMarkItem
{
	/// <summary>
	/// Indicates orientation of this symbol.
	/// </summary>
	public required Orientation Orientation { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_Hexagon;

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CellHexagonMarkItem);


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
