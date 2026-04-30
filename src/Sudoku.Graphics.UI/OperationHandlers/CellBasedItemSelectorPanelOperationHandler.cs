namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces instances that can be selected using <see cref="ItemSelectorPanel"/>.
/// </summary>
/// <seealso cref="ItemSelectorPanel"/>
public abstract partial class CellBasedItemSelectorPanelOperationHandler : OperationHandler
{
	/// <summary>
	/// Indicates the item type supported.
	/// </summary>
	public abstract ItemType ItemType { get; }

	/// <summary>
	/// Indicates the default changed button to be checked.
	/// </summary>
	public virtual MouseButton ChangedButton => MouseButton.Right;

	/// <summary>
	/// Indicates icon display item lookup, indexed by the source path.
	/// </summary>
	protected abstract ReadOnlySpan<Func<IIconDisplayItem>> IconsFactory { get; }

	/// <summary>
	/// Represents duplicate level on filtering duplicate items to remove before adding a new item.
	/// </summary>
	protected virtual DuplicateLevel ItemDuplicateLevel => DuplicateLevel.CellOnlyCurrentItemType;

	/// <summary>
	/// Represents a method that selects the target <see cref="ItemSelectorPanel"/> defined in the specified window.
	/// </summary>
	protected abstract Func<MainWindow, ItemSelectorPanel> PanelSelector { get; }

	/// <summary>
	/// Represents a method that selects the target <see cref="Popup"/> defined in the specified window.
	/// </summary>
	protected abstract Func<MainWindow, Popup> PopupSelector { get; }

	/// <summary>
	/// Represents a method that produces an item to add, or <see langword="null"/> if invalid.
	/// </summary>
	protected abstract Func<object?, Absolute, Item?> ItemFactory { get; }


	/// <inheritdoc/>
	protected internal sealed override void OnMouseButtonPressed(OperationHandlerContext context)
	{
		var panel = PanelSelector(context.OwnerWindow);
		panel.OperationHandlerContext = context;
		panel.SelectedItemChanged += Panel_SelectedItemChanged;

		LoadSamplePicture(panel);
	}

	/// <inheritdoc/>
	protected internal sealed override void OnMouseButtonReleased(OperationHandlerContext context)
		=> PopupSelector(context.OwnerWindow).IsOpen = true;

	/// <inheritdoc/>
	protected internal sealed override bool IsAvailable(OperationHandlerContext context)
		=> context.MouseEventArgs.ChangedButton == ChangedButton;

	private void Panel_SelectedItemChanged(ItemSelectorPanel sender, ItemSelectorPanelSelectedItemChangedEventArgs e)
	{
		if (e.Context is not { OwnerWindow: var window } context)
		{
			return;
		}

		var popup = PopupSelector(window);
		var panel = PanelSelector(window);
		var selectedItem = e.SelectedItem;

		popup.IsOpen = false;

		var cell = context.GetCell();
		var item = ItemFactory(selectedItem, cell);
		UpdateItems(
			window,
			items =>
			{
				if (item is null)
				{
					items.Clear(cell, ItemType);
					return;
				}

				var exists = items.Contains(item);
				switch (ItemDuplicateLevel, item)
				{
					case (DuplicateLevel.Item, _):
					{
						if (!items.Remove(item))
						{
							goto default;
						}
						break;
					}
					case (DuplicateLevel.CellAllTypes, IItem_CellProperty { Cell: var c }):
					{
						items.Clear(c);
						if (!exists)
						{
							goto default;
						}
						break;
					}
					case (DuplicateLevel.CellOnlyCurrentItemType, IItem_CellProperty { Cell: var c }):
					{
						items.Clear(c, ItemType);
						if (!exists)
						{
							goto default;
						}
						break;
					}
					default:
					{
						items.Add(item);
						break;
					}
				}
			}
		);

		sender.SelectedItemChanged -= Panel_SelectedItemChanged;
		panel.OperationHandlerContext = null;
	}

	private void LoadSamplePicture(ItemSelectorPanel panel)
	{
		var values = new List<IIconDisplayItem>();
		foreach (var instanceFactory in IconsFactory)
		{
			var instance = instanceFactory();
			using var canvas = Canvas.GetSampleCanvas(
				ResolveProperty(() => App.UserPreferences.SampleCanvasCellSize),
				ResolveProperty(() => App.UserPreferences.SampleCanvasMargin)
			);
			canvas.DrawItem(ItemFactory(instance, 0)!);

			using var image = canvas.Surface.Snapshot();
			using var data = image.Encode(SKEncodedImageFormat.Png, 100);
			var stream = data.AsStream();
			var bitmap = new BitmapImage();
			bitmap.BeginInit();
			bitmap.CacheOption = BitmapCacheOption.OnLoad;
			bitmap.StreamSource = stream;
			bitmap.EndInit();
			bitmap.Freeze();
			instance.Icon = bitmap;

			values.Add(instance);
		}

		panel.ItemsSource = values;
	}
}
