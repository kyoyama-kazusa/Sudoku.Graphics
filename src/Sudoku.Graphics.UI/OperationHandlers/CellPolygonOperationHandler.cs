namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents cell polygon operation handler.
/// </summary>
[OperationHandler(ItemType.Cell_Polygon)]
public sealed class CellPolygonOperationHandler : CellBasedItemSelectorPanelOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_Polygon;

	/// <inheritdoc/>
	public override Func<MainWindow, ItemSelectorPanel> PanelSelector => static window => window.PolygonPanel;

	/// <inheritdoc/>
	public override Func<MainWindow, Popup> PopupSelector => static window => window.PolygonPopup;

	/// <inheritdoc/>
	public override Func<object?, Absolute, Item?> ItemFactory
		=> static (item, cell) => item switch
		{
			PolygonDisplayItem { SidesCount: var sidesCount, IsConcave: var isConcave }
				=> ItemsFactory.Polygon(cell, sidesCount, isConcave),
			_ => null
		};
}
