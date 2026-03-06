namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Provides cell diamond mark item.
/// </summary>
public sealed record CellDiamondMarkItem : CellMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_Diamond;


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		canvas.BackingCanvas.DrawDiamondToCell(
			Cell,
			SizeScale,
			StrokeColor,
			StrokeWidthScale,
			FillColor,
			mapper
		);
	}
}
