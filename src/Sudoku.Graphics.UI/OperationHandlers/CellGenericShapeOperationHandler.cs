namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents cell generic shape operation handler.
/// </summary>
/// <typeparam name="TItem">The type of item.</typeparam>
public abstract class CellGenericShapeOperationHandler<TItem> : OperationHandler
	where TItem : Item, IItem_CellProperty
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
	public abstract Func<Absolute, TItem> ItemFactory { get; }


	/// <inheritdoc/>
	protected internal sealed override void OnMouseButtonPressed(OperationHandlerContext context)
	{
	}

	/// <inheritdoc/>
	protected internal sealed override void OnMouseButtonReleased(OperationHandlerContext context)
	{
		var cell = context.GetCell();
		var item = ItemFactory(cell);
		UpdateItems(
			context.OwnerWindow,
			items =>
			{
				var found = items.Find(cell, ItemType);
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
