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
				OwnerWindow: { DigitSelectorPanel: var panel, DigitSelectorPopup: var popup },
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
	protected internal sealed override bool IsAvailable(OperationHandlerContext context)
		=> context.MouseEventArgs.ChangedButton == MouseButton.Right;

	/// <inheritdoc/>
	protected internal sealed override ReadOnlySpan<Item> CreateItem(OperationHandlerContext context)
		=> context switch
		{
			{ State: int digit } => (Item[])[
				_isGiven
					? new GivenTextItem
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
					}
					: new ModifiableTextItem
					{
						TemplateIndex = 0,
						Cell = context.GetCell(),
						FontName = R(() => App.UserPreferences.ModifiableFontName),
						FontSizeScale = R(() => App.UserPreferences.ModifiableFontSizeScale),
						Text = digit.ToString(),
						Color = R(() => App.UserPreferences.ModifiableTextColor),
						FontWidth = R(() => App.UserPreferences.ModifiableFontWidth),
						FontSlant = R(() => App.UserPreferences.ModifiableFontSlant),
						FontWeight = R(() => App.UserPreferences.ModifiableFontWeight)
					}
			],
			_ => []
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
		if (CreateItem(context) is not { IsEmpty: false } itemsCreated)
		{
			return;
		}

		items.AddRange(itemsCreated);
		canvas.DrawItems(items);

		using var image = surface.Snapshot();
		window.GridImageSource = image.ToWriteableBitmap();

		sender.SelectedDigitChanged -= Panel_SelectedDigitChanged;
		panel.OperationHandlerContext = null;
	}
}
