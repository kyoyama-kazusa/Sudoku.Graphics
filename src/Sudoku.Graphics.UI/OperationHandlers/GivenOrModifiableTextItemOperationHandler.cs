namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that binds with <see cref="GivenOrModifiableTextItem"/>.
/// </summary>
/// <param name="_isGiven">Indicates whether the current handler is for given text items.</param>
/// <seealso cref="GivenOrModifiableTextItem"/>
public abstract class GivenOrModifiableTextItemOperationHandler(bool _isGiven) : OperationHandler
{
	/// <inheritdoc/>
	protected internal sealed override void OnMouseButtonPressed(OperationHandlerContext context)
	{
		var panel = context.OwnerWindow.DigitSelectorPanel;
		panel.OperationHandlerContext = context;
		panel.SelectedDigitChanged += Panel_SelectedDigitChanged;
	}

	/// <inheritdoc/>
	protected internal sealed override void OnMouseButtonReleased(OperationHandlerContext context)
	{
		if (context is not
			{
				OwnerWindow:
				{
					DigitSelectorPanel: var panel,
					DigitSelectorPopup: var popup,
					CurrentCanvas.Templates: [{ Mapper: var mapper }]
				}
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
	protected internal sealed override bool IsAvailable(OperationHandlerContext context)
		=> context.MouseEventArgs.ChangedButton == MouseButton.Right;

	private void Panel_SelectedDigitChanged(DigitSelectorPanel sender, DigitSelectorPanelSelectedDigitChangedEventArgs e)
	{
		if (e is not
			{
				Digit: var digit and not -1,
				Context:
				{
					OwnerWindow:
					{
						DigitSelectorPopup: var popup,
						DigitSelectorPanel: var panel,
						CurrentGrid: { } grid
					}
				} context
			})
		{
			return;
		}

		popup.IsOpen = false;

		var cell = context.GetCell();
		if (_isGiven)
		{
			grid.AddGiven(cell, digit);
		}
		else
		{
			grid.AddModifiable(cell, digit);
		}

		sender.SelectedDigitChanged -= Panel_SelectedDigitChanged;
		panel.OperationHandlerContext = null;
	}
}
