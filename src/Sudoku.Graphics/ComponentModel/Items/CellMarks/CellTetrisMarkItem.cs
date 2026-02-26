namespace Sudoku.ComponentModel.Items.CellMarks;

/// <summary>
/// Represents a cell tetris mark item.
/// </summary>
public sealed class CellTetrisMarkItem : CellMarkItem
{
	/// <summary>
	/// Indicates the piece used.
	/// </summary>
	public required Piece Piece { get; init; }

	/// <summary>
	/// Indicates rotation type of piece.
	/// </summary>
	public RotationType RotationType { get; init; } = RotationType.None;

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_Tetris;

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CellTetrisMarkItem);


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
	{
		var template = canvas.Templates[TemplateIndex];
		var mapper = template.Mapper;
		canvas.BackingCanvas.DrawTetromino(
			Cell,
			StrokeWidthScale,
			SizeScale,
			TetrisLineSegmentFactory.GetTetrisPieceBooleanSequence(Piece, RotationType),
			StrokeColor,
			FillColor,
			.1M,
			.3M,
			mapper
		);
	}
}
