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
				=> ItemsFactory.Polygon(cell, sidesCount, isConcave, true),
			_ => null
		};

	/// <inheritdoc/>
	public override IReadOnlyDictionary<string, Func<IIconDisplayItem>> IconDisplayItemFactory
		=> new Dictionary<string, Func<IIconDisplayItem>>
		{
			{ "Polygon_3_Convex", static () => new PolygonDisplayItem { SidesCount = 3, IsConcave = false } },
			{ "Polygon_4_Convex", static () => new PolygonDisplayItem { SidesCount = 4, IsConcave = false } },
			{ "Polygon_5_Convex", static () => new PolygonDisplayItem { SidesCount = 5, IsConcave = false } },
			{ "Polygon_6_Convex", static () => new PolygonDisplayItem { SidesCount = 6, IsConcave = false } },
			{ "Polygon_7_Convex", static () => new PolygonDisplayItem { SidesCount = 7, IsConcave = false } },
			{ "Polygon_8_Convex", static () => new PolygonDisplayItem { SidesCount = 8, IsConcave = false } },
			{ "Polygon_3_Concave", static () => new PolygonDisplayItem { SidesCount = 3, IsConcave = true } },
			{ "Polygon_4_Concave", static () => new PolygonDisplayItem { SidesCount = 4, IsConcave = true } },
			{ "Polygon_5_Concave", static () => new PolygonDisplayItem { SidesCount = 5, IsConcave = true } },
			{ "Polygon_6_Concave", static () => new PolygonDisplayItem { SidesCount = 6, IsConcave = true } },
			{ "Polygon_7_Concave", static () => new PolygonDisplayItem { SidesCount = 7, IsConcave = true } },
			{ "Polygon_8_Concave", static () => new PolygonDisplayItem { SidesCount = 8, IsConcave = true } }
		};
}
