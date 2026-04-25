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
						FontName = ResolveProperty(() => App.UserPreferences.GivenFontName),
						FontSizeScale = ResolveProperty(() => App.UserPreferences.GivenFontSizeScale),
						Text = digit.ToString(),
						Color = ResolveProperty(() => App.UserPreferences.GivenTextColor),
						FontWidth = ResolveProperty(() => App.UserPreferences.GivenFontWidth),
						FontSlant = ResolveProperty(() => App.UserPreferences.GivenFontSlant),
						FontWeight = ResolveProperty(() => App.UserPreferences.GivenFontWeight)
					}
					: new ModifiableTextItem
					{
						TemplateIndex = 0,
						Cell = context.GetCell(),
						FontName = ResolveProperty(() => App.UserPreferences.ModifiableFontName),
						FontSizeScale = ResolveProperty(() => App.UserPreferences.ModifiableFontSizeScale),
						Text = digit.ToString(),
						Color = ResolveProperty(() => App.UserPreferences.ModifiableTextColor),
						FontWidth = ResolveProperty(() => App.UserPreferences.ModifiableFontWidth),
						FontSlant = ResolveProperty(() => App.UserPreferences.ModifiableFontSlant),
						FontWeight = ResolveProperty(() => App.UserPreferences.ModifiableFontWeight)
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
