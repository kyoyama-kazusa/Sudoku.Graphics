namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler type that creates for <see cref="CellTetrisMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellTetrisMarkItem"/>
[OperationHandler(ItemType.Cell_Tetris)]
public sealed class TetrisOperationHandler : OperationHandler
{
	/// <inheritdoc/>
	protected internal override void OnMouseButtonPressed(OperationHandlerContext context)
	{
		var panel = context.OwnerWindow.TetrisSelectorPanel;
		panel.OperationHandlerContext = context;
		panel.SelectedItemChanged += Panel_SelectedItemChanged; ;
	}

	/// <inheritdoc/>
	protected internal override void OnMouseButtonReleased(OperationHandlerContext context)
	{
		if (context.OwnerWindow is { TetrisSelectorPopup: var popup })
		{
			popup.IsOpen = true;
		}
	}

	/// <inheritdoc/>
	protected internal override bool IsAvailable(OperationHandlerContext context)
		=> context.MouseEventArgs.ChangedButton == MouseButton.Right;

	private void Panel_SelectedItemChanged(ItemSelectorPanel sender, ItemSelectorPanelSelectedItemChangedEventArgs e)
	{
		if (e is not
			{
				SelectedItem: var selectedItem and (TetrominoDisplayItem or null),
				Context: { OwnerWindow: { TetrisSelectorPopup: var popup, TetrisSelectorPanel: var panel } window } context
			})
		{
			return;
		}

		popup.IsOpen = false;

		var cell = context.GetCell();
		var item = selectedItem switch
		{
			TetrominoDisplayItem { Type: var piece, RotationType: var rotationType } => new CellTetrisMarkItem
			{
				TemplateIndex = 0,
				Cell = cell,
				Piece = piece,
				CornerRadiusScale = 0.25M,
				StrokeWidthScale = ResolveProperty(() => App.UserPreferences.Template_ThinLineWidth),
				StrokeColor = ResolveProperty(() => App.UserPreferences.Template_ThinLineColor),
				FillColor = ResolveProperty(() => App.UserPreferences.BackgroundFillColor),
				SizeScale = 0.2M,
				RotationType = rotationType
			},
			null => null,
			_ => throw new UnreachableException()
		};
		UpdateItems(
			window,
			items =>
			{
				if (item is null)
				{
					items.Clear(cell, ItemType.Cell_Tetris);
				}
				else
				{
					items.Add(item);
				}
			}
		);

		sender.SelectedItemChanged -= Panel_SelectedItemChanged;
		panel.OperationHandlerContext = null;
	}
}
