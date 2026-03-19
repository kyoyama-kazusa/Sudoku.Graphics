namespace Sudoku.Graphics.Items.CellGroupMarks;

/// <summary>
/// Represents cell group thermometer mark item.
/// </summary>
public sealed record CellGroupThermometerMarkItem : CellGroupMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellGroup_Thermometer;

	/// <summary>
	/// Indicates scale of circle.
	/// </summary>
	public required Scale CircleScale { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawThermometer(
			Cells,
			StrokeWidthScale,
			StrokeColor,
			CircleScale,
			FillColor,
			canvas.Templates[TemplateIndex].Mapper
		);
}
