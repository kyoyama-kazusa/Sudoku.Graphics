namespace Sudoku.Graphics.UI.OperationHandlers.ItemSelectorPanelBased;

/// <summary>
/// Represents an operation handler that produces <see cref="CellArrowMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellArrowMarkItem"/>
[OperationHandler(ItemType.Cell_Arrow)]
public sealed class CellArrowOperationHandler : CellBasedItemSelectorPanelOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_Arrow;

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
		=> static (item, cell) => item is CellDirectionBasedDisplayItem { Direction: var direction } ? ItemsFactory.CellArrow(cell, direction) : null;

	/// <inheritdoc/>
	protected override Func<MainWindow, ItemSelectorPanel> PanelSelector => static window => window.CellArrowPanel;

	/// <inheritdoc/>
	protected override Func<MainWindow, Popup> PopupSelector => static window => window.CellArrowPopup;
}
