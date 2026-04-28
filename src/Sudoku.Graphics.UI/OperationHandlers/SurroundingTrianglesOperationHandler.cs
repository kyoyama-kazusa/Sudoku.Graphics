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

	/// <inheritdoc/>
	public override IReadOnlyDictionary<string, Func<IIconDisplayItem>> IconDisplayItemFactory
		=> new Dictionary<string, Func<IIconDisplayItem>>
		{
			{ "SurroundingTriangles_1", static () => new SurroundingTrianglesDisplayItem { Value = 1 } },
			{ "SurroundingTriangles_2", static () => new SurroundingTrianglesDisplayItem { Value = 2 } },
			{ "SurroundingTriangles_3", static () => new SurroundingTrianglesDisplayItem { Value = 3 } },
			{ "SurroundingTriangles_4", static () => new SurroundingTrianglesDisplayItem { Value = 4 } },
			{ "SurroundingTriangles_5", static () => new SurroundingTrianglesDisplayItem { Value = 5 } },
			{ "SurroundingTriangles_6", static () => new SurroundingTrianglesDisplayItem { Value = 6 } }
		};
}
