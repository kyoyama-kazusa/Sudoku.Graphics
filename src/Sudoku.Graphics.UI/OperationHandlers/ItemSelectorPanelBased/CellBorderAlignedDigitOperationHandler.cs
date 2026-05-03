namespace Sudoku.Graphics.UI.OperationHandlers.ItemSelectorPanelBased;

/// <summary>
/// Represents an operation handler that produces <see cref="CellBorderAlignedDigitTextMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellBorderAlignedDigitTextMarkItem"/>
[OperationHandler(ItemType.CellText_BorderAlignedDigit)]
public sealed class CellBorderAlignedDigitOperationHandler : CellBasedItemSelectorPanelOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.CellText_BorderAlignedDigit;

	/// <inheritdoc/>
	protected override DuplicateLevel ItemDuplicateLevel => DuplicateLevel.Item;

	/// <inheritdoc/>
	protected override ReadOnlySpan<Func<IIconDisplayItem>> IconsFactory
		=> (Func<IIconDisplayItem>[])[
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 0, Alignment = Alignment.TopLeft },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 1, Alignment = Alignment.TopLeft },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 2, Alignment = Alignment.TopLeft },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 3, Alignment = Alignment.TopLeft },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 4, Alignment = Alignment.TopLeft },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 5, Alignment = Alignment.TopLeft },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 6, Alignment = Alignment.TopLeft },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 7, Alignment = Alignment.TopLeft },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 8, Alignment = Alignment.TopLeft },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 0, Alignment = Alignment.TopRight },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 1, Alignment = Alignment.TopRight },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 2, Alignment = Alignment.TopRight },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 3, Alignment = Alignment.TopRight },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 4, Alignment = Alignment.TopRight },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 5, Alignment = Alignment.TopRight },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 6, Alignment = Alignment.TopRight },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 7, Alignment = Alignment.TopRight },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 8, Alignment = Alignment.TopRight },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 0, Alignment = Alignment.BottomLeft },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 1, Alignment = Alignment.BottomLeft },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 2, Alignment = Alignment.BottomLeft },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 3, Alignment = Alignment.BottomLeft },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 4, Alignment = Alignment.BottomLeft },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 5, Alignment = Alignment.BottomLeft },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 6, Alignment = Alignment.BottomLeft },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 7, Alignment = Alignment.BottomLeft },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 8, Alignment = Alignment.BottomLeft },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 0, Alignment = Alignment.BottomRight },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 1, Alignment = Alignment.BottomRight },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 2, Alignment = Alignment.BottomRight },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 3, Alignment = Alignment.BottomRight },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 4, Alignment = Alignment.BottomRight },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 5, Alignment = Alignment.BottomRight },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 6, Alignment = Alignment.BottomRight },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 7, Alignment = Alignment.BottomRight },
			static () => new CellBorderAlignedDigitDisplayItem { Digit = 8, Alignment = Alignment.BottomRight }
		];

	/// <inheritdoc/>
	protected override Func<object?, Absolute, Item?> ItemFactory
		=> (item, cell) => item is CellBorderAlignedDigitDisplayItem { Digit: var digit, Alignment: Alignment alignment }
			? ItemsFactory.CellBorderAlignedDigit(cell, digit, alignment)
			: null;

	/// <inheritdoc/>
	protected override Func<MainWindow, ItemSelectorPanel> PanelSelector => static window => window.CellBorderAlignedDigitPanel;

	/// <inheritdoc/>
	protected override Func<MainWindow, Popup> PopupSelector => static window => window.CellBorderAlignedDigitPopup;
}
