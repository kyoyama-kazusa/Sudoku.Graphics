namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell arrow mark item.
/// </summary>
public sealed record CellArrowMarkItem : CellMarkItem, IItem_DirectionProperty<Direction8>
{
	/// <inheritdoc/>
	public required Direction8 Direction { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_Arrow;

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
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawArrowToCell(
			Cell,
			Direction,
			TriangleWidthScale,
			TriangleHeightScale,
			ShaftWidthScale,
			ShaftHeightScale,
			StrokeWidthScale,
			StrokeColor,
			FillColor,
			Direction4.None,
			default,
			canvas.Templates[TemplateIndex].Mapper
		);
}
