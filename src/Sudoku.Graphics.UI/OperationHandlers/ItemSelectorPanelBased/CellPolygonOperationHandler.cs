namespace Sudoku.Graphics.UI.OperationHandlers.ItemSelectorPanelBased;

/// <summary>
/// Represents cell polygon operation handler.
/// </summary>
[OperationHandler(ItemType.Cell_Polygon)]
public sealed class CellPolygonOperationHandler : CellBasedItemSelectorPanelOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_Polygon;

	/// <inheritdoc/>
	protected override ReadOnlySpan<Func<IIconDisplayItem>> IconsFactory
		=> (Func<IIconDisplayItem>[])[
			static () => new PolygonDisplayItem { SidesCount = 3, IsConcave = false },
			static () => new PolygonDisplayItem { SidesCount = 4, IsConcave = false },
			static () => new PolygonDisplayItem { SidesCount = 5, IsConcave = false },
			static () => new PolygonDisplayItem { SidesCount = 6, IsConcave = false },
			static () => new PolygonDisplayItem { SidesCount = 7, IsConcave = false },
			static () => new PolygonDisplayItem { SidesCount = 8, IsConcave = false },
			static () => new PolygonDisplayItem { SidesCount = 3, IsConcave = true },
			static () => new PolygonDisplayItem { SidesCount = 4, IsConcave = true },
			static () => new PolygonDisplayItem { SidesCount = 5, IsConcave = true },
			static () => new PolygonDisplayItem { SidesCount = 6, IsConcave = true },
			static () => new PolygonDisplayItem { SidesCount = 7, IsConcave = true },
			static () => new PolygonDisplayItem { SidesCount = 8, IsConcave = true }
		];

	/// <inheritdoc/>
	protected override Func<object?, Absolute, Item?> ItemFactory
		=> static (item, cell) =>
			item is PolygonDisplayItem { SidesCount: var sidesCount, IsConcave: var isConcave }
				? ItemsFactory.Polygon(cell, sidesCount, isConcave)
				: null;

	/// <inheritdoc/>
	protected override Func<MainWindow, ItemSelectorPanel> PanelSelector => static window => window.PolygonPanel;

	/// <inheritdoc/>
	protected override Func<MainWindow, Popup> PopupSelector => static window => window.PolygonPopup;
}
