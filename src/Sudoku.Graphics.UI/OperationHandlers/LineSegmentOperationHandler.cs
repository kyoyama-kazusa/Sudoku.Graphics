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
						{
							return k(cell1, direction1) == k(cell2, direction2);


							EdgeKey k(Absolute cell, Direction4 direction)
							{
								var row = cell / mapper.AbsoluteColumnsCount;
								var column = cell % mapper.AbsoluteColumnsCount;
								var (x1, y1, x2, y2) = direction switch
								{
									Direction4.Up => (column, row, column + 1, row),
									Direction4.Right => (column + 1, row, column + 1, row + 1),
									Direction4.Down => (column, row + 1, column + 1, row + 1),
									Direction4.Left => (column, row, column, row + 1),
									_ => throw new ArgumentOutOfRangeException(nameof(direction))
								};
								if (x1 > x2 || x1 == x2 && y1 > y2)
								{
									(x1, y1, x2, y2) = (x2, y2, x1, y1);
								}
								return new(x1, y1, x2, y2);
							}
						}
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

/// <summary>
/// Represents a key of line segment.
/// </summary>
/// <param name="X1">Indicates the value X1.</param>
/// <param name="Y1">Indicates the value Y1.</param>
/// <param name="X2">Indicates the value X2.</param>
/// <param name="Y2">Indicates the value Y2.</param>
file readonly record struct EdgeKey(Absolute X1, Absolute Y1, Absolute X2, Absolute Y2);
