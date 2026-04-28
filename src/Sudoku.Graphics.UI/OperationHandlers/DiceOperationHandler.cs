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

	/// <inheritdoc/>
	public override IReadOnlyDictionary<string, Func<IIconDisplayItem>> IconDisplayItemFactory
		=> new Dictionary<string, Func<IIconDisplayItem>>
		{
			{ "Dice_0", static () => new DiceDisplayItem { Value = 0 } },
			{ "Dice_1", static () => new DiceDisplayItem { Value = 1 } },
			{ "Dice_2", static () => new DiceDisplayItem { Value = 2 } },
			{ "Dice_3", static () => new DiceDisplayItem { Value = 3 } },
			{ "Dice_4", static () => new DiceDisplayItem { Value = 4 } },
			{ "Dice_5", static () => new DiceDisplayItem { Value = 5 } },
			{ "Dice_6", static () => new DiceDisplayItem { Value = 6 } },
			{ "Dice_7", static () => new DiceDisplayItem { Value = 7 } },
			{ "Dice_8", static () => new DiceDisplayItem { Value = 8 } }
		};
}
