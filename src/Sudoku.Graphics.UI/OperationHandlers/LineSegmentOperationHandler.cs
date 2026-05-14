namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces line segment items.
/// </summary>
/// <param name="_isThick">Indicates whether the line is thick line.</param>
public abstract class LineSegmentOperationHandler(bool _isThick) : OperationHandler
{
	/// <inheritdoc/>
	public sealed override bool DiffersMousePositionsBetweenEvents => base.DiffersMousePositionsBetweenEvents;

	/// <inheritdoc/>
	public sealed override bool UsesDifferentInstancesBetweenEvents => base.UsesDifferentInstancesBetweenEvents;


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
		if (cell1 == -1 || cell2 == -1)
		{
			return;
		}

		if (cell1 > cell2)
		{
			(cell1, cell2) = (cell2, cell1);
		}

		var itemType = _isThick ? ItemType.LineSegment_Thick : ItemType.LineSegment_Thin;
		var itemCreated = _isThick
			? (LineSegmentItem)ItemsFactory.ThickLineSegment(cell1, cell2)
			: ItemsFactory.ThinLineSegment(cell1, cell2);

		UpdateItems(
			window,
			items =>
			{
				var found = items.Find(cell1, cell2, itemType);
				if (found.Length == 0)
				{
					items.Add(itemCreated);
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
		=> context.MouseEventArgs.ChangedButton == MouseButton.Left;
}
