namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces bridge line mark items.
/// </summary>
[OperationHandler(ItemType.CellPair_BridgeLine)]
public sealed class CellPairBridgeLineOperationHandler : OperationHandler
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
	public override bool UseDifferentInstancesBetweenEvents => false;

	/// <inheritdoc/>
	public override bool DiffersMousePositionsBetweenEvents => true;


	/// <inheritdoc/>
	protected internal override void OnMouseButtonPressed(OperationHandlerContext context) => _startCell = context.GetCell();

	/// <inheritdoc/>
	protected internal override void OnMouseButtonReleased(OperationHandlerContext context)
	{
		_endCell = context.GetCell();

		var popup = context.OwnerWindow.CellPairBridgeLinesCountPopup;
		popup.IsOpen = true;
		popup.Tag = context;

		popup.Closed += Popup_Closed;
	}

	/// <inheritdoc/>
	protected internal override bool IsAvailable(OperationHandlerContext context)
		=> context.MouseEventArgs.ChangedButton == MouseButton.Right;

	private void Popup_Closed(object? sender, EventArgs e)
	{
		if (sender is not Popup
			{
				Tag: OperationHandlerContext
				{
					OwnerWindow:
					{
						CurrentCanvas.Mapper: var mapper,
						CellPairBridgeLinesCountBox.Value: var linesCount and >= 1 and <= 4
					} window
				}
			} popup)
		{
			return;
		}

		if (_startCell == -1 || _endCell == -1)
		{
			return;
		}

		if (!_startCell.IsSideWith(_endCell, Direction4.Up, mapper, true)
			&& !_startCell.IsSideWith(_endCell, Direction4.Down, mapper, true)
			&& !_startCell.IsSideWith(_endCell, Direction4.Left, mapper, true)
			&& !_startCell.IsSideWith(_endCell, Direction4.Right, mapper, true))
		{
			return;
		}

		popup.IsOpen = false;

		var item = ItemsFactory.CellPairBridgeLine(_startCell, _endCell, linesCount);
		UpdateItems(
			window,
			items =>
			{
				var found = items.Find(_startCell, _endCell, ItemType.CellPair_BridgeLine);
				var sameItemIsFound = false;
				foreach (var item in found)
				{
					if (item is CellPairBridgeLineMarkItem { Cell1: var cell1, Cell2: var cell2, LinesCount: var tempLinesCount }
						&& (cell1 == _startCell && cell2 == _endCell || cell1 == _endCell && cell2 == _startCell)
						&& tempLinesCount == linesCount)
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

		popup.Closed -= Popup_Closed;
		popup.Tag = null;
		_startCell = -1;
		_endCell = -1;
	}
}
