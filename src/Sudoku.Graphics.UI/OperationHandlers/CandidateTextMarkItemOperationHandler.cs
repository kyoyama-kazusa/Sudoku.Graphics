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
				OwnerWindow: { MultipleDigitSelectorPanel: var panel, MultipleDigitSelectorPopup: var popup },
				Canvas.Templates: [{ Mapper: var mapper }]
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
			Canvas.Templates: [{ Mapper: { RowsCount: var rowsCount, ColumnsCount: var columnsCount } }]
		}
		&& rowsCount == columnsCount;

	/// <inheritdoc/>
	protected internal override ReadOnlySpan<Item> CreateItem(OperationHandlerContext context)
	{
		if (context is not
			{
				State: int[] digits,
				Canvas.Templates: [{ Mapper: { RowsCount: var rowsCount, ColumnsCount: var columnsCount } }]
			})
		{
			return [];
		}

		if (rowsCount != columnsCount)
		{
			return [];
		}

		var cell = context.GetCell();
		var subgridSize = rowsCount.GetCandidatesCountInEachRow();
		return
			from digit in digits
			select new CandidateTextItem
			{
				TemplateIndex = 0,
				CandidatePosition = new(cell, subgridSize, digit - 1),
				FontName = R(() => App.UserPreferences.GivenFontName),
				FontSizeScale = R(() => App.UserPreferences.GivenFontSizeScale),
				Text = digit.ToString(),
				Color = R(() => App.UserPreferences.GivenTextColor),
				FontWidth = R(() => App.UserPreferences.GivenFontWidth),
				FontSlant = R(() => App.UserPreferences.GivenFontSlant),
				FontWeight = R(() => App.UserPreferences.GivenFontWeight)
			};
	}

	private void Panel_SelectedDigitsChanged(MultipleDigitSelectorPanel sender, MultipleDigitSelectorPanelSelectedDigitsChangedEventArgs e)
	{
		if (e is not
			{
				Digits: { } digits,
				Context:
				{
					OwnerWindow: { MultipleDigitSelectorPopup: var popup, MultipleDigitSelectorPanel: var panel } window,
					Canvas: { Surface: var surface } canvas,
					Items: var items
				} context
			})
		{
			return;
		}

		popup.IsOpen = false;

		context.State = digits;
		if (CreateItem(context) is not { IsEmpty: false } itemsCreated)
		{
			return;
		}

		items.AddRange(itemsCreated);
		canvas.DrawItems(items);

		using var image = surface.Snapshot();
		window.GridImageSource = image.ToWriteableBitmap();

		sender.SelectedDigitsChanged -= Panel_SelectedDigitsChanged;
		panel.OperationHandlerContext = null;
	}
}
