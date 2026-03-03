namespace Sudoku.ComponentModel.Items.CellMarks;

/// <summary>
/// Represents a cell polygon mark item.
/// </summary>
public sealed class CellPolygonMarkItem : CellMarkItem
{
	/// <summary>
	/// Indicates whether this type draws for concave polygon or not. By default it's <see langword="false"/>.
	/// </summary>
	public bool DrawConcavePolygon { get; init; } = false;

	/// <summary>
	/// Indicates rotation degrees, in angles. By default it's 0.
	/// </summary>
	public float RotationDegrees { get; init; } = 0;

	/// <summary>
	/// Indicates the number of sides.
	/// </summary>
	public required int SidesCount { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_Polygon;

	/// <summary>
	/// Indicates concave inner scale. By default it's 0.
	/// </summary>
	public Scale ConcaveInnerScale { get; init; } = 0M;

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CellPolygonMarkItem);


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		if (DrawConcavePolygon)
		{
			canvas.BackingCanvas.DrawConcavePolygonToCell(
				Cell,
				SidesCount,
				SizeScale,
				ConcaveInnerScale,
				StrokeWidthScale,
				StrokeColor,
				FillColor,
				mapper,
				RotationDegrees
			);
		}
		else
		{
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
}
