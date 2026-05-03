namespace Sudoku.Graphics.UI.OperationHandlers.ItemSelectorPanelBased;

/// <summary>
/// Represents an operation handler that produces <see cref="CellArrowTriangleMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellArrowTriangleMarkItem"/>
[OperationHandler(ItemType.Cell_ArrowTriangle)]
public sealed class CellArrowTriangleOperationHandler : CellBasedItemSelectorPanelOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_ArrowTriangle;

	/// <inheritdoc/>
	protected override ReadOnlySpan<Func<IIconDisplayItem>> IconsFactory
		=> (Func<IIconDisplayItem>[])[
			static () => new CellDirectionBasedDisplayItem { Direction = Direction8.Up },
			static () => new CellDirectionBasedDisplayItem { Direction = Direction8.Down },
			static () => new CellDirectionBasedDisplayItem { Direction = Direction8.Left },
			static () => new CellDirectionBasedDisplayItem { Direction = Direction8.Right },
			static () => new CellDirectionBasedDisplayItem { Direction = Direction8.LeftUp },
			static () => new CellDirectionBasedDisplayItem { Direction = Direction8.RightUp },
			static () => new CellDirectionBasedDisplayItem { Direction = Direction8.LeftDown },
			static () => new CellDirectionBasedDisplayItem { Direction = Direction8.RightDown }
		];

	/// <inheritdoc/>
	protected override Func<object?, Absolute, Item?> ItemFactory
		=> static (item, cell) => item is CellDirectionBasedDisplayItem { Direction: var direction } ? ItemsFactory.CellArrowTriangle(cell, direction) : null;

	/// <inheritdoc/>
	protected override Func<MainWindow, ItemSelectorPanel> PanelSelector => static window => window.CellArrowTrianglePanel;

	/// <inheritdoc/>
	protected override Func<MainWindow, Popup> PopupSelector => static window => window.CellArrowTrianglePopup;
}
