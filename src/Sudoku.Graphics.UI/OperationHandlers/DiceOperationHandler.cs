namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CellDiceMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellDiceMarkItem"/>
[OperationHandler(ItemType.Cell_Dice)]
public sealed class DiceOperationHandler : CellBasedItemSelectorPanelOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_Dice;

	/// <inheritdoc/>
	public override Func<object?, Absolute, Item?> ItemFactory
		=> static (item, cell) => item switch
		{
			DiceDisplayItem { Value: var value } => ItemsFactory.Dice(cell, value),
			_ => null
		};

	/// <inheritdoc/>
	public override Func<MainWindow, Popup> PopupSelector => static window => window.DiceSelectorPopup;

	/// <inheritdoc/>
	public override Func<MainWindow, ItemSelectorPanel> PanelSelector => static window => window.DiceSelectorPanel;
}
