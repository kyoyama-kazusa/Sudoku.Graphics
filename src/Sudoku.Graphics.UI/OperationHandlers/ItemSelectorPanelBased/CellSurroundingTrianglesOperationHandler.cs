namespace Sudoku.Graphics.UI.OperationHandlers.ItemSelectorPanelBased;

/// <summary>
/// Represents an operation handler that produces <see cref="CellSurroundingTrianglesMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellSurroundingTrianglesMarkItem"/>
[OperationHandler(ItemType.Cell_SurroundingTriangles)]
public sealed class CellSurroundingTrianglesOperationHandler : CellBasedItemSelectorPanelOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_SurroundingTriangles;

	/// <inheritdoc/>
	protected override ReadOnlySpan<Func<IIconDisplayItem>> IconsFactory
		=> (Func<IIconDisplayItem>[])[
			static () => new SurroundingTrianglesDisplayItem { Value = 1 },
			static () => new SurroundingTrianglesDisplayItem { Value = 2 },
			static () => new SurroundingTrianglesDisplayItem { Value = 3 },
			static () => new SurroundingTrianglesDisplayItem { Value = 4 },
			static () => new SurroundingTrianglesDisplayItem { Value = 5 },
			static () => new SurroundingTrianglesDisplayItem { Value = 6 }
		];

	/// <inheritdoc/>
	protected override Func<object?, Absolute, Item?> ItemFactory
		=> static (item, cell) => item is SurroundingTrianglesDisplayItem { Value: var value } ? ItemsFactory.SurroundingTriangles(cell, value) : null;

	/// <inheritdoc/>
	protected override Func<MainWindow, Popup> PopupSelector => static window => window.SurroundingTrianglesPopup;

	/// <inheritdoc/>
	protected override Func<MainWindow, ItemSelectorPanel> PanelSelector => static window => window.SurroundingTrianglesPanel;
}
