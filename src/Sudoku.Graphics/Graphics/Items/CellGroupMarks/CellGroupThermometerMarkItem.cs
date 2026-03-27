namespace Sudoku.Graphics.Items.CellGroupMarks;

/// <summary>
/// Represents cell group thermometer mark item.
/// </summary>
public sealed record CellGroupThermometerMarkItem : CellGroupMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellGroupMark_Thermometer;

	/// <summary>
	/// Indicates scale of circle.
	/// </summary>
	public required Scale CircleScale { get; init; }

	/// <inheritdoc/>
	public override required Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public override required SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public override required SerializableColor FillColor { get; init; }


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
