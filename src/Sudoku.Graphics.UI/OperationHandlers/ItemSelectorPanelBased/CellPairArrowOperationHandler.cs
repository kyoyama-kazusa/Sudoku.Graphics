namespace Sudoku.Graphics.UI.OperationHandlers.ItemSelectorPanelBased;

/// <summary>
/// Represents an operation handler that produces <see cref="CellPairArrowTextMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellPairArrowTextMarkItem"/>
[OperationHandler(ItemType.CellPairText_Arrow)]
public sealed class CellPairArrowOperationHandler : CellPairBasedItemSelectorPanelOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.CellPairText_Arrow;

	/// <inheritdoc/>
	public override ReadOnlySpan<Func<ITextDisplayItem>> DisplayItemFactory
		=> (Func<ArrowDirectionDisplayItem>[])[
			static () => new() { Direction = Direction8.Up },
			static () => new() { Direction = Direction8.Down },
			static () => new() { Direction = Direction8.Left },
			static () => new() { Direction = Direction8.Right },
			static () => new() { Direction = Direction8.LeftUp },
			static () => new() { Direction = Direction8.RightUp },
			static () => new() { Direction = Direction8.LeftDown },
			static () => new() { Direction = Direction8.RightDown }
		];

	/// <inheritdoc/>
	public override Func<MainWindow, ItemSelectorPanel> PanelSelector => static window => window.CellPairArrowPanel;

	/// <inheritdoc/>
	public override Func<MainWindow, Popup> PopupSelector => static window => window.CellPairArrowPopup;

	/// <inheritdoc/>
	public override Func<ITextDisplayItem, Absolute, Absolute, Item> ItemFactory
		=> static (item, cell1, cell2) => ItemsFactory.CellPairArrow(cell1, cell2, ((ArrowDirectionDisplayItem)item).Direction);
}
