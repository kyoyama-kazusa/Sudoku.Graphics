namespace Sudoku.Graphics.Items.CellPairMarks;

/// <summary>
/// Represents a cell pair connection line mark item.
/// </summary>
public sealed record CellPairConnectionLineMarkItem : CellPairMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellPair_ConnectionLine;

	/// <summary>
	/// Indicates the size scale.
	/// </summary>
	public required Scale SizeScale { get; init; }

	/// <inheritdoc/>
	public override required Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public override required SerializableColor StrokeColor { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawConnectionLine(
			Cell1,
			Cell2,
			StrokeWidthScale,
			StrokeColor,
			SizeScale,
			canvas.Templates[TemplateIndex].Mapper
		);
}
