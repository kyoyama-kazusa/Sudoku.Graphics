namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents a cell pair shape operation handler.
/// </summary>
public abstract class CellPairGenericShapeOperationHandler : OperationHandler
{
	/// <summary>
	/// Indicates the supported item type.
	/// </summary>
	public abstract ItemType ItemType { get; }

	/// <summary>
	/// Indicates the changed mouse button that will trigger the event.
	/// </summary>
	public virtual MouseButton ChangedButton => MouseButton.Left;

	/// <summary>
	/// Indicates the item factory.
	/// </summary>
	public abstract Func<Absolute, Absolute, PointMapper, IItem_CellPairProperty?> ItemFactory { get; }


	/// <inheritdoc/>
	protected internal sealed override void OnMouseButtonPressed(OperationHandlerContext context)
	{
	}

	/// <inheritdoc/>
	protected internal sealed override void OnMouseButtonReleased(OperationHandlerContext context)
	{
		if (context is not { OwnerWindow: { CurrentCanvas.Mapper: var mapper } window })
		{
			return;
		}

		var (cell1, cell2) = context.GetBorder();
		if (ItemFactory(cell1, cell2, mapper) is not Item item)
		{
			return;
		}

		UpdateItems(
			window,
			items =>
			{
				var found = items.Find(cell1, cell2, ItemType);
				if (found.Length == 0)
				{
					items.Add(item);
				}
				else
				{
					items.RemoveRange(found);
				}
			}
		);
	}

	/// <inheritdoc/>
	protected internal sealed override bool IsAvailable(OperationHandlerContext context)
		=> context.MouseEventArgs.ChangedButton == ChangedButton;
}
