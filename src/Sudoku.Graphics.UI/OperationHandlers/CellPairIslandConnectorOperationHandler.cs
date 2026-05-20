namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CellPairIslandConnectorMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellPairIslandConnectorMarkItem"/>
[OperationHandler(ItemType.CellPair_IslandConnector)]
public sealed class CellPairIslandConnectorOperationHandler : OperationHandler
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

		var popup = context.OwnerWindow.CellPairIslandConnectorPopup;
		popup.Tag = context;
		popup.Closed += Popup_Closed;

		popup.IsOpen = true;
	}

	/// <inheritdoc/>
	protected internal override bool IsAvailable(OperationHandlerContext context)
		=> context.MouseEventArgs.ChangedButton == MouseButton.Right;

	[SuppressMessage("Style", "IDE0220:Add explicit cast", Justification = "<Pending>")]
	private void Popup_Closed(object? sender, EventArgs e)
	{
		if (sender is not Popup
			{
				Tag: OperationHandlerContext
				{
					OwnerWindow:
					{
						CurrentIslandConnectorMode: var mode,
						IslandCorner1Direction: var direction1,
						IslandCorner2Direction: var direction2,
						CellPairIslandConnectorOffsetInputBox.Value: var offset,
						CurrentCanvas.Mapper: var mapper
					} window
				} context
			} popup)
		{
			return;
		}

		var connector = mode switch
		{
			IslandConnectorMode.Direct => new DirectIslandConnector(),
			IslandConnectorMode.SingleCorner => new SingleCornerIslandConnector { ConnectedDirection = direction1 },
			IslandConnectorMode.DoubleCorners => new DoubleCornerIslandConnector
			{
				StartConnectedDirection = direction1,
				EndConnectedDirection = direction2,
				Offset = offset
			},
			_ => default(IslandConnector)
		};
		if (connector is null)
		{
			goto ResetValues;
		}

		var item = ItemsFactory.CellPairIslandConnector(_startCell, _endCell, connector);
		UpdateItems(
			window,
			items =>
			{
				var found = items.Find(findCondition);
				var foundMatched = false;
				foreach (CellPairIslandConnectorMarkItem i in found)
				{
					foundMatched = true;
					items.Remove(i);
				}
				if (!foundMatched)
				{
					items.Add(item);
				}


				bool findCondition(Item i)
					=> i is CellPairIslandConnectorMarkItem { Cell1: var c1, Cell2: var c2 } && c1 == _startCell && c2 == _endCell;
			}
		);

	ResetValues:
		// Clear context.
		_startCell = -1;
		_endCell = -1;
		popup.Closed -= Popup_Closed;
		popup.Tag = null;
	}
}
