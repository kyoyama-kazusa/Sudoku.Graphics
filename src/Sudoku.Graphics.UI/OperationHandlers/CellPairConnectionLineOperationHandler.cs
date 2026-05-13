namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CellPairConnectionLineMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellPairConnectionLineMarkItem"/>
[OperationHandler(ItemType.CellPair_ConnectionLine)]
public sealed class CellPairConnectionLineOperationHandler : OperationHandler
{
	/// <summary>
	/// Indicates the captured start cell user clicked.
	/// </summary>
	private Absolute _startCell = -1;

	/// <summary>
	/// Indicates the captured end cell user clicked.
	/// </summary>
	private Absolute _endCell = -1;


	/// <inheritdoc/>
	public override bool UsesDifferentInstancesBetweenEvents => false;

	/// <inheritdoc/>
	public override bool DiffersMousePositionsBetweenEvents => true;


	/// <inheritdoc/>
	protected internal override void OnMouseButtonPressed(OperationHandlerContext context) => _startCell = context.GetCell();

	/// <inheritdoc/>
	protected internal override void OnMouseButtonReleased(OperationHandlerContext context)
	{
		_endCell = context.GetCell();

		if (context is not OperationHandlerContext { OwnerWindow: { CurrentCanvas.Mapper: var mapper } window })
		{
			goto ResetValues;
		}

		if (_startCell == -1 || _endCell == -1)
		{
			return;
		}

		//if (!_startCell.IsSideWith(_endCell, Direction4.Up, mapper, true)
		//	&& !_startCell.IsSideWith(_endCell, Direction4.Down, mapper, true)
		//	&& !_startCell.IsSideWith(_endCell, Direction4.Left, mapper, true)
		//	&& !_startCell.IsSideWith(_endCell, Direction4.Right, mapper, true))
		//{
		//	return;
		//}

		if (_startCell > _endCell)
		{
			(_startCell, _endCell) = (_endCell, _startCell);
		}

		var item = ItemsFactory.CellPairConnectionLine(_startCell, _endCell);
		UpdateItems(
			window,
			items =>
			{
				var found = items.Find(_startCell, _endCell, ItemType.CellPair_ConnectionLine);
				var sameItemIsFound = false;
				foreach (var item in found)
				{
					if (item is CellPairConnectionLineMarkItem { Cell1: var cell1, Cell2: var cell2 }
						&& (cell1 == _startCell && cell2 == _endCell || cell1 == _endCell && cell2 == _startCell))
					{
						sameItemIsFound = true;
					}
					items.Remove(item);
				}
				if (!sameItemIsFound)
				{
					items.Add(item);
				}
			}
		);

	ResetValues:
		_startCell = -1;
		_endCell = -1;
	}

	/// <inheritdoc/>
	protected internal override bool IsAvailable(OperationHandlerContext context)
		=> context.MouseEventArgs.ChangedButton == MouseButton.Left;
}
