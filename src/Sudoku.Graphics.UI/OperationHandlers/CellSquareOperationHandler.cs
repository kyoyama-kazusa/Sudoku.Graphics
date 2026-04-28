namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CellSquareMarkItem"/>.
/// </summary>
/// <seealso cref="CellSquareMarkItem"/>
[OperationHandler(ItemType.Cell_Square)]
public sealed class CellSquareOperationHandler : OperationHandler
{
	/// <inheritdoc/>
	protected internal override void OnMouseButtonPressed(OperationHandlerContext context)
	{
	}

	/// <inheritdoc/>
	protected internal override void OnMouseButtonReleased(OperationHandlerContext context)
	{
		var cell = context.GetCell();
		var item = ItemsFactory.Square(cell);
		UpdateItems(
			context.OwnerWindow,
			items =>
			{
				var found = items.Find(cell, ItemType.Cell_Square);
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
