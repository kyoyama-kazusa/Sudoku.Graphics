namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell mark item that renders a list of triangles, surrounding with cell center.
/// </summary>
public sealed record CellSurroundingTrianglesMarkItem : CellMarkItem
{
	/// <summary>
	/// Indicates the number of triangles.
	/// </summary>
	public required int TrianglesCount { get; init; }

	/// <summary>
	/// Indicates the tip distance with cell center point.
	/// </summary>
	public required Scale TipDistanceScale { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_SurroundingTriangles;


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		canvas.BackingCanvas.DrawSurroundingTrianglesInCell(
			Cell,
			TrianglesCount,
			SizeScale,
			StrokeColor,
			StrokeWidthScale,
			FillColor,
			TipDistanceScale,
			CornerRadiusScale,
			mapper
		);
	}
}
