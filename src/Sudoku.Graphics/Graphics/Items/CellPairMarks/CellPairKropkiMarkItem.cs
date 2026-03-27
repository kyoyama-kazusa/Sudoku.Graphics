namespace Sudoku.Graphics.Items.CellPairMarks;

/// <summary>
/// Represents a cell pair kropki (circle) mark item.
/// </summary>
public sealed record CellPairKropkiMarkItem : CellPairMarkItem
{
	/// <summary>
	/// Indicates whether the mark should be drawn solid.
	/// </summary>
	public required bool IsSolid { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellPair_Kropki;

	/// <summary>
	/// Indicates the scale of size of circle drawn, related to cell size.
	/// </summary>
	public required Scale SizeScale { get; init; }

	/// <inheritdoc/>
	public override required Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public override required SerializableColor StrokeColor { get; init; }

	/// <inheritdoc/>
	public override required SerializableColor FillColor { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawKropki(
			Cell1,
			Cell2,
			IsSolid,
			SizeScale,
			StrokeWidthScale,
			StrokeColor,
			FillColor,
			canvas.Templates[TemplateIndex].Mapper
		);
}
