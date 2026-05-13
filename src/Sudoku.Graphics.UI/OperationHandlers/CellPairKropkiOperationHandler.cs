namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents a cell pair kropki operation handler.
/// </summary>
public abstract class CellPairKropkiOperationHandler : OperationHandler
{
	/// <summary>
	/// Indicates the item type supported.
	/// </summary>
	public abstract ItemType ItemType { get; }

	/// <summary>
	/// Indicates the item factory.
	/// </summary>
	public abstract Func<Absolute, Absolute, bool, Item> ItemFactory { get; }


	/// <inheritdoc/>
	protected internal sealed override void OnMouseButtonPressed(OperationHandlerContext context)
	{
	}

	/// <inheritdoc/>
	protected internal sealed override void OnMouseButtonReleased(OperationHandlerContext context)
	{
		if (context is not { OwnerWindow: { CurrentCanvas.Mapper: var mapper } window, MouseEventArgs.ChangedButton: var button })
		{
			return;
		}

		var (cell1, cell2) = context.GetBorderOrCorner();
		if (cell1 > cell2)
		{
			(cell1, cell2) = (cell2, cell1);
		}
		if (!cell1.IsSideWith(cell2, Direction4.Up, mapper, true)
			&& !cell1.IsSideWith(cell2, Direction4.Down, mapper, true)
			&& !cell1.IsSideWith(cell2, Direction4.Left, mapper, true)
			&& !cell1.IsSideWith(cell2, Direction4.Right, mapper, true)
			&& !cell1.IsOrientationWith(cell2, Orientation4.Slash, mapper)
			&& !cell1.IsOrientationWith(cell2, Orientation4.Backslash, mapper))
		{
			return;
		}

		if (ItemFactory(cell1, cell2, button == MouseButton.Left) is not Item item)
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
	protected internal sealed override bool IsAvailable(OperationHandlerContext context) => base.IsAvailable(context);
}
