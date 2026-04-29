namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CellDiceMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellDiceMarkItem"/>
[OperationHandler(ItemType.Cell_Dice)]
public sealed class CellDiceOperationHandler : CellBasedItemSelectorPanelOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_Dice;

	/// <inheritdoc/>
	protected override ReadOnlySpan<Func<IIconDisplayItem>> IconsFactory
		=> (Func<IIconDisplayItem>[])[
			static () => new DiceDisplayItem { Value = 0 },
			static () => new DiceDisplayItem { Value = 1 },
			static () => new DiceDisplayItem { Value = 2 },
			static () => new DiceDisplayItem { Value = 3 },
			static () => new DiceDisplayItem { Value = 4 },
			static () => new DiceDisplayItem { Value = 5 },
			static () => new DiceDisplayItem { Value = 6 },
			static () => new DiceDisplayItem { Value = 7 },
			static () => new DiceDisplayItem { Value = 8 }
		];

	/// <inheritdoc/>
	protected override Func<object?, Absolute, Item?> ItemFactory
		=> static (item, cell) => item is DiceDisplayItem { Value: var value } ? ItemsFactory.Dice(cell, value) : null;

	/// <inheritdoc/>
	protected override Func<MainWindow, Popup> PopupSelector => static window => window.DiceSelectorPopup;

	/// <inheritdoc/>
	protected override Func<MainWindow, ItemSelectorPanel> PanelSelector => static window => window.DiceSelectorPanel;

	/// <inheritdoc/>
	protected override Func<IIconDisplayItem, Item?> SampleItemFactory
		=> static item => ItemsFactory.Dice(0, ((DiceDisplayItem)item).Value, true);
}
