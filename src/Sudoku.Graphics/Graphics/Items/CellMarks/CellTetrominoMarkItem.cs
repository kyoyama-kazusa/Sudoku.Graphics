namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a cell tetromino mark item.
/// </summary>
public sealed record CellTetrominoMarkItem : CellMarkItem
{
	/// <summary>
	/// Indicates the piece used.
	/// </summary>
	public required Tetromino Piece { get; init; }

	/// <summary>
	/// Indicates rotation type of piece.
	/// </summary>
	public TetrominoRotationType RotationType { get; init; } = TetrominoRotationType.None;

	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_Tetromino;


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawTetromino(
			Cell,
			StrokeWidthScale,
			SizeScale,
			TetrominoLineSegmentsFactory.GetTetrominoBooleanSequence(Piece, RotationType),
			StrokeColor,
			FillColor,
			.1M,
			.3M,
			canvas.Mapper
		);
}
