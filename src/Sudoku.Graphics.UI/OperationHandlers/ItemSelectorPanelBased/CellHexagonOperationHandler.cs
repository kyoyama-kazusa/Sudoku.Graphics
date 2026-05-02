namespace Sudoku.Graphics.UI.OperationHandlers.ItemSelectorPanelBased;

/// <summary>
/// Represents an operation handler that produces <see cref="CellHexagonMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellHexagonMarkItem"/>
[OperationHandler(ItemType.Cell_Hexagon)]
public sealed class CellHexagonOperationHandler : CellBasedItemSelectorPanelOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_Hexagon;

	/// <inheritdoc/>
	protected override ReadOnlySpan<Func<IIconDisplayItem>> IconsFactory
		=> (Func<IIconDisplayItem>[])[
			static () => new HexagonDisplayItem { Orientation = Orientation2.Horizontal },
			static () => new HexagonDisplayItem { Orientation = Orientation2.Vertical }
		];

	/// <inheritdoc/>
	protected override Func<object?, Absolute, Item?> ItemFactory
		=> static (item, cell) => item is HexagonDisplayItem { Orientation: var orientation } ? ItemsFactory.Hexagon(cell, orientation) : null;

	/// <inheritdoc/>
	protected override Func<MainWindow, ItemSelectorPanel> PanelSelector => static window => window.CellHexagonPanel;

	/// <inheritdoc/>
	protected override Func<MainWindow, Popup> PopupSelector => static window => window.CellHexagonPopup;
}
