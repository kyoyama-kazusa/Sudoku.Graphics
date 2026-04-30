namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CellSevenSegmentDisplayMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellSevenSegmentDisplayMarkItem"/>
[OperationHandler(ItemType.Cell_SevenSegmentDisplay)]
public sealed class CellSevenSegmentDisplayOperationHandler : CellBasedItemSelectorPanelOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_SevenSegmentDisplay;

	/// <inheritdoc/>
	protected override ReadOnlySpan<Func<IIconDisplayItem>> IconsFactory
		=> (Func<IIconDisplayItem>[])[
			static () => new CellSevenSegmentDisplayDisplayItem { Value = 1 },
			static () => new CellSevenSegmentDisplayDisplayItem { Value = 2 },
			static () => new CellSevenSegmentDisplayDisplayItem { Value = 3 },
			static () => new CellSevenSegmentDisplayDisplayItem { Value = 4 },
			static () => new CellSevenSegmentDisplayDisplayItem { Value = 5 },
			static () => new CellSevenSegmentDisplayDisplayItem { Value = 6 },
			static () => new CellSevenSegmentDisplayDisplayItem { Value = 7 },
			static () => new CellSevenSegmentDisplayDisplayItem { Value = 8 },
			static () => new CellSevenSegmentDisplayDisplayItem { Value = 9 }
		];

	/// <inheritdoc/>
	protected override Func<object?, Absolute, Item?> ItemFactory
		=> static (item, cell) =>
			item is CellSevenSegmentDisplayDisplayItem { Value: var value }
				? ItemsFactory.CellSevenSegmentDisplay(cell, value)
				: null;

	/// <inheritdoc/>
	protected override Func<MainWindow, ItemSelectorPanel> PanelSelector => static window => window.CellSevenSegmentDisplayPanel;

	/// <inheritdoc/>
	protected override Func<MainWindow, Popup> PopupSelector => static window => window.CellSevenSegmentDisplayPopup;
}
