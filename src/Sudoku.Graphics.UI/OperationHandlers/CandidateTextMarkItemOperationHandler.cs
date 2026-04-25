namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CandidateTextItem"/>.
/// </summary>
/// <seealso cref="CandidateTextItem"/>
[OperationHandler(ItemType.Text_Candidate)]
public sealed class CandidateTextMarkItemOperationHandler : OperationHandler
{
	/// <inheritdoc/>
	protected internal override void OnMouseButtonPressed(OperationHandlerContext context)
	{
		var panel = context.OwnerWindow.MultipleDigitSelectorPanel;
		panel.OperationHandlerContext = context;
		panel.SelectedDigitsChanged += Panel_SelectedDigitsChanged;
	}

	/// <inheritdoc/>
	protected internal override void OnMouseButtonReleased(OperationHandlerContext context)
	{
		if (context is not
			{
				OwnerWindow:
				{
					MultipleDigitSelectorPanel: var panel,
					MultipleDigitSelectorPopup: var popup,
					CurrentCanvas.Templates: [{ Mapper: var mapper }]
				},
			})
		{
			return;
		}

		var desiredCandidateSize = mapper.RowsCount.GetCandidatesCountInEachRow();
		panel.RowsCount = panel.ColumnsCount = desiredCandidateSize;
		panel.MaxDigit = mapper.RowsCount;
		popup.IsOpen = true;
	}

	/// <inheritdoc/>
	protected internal override bool IsAvailable(OperationHandlerContext context)
		=> context is
		{
			MouseEventArgs.ChangedButton: MouseButton.Right,
			OwnerWindow.CurrentCanvas.Templates: [{ Mapper: { RowsCount: var rowsCount, ColumnsCount: var columnsCount } }]
		}
		&& rowsCount == columnsCount;

	private void Panel_SelectedDigitsChanged(MultipleDigitSelectorPanel sender, MultipleDigitSelectorPanelSelectedDigitsChangedEventArgs e)
	{
		if (e is not
			{
				Digits: { } digits,
				Context:
				{
					OwnerWindow:
					{
						MultipleDigitSelectorPopup: var popup,
						MultipleDigitSelectorPanel: var panel,
						CurrentCanvas: { Surface: var surface } canvas,
						CurrentGrid: { } grid
					} window,
					Items: var items
				} context
			})
		{
			return;
		}

		popup.IsOpen = false;

		grid.AddCandidates(context.GetCell(), digits);

		sender.SelectedDigitsChanged -= Panel_SelectedDigitsChanged;
		panel.OperationHandlerContext = null;
		panel.SelectedDigits = []; // It may trigger 'Panel_SelectedDigitsChanged' but we have just already unsubscribed this event.
	}
}
