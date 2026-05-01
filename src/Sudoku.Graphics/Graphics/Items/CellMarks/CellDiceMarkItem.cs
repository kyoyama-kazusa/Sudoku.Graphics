namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents cell dice mark item.
/// </summary>
public sealed record CellDiceMarkItem : CellMarkItem, IItem_ValueProperty<int>
{
	/// <summary>
	/// Represents an array of lightup positions of all possible dice marks.
	/// </summary>
	public static readonly bool[][,] DiceLightupMatrix = [
		new[,] { { true } },
		new[,] { { true, false }, { false, true } },
		new[,] { { true, false, false }, { false, true, false }, { false, false, true } },
		new[,] { { true, true }, { true, true } },
		new[,] { { true, false, true }, { false, true, false }, { true, false, true } },
		new[,] { { true, true }, { true, true }, { true, true } },
		new[,] { { true, true, true }, { false, true, false }, { true, true, true } },
		new[,] { { true, true, true }, { true, false, true }, { true, true, true } },
		new[,] { { true, true, true }, { true, true, true }, { true, true, true } }
	];


	/// <inheritdoc/>
	public required int Value { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_Dice;


	/// <inheritdoc/>
	protected internal override void DrawTo(Canvas canvas)
		=> canvas.BackingCanvas.DrawTetromino(
			Cell,
			StrokeWidthScale,
			SizeScale,
			DiceLightupMatrix[Value],
			StrokeColor,
			FillColor,
			.2M,
			1M,
			canvas.Mapper
		);
}
