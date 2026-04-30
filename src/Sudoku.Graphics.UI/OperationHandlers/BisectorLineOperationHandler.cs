namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CellBisectorLineMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellBisectorLineMarkItem"/>
[OperationHandler(ItemType.Cell_BisectorLine)]
public sealed class BisectorLineOperationHandler : CellBasedItemSelectorPanelOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_BisectorLine;

	/// <inheritdoc/>
	protected override ReadOnlySpan<Func<IIconDisplayItem>> IconsFactory
		=> (Func<IIconDisplayItem>[])[
			static () => new BisectorLineDisplayItem { Orientation = Orientation4.Horizontal },
			static () => new BisectorLineDisplayItem { Orientation = Orientation4.Vertical },
			static () => new BisectorLineDisplayItem { Orientation = Orientation4.Slash },
			static () => new BisectorLineDisplayItem { Orientation = Orientation4.Backslash }
		];

	/// <inheritdoc/>
	protected override Func<object?, Absolute, Item?> ItemFactory
		=> static (item, cell) =>
			item is BisectorLineDisplayItem { Orientation: var orientation }
				? ItemsFactory.CellBisectorLine(cell, orientation)
				: null;

	/// <inheritdoc/>
	protected override Func<MainWindow, ItemSelectorPanel> PanelSelector => static window => window.CellBisectorLinePanel;

	/// <inheritdoc/>
	protected override Func<MainWindow, Popup> PopupSelector => static window => window.CellBisectorLinePopup;
}
