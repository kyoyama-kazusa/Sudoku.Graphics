namespace Sudoku.Graphics.UI.OperationHandlers.ItemSelectorPanelBased;

/// <summary>
/// Represents an operation handler that produces <see cref="CellBorderAlignedTriangleMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellBorderAlignedTriangleMarkItem"/>
[OperationHandler(ItemType.Cell_BorderAlignedTriangle)]
public sealed class CellBorderAlignedTriangleOperationHandler : CellBasedItemSelectorPanelOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_BorderAlignedTriangle;

	/// <inheritdoc/>
	protected override DuplicateLevel ItemDuplicateLevel => DuplicateLevel.Item;

	/// <inheritdoc/>
	protected override ReadOnlySpan<Func<IIconDisplayItem>> IconsFactory
		=> (Func<IIconDisplayItem>[])[
			static () => new BorderAlignedTriangleDisplayItem { Direction = Direction4.Up },
			static () => new BorderAlignedTriangleDisplayItem { Direction = Direction4.Down },
			static () => new BorderAlignedTriangleDisplayItem { Direction = Direction4.Left },
			static () => new BorderAlignedTriangleDisplayItem { Direction = Direction4.Right }
		];

	/// <inheritdoc/>
	protected override Func<object?, Absolute, Item?> ItemFactory
		=> static (item, cell) =>
			item is BorderAlignedTriangleDisplayItem { Direction: var direction }
				? ItemsFactory.CellBorderAlignedTriangle(cell, direction)
				: null;

	/// <inheritdoc/>
	protected override Func<MainWindow, ItemSelectorPanel> PanelSelector => static window => window.CellBorderAlignedTrianglePanel;

	/// <inheritdoc/>
	protected override Func<MainWindow, Popup> PopupSelector => static window => window.CellBorderAlignedTrianglePopup;
}
