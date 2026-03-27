namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Provides with a cell phantom dice mark item.
/// </summary>
public sealed record CellPhantomDiceMarkItem : CellMarkItem
{
	/// <summary>
	/// Indicates subgrid size.
	/// </summary>
	public required Relative SubgridSize { get; init; }

	/// <summary>
	/// Indicates scale of phantom stroke width, related to cell size.
	/// </summary>
	public required Scale PhantomStrokeWidthScale { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_PhantomDice;

	/// <summary>
	/// Indicates the states.
	/// </summary>
	public required BitArray States { get; init; }


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawPhantomDiceToCell(
			Cell,
			SubgridSize,
			SizeScale,
			StrokeColor,
			StrokeWidthScale,
			PhantomStrokeWidthScale,
			FillColor,
			States,
			canvas.Templates[TemplateIndex].Mapper
		);
}
