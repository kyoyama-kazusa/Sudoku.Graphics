namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CellTriangleMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellTriangleMarkItem"/>
[OperationHandler(ItemType.Cell_Triangle)]
public sealed class CellTriangleOperationHandler : CellBasedItemSelectorPanelOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_Triangle;

	/// <inheritdoc/>
	protected override ReadOnlySpan<Func<IIconDisplayItem>> IconsFactory
		=> (Func<IIconDisplayItem>[])[
			static () => new CellTriagleDisplayItem { Direction = Direction8.Up },
			static () => new CellTriagleDisplayItem { Direction = Direction8.Down },
			static () => new CellTriagleDisplayItem { Direction = Direction8.Left },
			static () => new CellTriagleDisplayItem { Direction = Direction8.Right },
			static () => new CellTriagleDisplayItem { Direction = Direction8.LeftUp },
			static () => new CellTriagleDisplayItem { Direction = Direction8.RightUp },
			static () => new CellTriagleDisplayItem { Direction = Direction8.LeftDown },
			static () => new CellTriagleDisplayItem { Direction = Direction8.RightDown }
		];

	/// <inheritdoc/>
	protected override Func<object?, Absolute, Item?> ItemFactory
		=> static (item, cell) => item is CellTriagleDisplayItem { Direction: var direction } ? ItemsFactory.CellTriangle(cell, direction) : null;

	/// <inheritdoc/>
	protected override Func<MainWindow, ItemSelectorPanel> PanelSelector => static window => window.CellTrianglePanel;

	/// <inheritdoc/>
	protected override Func<MainWindow, Popup> PopupSelector => static window => window.CellTrianglePopup;
}
