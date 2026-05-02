namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents a cell or candidate shape operation handler.
/// </summary>
/// <param name="_isCellBased">Indicates whether the handler is cell-based or not.</param>
public abstract class LocatorGenericShapeOperationHandler(bool _isCellBased) : OperationHandler
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
	protected abstract Func<Locator, Item> ItemFactoryBase { get; }


	/// <inheritdoc/>
	protected internal sealed override void OnMouseButtonPressed(OperationHandlerContext context)
	{
	}

	/// <inheritdoc/>
	protected internal sealed override void OnMouseButtonReleased(OperationHandlerContext context)
	{
		var locator = (Locator)(_isCellBased ? context.GetCell() : context.GetCandidate());
		if (ItemFactoryBase(locator) is not Item item)
		{
			return;
		}

		UpdateItems(
			context.OwnerWindow,
			items =>
			{
				var found = items.Find(locator, ItemType);
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
