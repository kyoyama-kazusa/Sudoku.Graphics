namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell cross item.
/// </summary>
public sealed record CellCrossMarkItem : CellMarkItem
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_Cross;

	/// <inheritdoc/>
	public required override Scale SizeScale { get; init; }

	/// <inheritdoc/>
	public required override Scale StrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public required override SerializableColor StrokeColor { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawCrossTo(
			Cell,
			SizeScale,
			StrokeWidthScale,
			StrokeColor,
			canvas.Mapper
		);
}
