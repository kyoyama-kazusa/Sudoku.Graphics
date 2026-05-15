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

		var (cell, direction) = context.GetBorderWithDirection();
		if (cell == -1 || direction == Direction4.None)
		{
			return;
		}

		var itemType = _isThick ? ItemType.LineSegment_Thick : ItemType.LineSegment_Thin;
		var itemCreated = _isThick
			? (LineSegmentItem)ItemsFactory.ThickLineSegment(cell, direction)
			: ItemsFactory.ThinLineSegment(cell, direction);

		UpdateItems(
			window,
			items =>
			{
				var found = items.Find(
					item =>
					{
						return itemType == ItemType.LineSegment_Thick
							&& item is ThickLineSegmentItem { Cell: var c1, Direction: var d1 }
							&& areSame(c1, d1, cell, direction)
							|| itemType == ItemType.LineSegment_Thin
							&& item is ThinLineSegmentItem { Cell: var c2, Direction: var d2 }
							&& areSame(c2, d2, cell, direction);


						bool areSame(Absolute cell1, Direction4 direction1, Absolute cell2, Direction4 direction2)
							=> EdgeKey.Create(cell1, direction1, mapper) == EdgeKey.Create(cell2, direction2, mapper);
					}
				);
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
