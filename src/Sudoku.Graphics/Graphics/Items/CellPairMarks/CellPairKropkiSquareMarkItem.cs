namespace Sudoku.Graphics.Items.CellPairMarks;

/// <summary>
/// Represents a cell pair kropki (square) mark item.
/// </summary>
public sealed record CellPairKropkiSquareMarkItem : CellPairMarkItem
{
	/// <summary>
	/// Indicates whether the mark should be drawn solid.
	/// </summary>
	public required bool IsSolid { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellPair_KropkiSquare;

	/// <summary>
	/// Indicates the scale of size of square drawn, related to cell size.
	/// </summary>
	public required Scale SizeScale { get; init; }

	/// <inheritdoc/>
	public required override Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public required override Scale CornerRadiusScale { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor FillColor { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawKropkiSquare(
			Cell1,
			Cell2,
			IsSolid,
			SizeScale,
			StrokeWidthScale,
			StrokeColor,
			FillColor,
			CornerRadiusScale,
			canvas.Templates[TemplateIndex].Mapper
		);
}
