namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CellPairKropkiMarkItem"/>.
/// </summary>
/// <seealso cref="CellPairKropkiMarkItem"/>
[OperationHandler(ItemType.CellPair_Kropki)]
public sealed class CellPairKropkiCircleOperationHandler : CellPairKropkiOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.CellPair_Kropki;

	/// <inheritdoc/>
	public override Func<Absolute, Absolute, bool, Item> ItemFactory => ItemsFactory.CellPairKropki;
}
