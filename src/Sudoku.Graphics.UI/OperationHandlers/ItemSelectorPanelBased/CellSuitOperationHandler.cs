namespace Sudoku.Graphics.UI.OperationHandlers.ItemSelectorPanelBased;

/// <summary>
/// Represents an operation handler that produces suit mark items.
/// </summary>
[OperationHandler(ItemType.CellText_Suit)]
public sealed class CellSuitOperationHandler : CellBasedItemSelectorPanelOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.CellText_Suit;

	/// <inheritdoc/>
	protected override ReadOnlySpan<Func<IIconDisplayItem>> IconsFactory
		=> (Func<IIconDisplayItem>[])[
			static () => new CellSuitDisplayItem { Suit = Suit.Heart },
			static () => new CellSuitDisplayItem { Suit = Suit.Spade },
			static () => new CellSuitDisplayItem { Suit = Suit.Diamond },
			static () => new CellSuitDisplayItem { Suit = Suit.Club }
		];

	/// <inheritdoc/>
	protected override Func<object?, Absolute, Item?> ItemFactory
		=> static (item, cell) => item is CellSuitDisplayItem { Suit: var suit } ? ItemsFactory.CellSuit(cell, suit) : null;

	/// <inheritdoc/>
	protected override Func<MainWindow, ItemSelectorPanel> PanelSelector => static window => window.CellSuitPanel;

	/// <inheritdoc/>
	protected override Func<MainWindow, Popup> PopupSelector => static window => window.CellSuitPopup;
}
