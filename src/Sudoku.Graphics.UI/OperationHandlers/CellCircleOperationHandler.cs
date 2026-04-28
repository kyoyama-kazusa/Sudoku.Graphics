namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CellCircleMarkItem"/>.
/// </summary>
/// <seealso cref="CellCircleMarkItem"/>
[OperationHandler(ItemType.Cell_Circle)]
public sealed class CellCircleOperationHandler : OperationHandler
{
	/// <inheritdoc/>
	protected internal override void OnMouseButtonPressed(OperationHandlerContext context)
	{
	}

	/// <inheritdoc/>
	protected internal override void OnMouseButtonReleased(OperationHandlerContext context)
	{
		var cell = context.GetCell();
		var item = ItemsFactory.Circle(cell);
		UpdateItems(
			context.OwnerWindow,
			items =>
			{
				var found = items.Find(cell, ItemType.Cell_Circle);
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
	protected internal override bool IsAvailable(OperationHandlerContext context)
		=> context.MouseEventArgs.ChangedButton == MouseButton.Left;
}
