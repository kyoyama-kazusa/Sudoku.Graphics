namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CandidateTextItem"/>.
/// </summary>
/// <seealso cref="CandidateTextItem"/>
[OperationHandler(ItemType.Text_Candidate)]
public sealed class CandidateOperationHandler : OperationHandler
{
	/// <inheritdoc/>
	protected internal override void OnMouseButtonPressed(OperationHandlerContext context)
	{
	}

	/// <inheritdoc/>
	protected internal override void OnMouseButtonReleased(OperationHandlerContext context)
	{
		if (context.OwnerWindow.CurrentGrid is { } grid)
		{
			var candidate = context.GetCandidate();
			grid.FlipCandidate(candidate);
		}
	}

	/// <inheritdoc/>
	protected internal override bool IsAvailable(OperationHandlerContext context)
		=> context is
		{
			MouseEventArgs.ChangedButton: MouseButton.Left,
			OwnerWindow:
			{
				CurrentCanvas.Mapper: { RowsCount: var rowsCount, ColumnsCount: var columnsCount },
				CurrentGrid: not null
			}
		}
		&& rowsCount == columnsCount;
}
