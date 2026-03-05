namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell cross item.
/// </summary>
public sealed class CellCrossMarkItem : CellMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_Cross;

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CellCrossMarkItem);


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		canvas.BackingCanvas.DrawCrossInCell(
			Cell,
			SizeScale,
			StrokeWidthScale,
			StrokeColor,
			mapper
		);
	}
}
