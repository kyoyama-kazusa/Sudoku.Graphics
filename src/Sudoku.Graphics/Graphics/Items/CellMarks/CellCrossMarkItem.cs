namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell cross item.
/// </summary>
public sealed record CellCrossMarkItem : CellMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_Cross;


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		canvas.BackingCanvas.DrawCrossTo(
			Cell,
			SizeScale,
			StrokeWidthScale,
			StrokeColor,
			mapper
		);
	}
}
