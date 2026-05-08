namespace Sudoku.Graphics.UI.OperationHandlers.ItemSelectorPanelBased;

/// <summary>
/// Represents an operation handler that produces <see cref="CellPairRomanNumeralTextMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellPairRomanNumeralTextMarkItem"/>
[OperationHandler(ItemType.CellPairText_RomanNumeral)]
public sealed class CellPairRomanNumeralOperationHandler : CellPairBasedItemSelectorPanelOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.CellPairText_RomanNumeral;

	/// <inheritdoc/>
	public override ReadOnlySpan<Func<ITextDisplayItem>> DisplayItemFactory
		=> (Func<RomanNumeralDisplayItem>[])[
			static () => new() { Value = 1 },
			static () => new() { Value = 4 },
			static () => new() { Value = 5 },
			static () => new() { Value = 6 },
			static () => new() { Value = 9 },
			static () => new() { Value = 10 },
			static () => new() { Value = 11 },
			static () => new() { Value = 14 },
			static () => new() { Value = 15 },
			static () => new() { Value = 16 },
			static () => new() { Value = 19 },
			static () => new() { Value = 20 }
		];

	/// <inheritdoc/>
	public override Func<MainWindow, ItemSelectorPanel> PanelSelector => static window => window.CellPairRomanNumeralPanel;

	/// <inheritdoc/>
	public override Func<MainWindow, Popup> PopupSelector => static window => window.CellPairRomanNumeralPopup;

	/// <inheritdoc/>
	public override Func<ITextDisplayItem?, Absolute, Absolute, Item?> ItemFactory
		=> static (item, cell1, cell2) =>
			item is null
				? null
				: ItemsFactory.CellPairRomanNumeral(cell1, cell2, ((RomanNumeralDisplayItem)item).Value);
}
