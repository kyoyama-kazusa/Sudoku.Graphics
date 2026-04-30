namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CellBattenburgMarkItem"/>.
/// </summary>
/// <seealso cref="CellBattenburgMarkItem"/>
[OperationHandler(ItemType.Cell_Battenburg)]
public sealed class CellBattenburgOperationHandler : CellShapeOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_Battenburg;

	/// <inheritdoc/>
	public override Func<Absolute, CellMarkItem> ItemFactory => ItemsFactory.Battenburg;
}
