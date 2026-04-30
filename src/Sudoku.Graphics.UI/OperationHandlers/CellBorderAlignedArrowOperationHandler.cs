namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CellBorderAlignedArrowMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellBorderAlignedArrowMarkItem"/>
[OperationHandler(ItemType.Cell_BorderAlignedArrow)]
public sealed class CellBorderAlignedArrowOperationHandler : CellBasedItemSelectorPanelOperationHandler
{
	/// <inheritdoc/>
	public override ItemType ItemType => ItemType.Cell_BorderAlignedArrow;

	/// <inheritdoc/>
	protected override DuplicateLevel ItemDuplicateLevel => DuplicateLevel.Item;

	/// <inheritdoc/>
	protected override ReadOnlySpan<Func<IIconDisplayItem>> IconsFactory
		=> (Func<IIconDisplayItem>[])[
			static () => new BorderAlignedArrowDisplayItem { Direction = Direction4.Up, RotationDirection = RotationDirection.Clockwise },
			static () => new BorderAlignedArrowDisplayItem { Direction = Direction4.Down, RotationDirection = RotationDirection.Clockwise },
			static () => new BorderAlignedArrowDisplayItem { Direction = Direction4.Left, RotationDirection = RotationDirection.Clockwise },
			static () => new BorderAlignedArrowDisplayItem { Direction = Direction4.Right, RotationDirection = RotationDirection.Clockwise },
			static () => new BorderAlignedArrowDisplayItem { Direction = Direction4.Up, RotationDirection = RotationDirection.Counterclockwise },
			static () => new BorderAlignedArrowDisplayItem { Direction = Direction4.Down, RotationDirection = RotationDirection.Counterclockwise },
			static () => new BorderAlignedArrowDisplayItem { Direction = Direction4.Left, RotationDirection = RotationDirection.Counterclockwise },
			static () => new BorderAlignedArrowDisplayItem { Direction = Direction4.Right, RotationDirection = RotationDirection.Counterclockwise }
		];

	/// <inheritdoc/>
	protected override Func<object?, Absolute, Item?> ItemFactory
		=> static (item, cell) =>
			item is BorderAlignedArrowDisplayItem { Direction: var direction, RotationDirection: var rotationDirection }
				? ItemsFactory.CellBorderAlignedArrow(cell, direction, rotationDirection)
				: null;

	/// <inheritdoc/>
	protected override Func<MainWindow, ItemSelectorPanel> PanelSelector => static window => window.CellBorderAlignedArrowPanel;

	/// <inheritdoc/>
	protected override Func<MainWindow, Popup> PopupSelector => static window => window.CellBorderAlignedArrowPopup;
}
