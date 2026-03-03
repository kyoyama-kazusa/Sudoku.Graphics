namespace Sudoku.ComponentModel.Items.CellMarks;

/// <summary>
/// Represents a cell arrow mark item.
/// </summary>
public sealed class CellArrowMarkItem : CellMarkItem
{
	/// <summary>
	/// Indicates the direction.
	/// </summary>
	public required ArrowDirection Direction { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_Arrow;

	/// <summary>
	/// Indicates the triangle width scale, related to cell size.
	/// </summary>
	public required Scale TriangleWidthScale { get; init; }

	/// <summary>
	/// Indicates the triangle height scale, related to cell size.
	/// </summary>
	public required Scale TriangleHeightScale { get; init; }

	/// <summary>
	/// Indicates the shaft width scale, related to cell size.
	/// </summary>
	public required Scale ShaftWidthScale { get; init; }

	/// <summary>
	/// Indicates the shaft height scale, related to cell size.
	/// </summary>
	public required Scale ShaftHeightScale { get; init; }

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CellArrowMarkItem);


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		canvas.BackingCanvas.DrawArrowToCell(
			Cell,
			Direction,
			TriangleWidthScale,
			TriangleHeightScale,
			ShaftWidthScale,
			ShaftHeightScale,
			StrokeWidthScale,
			StrokeColor,
			FillColor,
			mapper
		);
	}
}
