namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CellCrossMarkItem"/>.
/// </summary>
/// <seealso cref="CellCrossMarkItem"/>
[OperationHandler(ItemType.Cell_Cross)]
public sealed class CellCrossOperationHandler : OperationHandler
{
	/// <inheritdoc/>
	protected internal override void OnMouseButtonPressed(OperationHandlerContext context)
	{
	}

	/// <inheritdoc/>
	protected internal override void OnMouseButtonReleased(OperationHandlerContext context)
	{
		var cell = context.GetCell();
		var item = ItemsFactory.Cross(cell);
		UpdateItems(
			context.OwnerWindow,
			items =>
			{
				var found = items.Find(cell, ItemType.Cell_Cross);
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
