namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CellPairKropkiTriangleMarkItem"/>.
/// </summary>
/// <seealso cref="CellPairKropkiTriangleMarkItem"/>
[OperationHandler(ItemType.CellPair_KropkiTriangle)]
public sealed class CellPairKropkiTriangleOperationHandler : CellPairKropkiOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.CellPair_KropkiTriangle;

	/// <inheritdoc/>
	public override Func<Absolute, Absolute, bool, Item> ItemFactory => ItemsFactory.CellPairKropkiTriangle;
}
