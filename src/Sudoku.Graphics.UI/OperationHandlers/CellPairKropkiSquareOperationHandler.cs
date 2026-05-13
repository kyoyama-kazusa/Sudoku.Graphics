namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CellPairKropkiSquareMarkItem"/>.
/// </summary>
/// <seealso cref="CellPairKropkiSquareMarkItem"/>
[OperationHandler(ItemType.CellPair_KropkiSquare)]
public sealed class CellPairKropkiSquareOperationHandler : CellPairKropkiOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.CellPair_KropkiSquare;

	/// <inheritdoc/>
	public override Func<Absolute, Absolute, bool, Item> ItemFactory => ItemsFactory.CellPairKropkiSquare;
}
