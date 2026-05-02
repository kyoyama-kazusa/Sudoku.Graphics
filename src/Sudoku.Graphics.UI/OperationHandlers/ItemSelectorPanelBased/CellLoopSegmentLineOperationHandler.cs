namespace Sudoku.Graphics.UI.OperationHandlers.ItemSelectorPanelBased;

/// <summary>
/// Represents cell loop segment line operation handler.
/// </summary>
[OperationHandler(ItemType.Cell_LoopSegmentLine)]
public sealed class CellLoopSegmentLineOperationHandler : CellBasedItemSelectorPanelOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_LoopSegmentLine;

	/// <inheritdoc/>
	protected override DuplicateLevel ItemDuplicateLevel => DuplicateLevel.Item;

	/// <inheritdoc/>
	protected override ReadOnlySpan<Func<IIconDisplayItem>> IconsFactory
		=> (Func<IIconDisplayItem>[])[
			static () => new LoopSegmentLineDisplayItem { Direction = Direction4.Up },
			static () => new LoopSegmentLineDisplayItem { Direction = Direction4.Down },
			static () => new LoopSegmentLineDisplayItem { Direction = Direction4.Left },
			static () => new LoopSegmentLineDisplayItem { Direction = Direction4.Right }
		];

	/// <inheritdoc/>
	protected override Func<object?, Absolute, Item?> ItemFactory
		=> static (item, cell) =>
			item is LoopSegmentLineDisplayItem { Direction: var direction }
				? ItemsFactory.CellLoopSegmentLine(cell, direction)
				: null;

	/// <inheritdoc/>
	protected override Func<MainWindow, ItemSelectorPanel> PanelSelector => static window => window.CellLoopSegmentLinePanel;

	/// <inheritdoc/>
	protected override Func<MainWindow, Popup> PopupSelector => static window => window.CellLoopSegmentLinePopup;
}
