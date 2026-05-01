namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces candidate shape items.
/// </summary>
public abstract class CandidateShapeOperationHandler : OperationHandler
{
	/// <summary>
	/// Indicates the supported item type.
	/// </summary>
	public abstract ItemType ItemType { get; }

	/// <summary>
	/// Indicates the item factory.
	/// </summary>
	public abstract Func<CandidatePosition, CandidateMarkItem> ItemFactory { get; }

	/// <summary>
	/// Indicates the changed mouse button that will trigger the event.
	/// </summary>
	public virtual MouseButton ChangedButton => MouseButton.Left;


	/// <inheritdoc/>
	protected internal sealed override void OnMouseButtonPressed(OperationHandlerContext context)
	{
	}

	/// <inheritdoc/>
	protected internal sealed override void OnMouseButtonReleased(OperationHandlerContext context)
	{
		var candidate = context.GetCandidate();
		var item = ItemFactory(candidate);
		UpdateItems(
			context.OwnerWindow,
			items =>
			{
				var found = items.Find(candidate, ItemType);
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
