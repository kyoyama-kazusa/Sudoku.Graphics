namespace Sudoku.Graphics.UI.Views.Controls;

/// <summary>
/// A selector panel that generates buttons from <see cref="ItemsSource"/>.
/// </summary>
public partial class ItemSelectorPanel : UserControl
{
	public static readonly DependencyProperty ItemsSourceProperty =
		DependencyProperty.Register(
			nameof(ItemsSource),
			typeof(IEnumerable),
			typeof(ItemSelectorPanel),
			new PropertyMetadata(null, OnItemsSourceChanged)
		);

	public static readonly DependencyProperty SelectedItemProperty =
		DependencyProperty.Register(
			nameof(SelectedItem),
			typeof(object),
			typeof(ItemSelectorPanel),
			new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBoardConfigChanged)
		);

	public static readonly DependencyProperty ColumnsCountProperty =
		DependencyProperty.Register(
			nameof(ColumnsCount),
			typeof(int),
			typeof(ItemSelectorPanel),
			new PropertyMetadata(0, OnBoardConfigChanged)
		);

	public static readonly DependencyProperty CellSizeProperty =
		DependencyProperty.Register(
			nameof(CellSize),
			typeof(double),
			typeof(ItemSelectorPanel),
			new PropertyMetadata(40D, OnBoardConfigChanged)
		);

	public static readonly DependencyProperty ItemTemplateProperty =
		DependencyProperty.Register(
			nameof(ItemTemplate),
			typeof(DataTemplate),
			typeof(ItemSelectorPanel),
			new PropertyMetadata(null, OnBoardConfigChanged)
		);


	/// <summary>
	/// Initializes a <see cref="ItemSelectorPanel"/> instance.
	/// </summary>
	public ItemSelectorPanel()
	{
		InitializeComponent();

		Loaded += (_, _) => RefreshBoard();
		Unloaded += (_, _) => UnhookCollectionChanged(ItemsSource);
	}


	public IEnumerable? ItemsSource
	{
		get => (IEnumerable?)GetValue(ItemsSourceProperty);

		set => SetValue(ItemsSourceProperty, value);
	}

	public object? SelectedItem
	{
		get => GetValue(SelectedItemProperty);

		set => SetValue(SelectedItemProperty, value);
	}

	public int ColumnsCount
	{
		get => (int)GetValue(ColumnsCountProperty);

		set => SetValue(ColumnsCountProperty, value);
	}

	public double CellSize
	{
		get => (double)GetValue(CellSizeProperty);

		set => SetValue(CellSizeProperty, value);
	}

	public DataTemplate? ItemTemplate
	{
		get => (DataTemplate?)GetValue(ItemTemplateProperty);

		set => SetValue(ItemTemplateProperty, value);
	}


	public OperationHandlerContext? OperationHandlerContext { get; set; }


	public event EventHandler<ItemSelectorPanel, ItemSelectorPanelSelectedItemChangedEventArgs>? SelectedItemChanged;


	private void HookCollectionChanged(IEnumerable? source)
	{
		if (source is INotifyCollectionChanged incc)
		{
			CollectionChangedEventManager.AddHandler(incc, OnItemsCollectionChanged);
		}
	}

	private void UnhookCollectionChanged(IEnumerable? source)
	{
		if (source is INotifyCollectionChanged incc)
		{
			CollectionChangedEventManager.RemoveHandler(incc, OnItemsCollectionChanged);
		}
	}

	private void OnItemsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) => RefreshBoard();

	private void RefreshBoard()
	{
		if (PART_Root is null)
		{
			return;
		}

		PART_Root.Children.Clear();

		var items = EnumerateItems().ToArray();
		if (items.Length == 0)
		{
			return;
		}

		PART_Root.Columns = Math.Max(0, ColumnsCount);

		foreach (var item in items)
		{
			var button = new Button
			{
				Content = item,
				ContentTemplate = ItemTemplate,
				Width = CellSize,
				Height = CellSize,
				Margin = new Thickness(2),
				Tag = item,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center,
				FontSize = FontSize,
				FontFamily = FontFamily,
				FontWeight = FontWeight,
				Foreground = Foreground
			};

			button.Click += ItemButton_Click;

			PART_Root.Children.Add(button);
		}
	}

	private IEnumerable<object?> EnumerateItems()
	{
		if (ItemsSource is null)
		{
			yield break;
		}

		if (ItemsSource is string str)
		{
			yield return str;
			yield break;
		}

		foreach (var item in ItemsSource)
		{
			yield return item;
		}
	}

	private void ItemButton_Click(object sender, RoutedEventArgs e)
	{
		if (sender is not Button button)
		{
			return;
		}

		SelectedItem = button.Tag;
		SelectedItemChanged?.Invoke(
			this,
			new(SelectedItem, OperationHandlerContext ?? throw new InvalidOperationException("Expect non-null context."))
		);
	}

	private void ClearButton_Click(object sender, RoutedEventArgs e)
	{
		if (sender is not Button)
		{
			return;
		}

		SelectedItem = null;
		SelectedItemChanged?.Invoke(
			this,
			new(null, OperationHandlerContext ?? throw new InvalidOperationException("Expect non-null context."))
		);
	}


	private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var control = (ItemSelectorPanel)d;
		control.UnhookCollectionChanged(e.OldValue as IEnumerable);
		control.HookCollectionChanged(e.NewValue as IEnumerable);
		control.RefreshBoard();
	}

	private static void OnBoardConfigChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		=> ((ItemSelectorPanel)d).RefreshBoard();
}
