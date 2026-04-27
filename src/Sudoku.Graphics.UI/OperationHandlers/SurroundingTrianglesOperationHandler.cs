namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CellSurroundingTrianglesMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellSurroundingTrianglesMarkItem"/>
[OperationHandler(ItemType.Cell_SurroundingTriangles)]
public sealed class SurroundingTrianglesOperationHandler : CellBasedItemSelectorPanelOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_SurroundingTriangles;

	/// <inheritdoc/>
	public override Func<object?, Absolute, Item?> ItemFactory
		=> static (item, cell) => item switch
		{
			SurroundingTrianglesDisplayItem { Value: var value } => ItemsFactory.SurroundingTriangles(cell, value),
			_ => null
		};

	/// <inheritdoc/>
	public override Func<MainWindow, Popup> PopupSelector => static window => window.SurroundingTrianglesPopup;

	/// <inheritdoc/>
	public override Func<MainWindow, ItemSelectorPanel> PanelSelector => static window => window.SurroundingTrianglesPanel;
}
