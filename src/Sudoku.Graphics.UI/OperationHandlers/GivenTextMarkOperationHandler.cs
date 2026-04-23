namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Provides an operation handler that produces <see cref="GivenTextItem"/> instances.
/// </summary>
/// <seealso cref="GivenTextItem"/>
[OperationHandler(ItemType.Text_Given)]
public sealed class GivenTextMarkOperationHandler : OperationHandler
{
	/// <inheritdoc/>
	protected internal override void OnMouseButtonPressed(OperationHandlerContext context)
	{
		if (context is not { OwnerWindow.DigitSelectorPanel: var panel })
		{
			return;
		}

		panel.OperationHandlerContext = context;
		panel.SelectedDigitChanged += Panel_SelectedDigitChanged;
	}

	/// <inheritdoc/>
	protected internal override void OnMouseButtonReleased(OperationHandlerContext context)
	{
		if (context is not
			{
				OwnerWindow: { MainGrid: { Source: var source } mainGrid, DigitSelectorPanel: var panel, DigitSelectorPopup: var popup },
				MouseEventArgs.ChangedButton: MouseButton.Right,
				Canvas.Templates: [{ Mapper: var mapper }]
			})
		{
			return;
		}

		var desiredCandidateSize = (double)(int)mapper.RowsCount >> Math.Sqrt >> Math.Ceiling >> Convert.ToInt32;
		panel.RowsCount = panel.ColumnsCount = desiredCandidateSize;
		panel.MaxDigit = mapper.RowsCount;
		popup.IsOpen = true;
	}

	/// <inheritdoc/>
	protected internal override Item? CreateItem(OperationHandlerContext context)
		=> context switch
		{
			{ State: int digit } => new GivenTextItem
			{
				TemplateIndex = 0,
				Cell = context.GetCell(),
				FontName = R(() => App.UserPreferences.GivenFontName),
				FontSizeScale = R(() => App.UserPreferences.GivenFontSizeScale),
				Text = digit.ToString(),
				Color = R(() => App.UserPreferences.GivenTextColor),
				FontWidth = R(() => App.UserPreferences.GivenFontWidth),
				FontSlant = R(() => App.UserPreferences.GivenFontSlant),
				FontWeight = R(() => App.UserPreferences.GivenFontWeight)
			},
			_ => null
		};

	private void Panel_SelectedDigitChanged(DigitSelectorPanel sender, DigitSelectorPanelSelectedDigitChangedEventArgs e)
	{
		if (e is not
			{
				Digit: var digit and not 0,
				Context:
				{
					OwnerWindow: { DigitSelectorPopup: var popup, DigitSelectorPanel: var panel } window,
					Canvas: { Surface: var surface } canvas,
					Items: var items
				} context
			})
		{
			return;
		}

		popup.IsOpen = false;

		context.State = digit;
		if (CreateItem(context) is not { } item)
		{
			return;
		}

		items.Add(item);
		canvas.DrawItems(items);

		using var image = surface.Snapshot();
		window.GridImageSource = image.ToWriteableBitmap();

		sender.SelectedDigitChanged -= Panel_SelectedDigitChanged;
		panel.OperationHandlerContext = null;
	}
}
