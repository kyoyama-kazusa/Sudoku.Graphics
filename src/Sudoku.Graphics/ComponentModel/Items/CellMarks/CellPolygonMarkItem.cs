namespace Sudoku.ComponentModel.Items.CellMarks;

/// <summary>
/// Represents a cell polygon mark item.
/// </summary>
public sealed class CellPolygonMarkItem : CellMarkItem
{
	/// <summary>
	/// Indicates the number of sides.
	/// </summary>
	public required int SidesCount { get; init; }

	/// <summary>
	/// Indicates rotation degrees, in angles. By default it's 0.
	/// </summary>
	public float RotationDegrees { get; init; } = 0;

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_Polygon;

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CellPolygonMarkItem);


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		canvas.BackingCanvas.DrawPolygonToCell(
			Cell,
			SidesCount,
			SizeScale,
			StrokeWidthScale,
			StrokeColor,
			FillColor,
			mapper,
			RotationDegrees
		);
	}
}
