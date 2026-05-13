namespace Sudoku.Graphics.Items.CellPairMarks;

/// <summary>
/// Represents a cell pair battenburg mark item.
/// </summary>
public sealed record CellPairBattenburgMarkItem : CellPairMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellPair_Battenburg;

	/// <summary>
	/// Indicates the color 1.
	/// </summary>
	public required SerializableColor Color1 { get; init; }

	/// <summary>
	/// Indicates the color 2.
	/// </summary>
	public required SerializableColor Color2 { get; init; }

	/// <inheritdoc/>
	public override SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public override Scale StrokeWidthScale { get; init; }

	/// <summary>
	/// Indicates the scale of size of mark, related to cell size.
	/// </summary>
	public required Scale SizeScale { get; init; }

	/// <summary>
	/// Indicates uniform corner radius.
	/// </summary>
	public required Scale UniformCornerRadiusScale { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var mapper = canvas.Mapper;

		var (cell1, cell2) = (Cell1, Cell2);
		if (cell1 > cell2)
		{
			(cell1, _) = (cell2, cell1);
		}

		canvas.BackingCanvas.DrawBattenburg(
			mapper.GetPointBetweenWithAdjacentRelation(cell1, cell2, out _),
			SizeScale,
			Color1,
			Color2,
			StrokeColor,
			StrokeWidthScale,
			[UniformCornerRadiusScale, UniformCornerRadiusScale, UniformCornerRadiusScale, UniformCornerRadiusScale],
			mapper
		);
	}
}
