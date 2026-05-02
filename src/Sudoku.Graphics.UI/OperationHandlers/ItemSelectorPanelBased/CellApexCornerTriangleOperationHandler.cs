namespace Sudoku.Graphics.UI.OperationHandlers.ItemSelectorPanelBased;

/// <summary>
/// Represents an operation handler that produces <see cref="CellApexCornerTriangleMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellApexCornerTriangleMarkItem"/>
[OperationHandler(ItemType.Cell_ApexCornerTriangle)]
public sealed class CellApexCornerTriangleOperationHandler : CellBasedItemSelectorPanelOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_ApexCornerTriangle;

	/// <inheritdoc/>
	protected override ReadOnlySpan<Func<IIconDisplayItem>> IconsFactory
		=> (Func<IIconDisplayItem>[])[
			static () => new CellApexCornerTriangleDisplayItem { Alignment = Alignment.TopLeft },
			static () => new CellApexCornerTriangleDisplayItem { Alignment = Alignment.TopRight },
			static () => new CellApexCornerTriangleDisplayItem { Alignment = Alignment.BottomLeft },
			static () => new CellApexCornerTriangleDisplayItem { Alignment = Alignment.BottomRight }
		];

	/// <inheritdoc/>
	protected override DuplicateLevel ItemDuplicateLevel => DuplicateLevel.Item;

	/// <inheritdoc/>
	protected override Func<object?, Absolute, Item?> ItemFactory
		=> static (item, cell) => item is CellApexCornerTriangleDisplayItem { Alignment: var alignment } ? ItemsFactory.CellApexCornerTriangle(cell, alignment) : null;

	/// <inheritdoc/>
	protected override Func<MainWindow, ItemSelectorPanel> PanelSelector => static window => window.CellApexCornerTrianglePanel;

	/// <inheritdoc/>
	protected override Func<MainWindow, Popup> PopupSelector => static window => window.CellApexCornerTrianglePopup;
}
