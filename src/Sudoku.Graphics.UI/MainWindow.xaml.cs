namespace Sudoku.Graphics.UI;

/// <summary>
/// Interaction logic for MainWindow.xaml.
/// </summary>
[INotifyPropertyChanged]
public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();

		DataContext = this;
	}


	[ObservableProperty]
	public partial string CurrentModeString { get; set; } = string.Empty;

	[ObservableProperty]
	public partial ItemType CurrentItemType { get; set; } = ItemType.None;

	[ObservableProperty]
	public partial ImageSource? GridImageSource { get; set; }

	public ICommand CreateCanvasCommand { get; } = null!;

	public ICommand CloseCanvasCommand { get; } = null!;

	public ICommand SaveCanvasCommand { get; } = null!;

	public ICommand QuitCommand => new RelayCommand(Close);


	private void AboutMeMenuItem_Click(object sender, RoutedEventArgs e) => new AboutWindow { Owner = this }.ShowDialog();

	partial void OnCurrentItemTypeChanged(ItemType value)
	{
		var modeString = LocalizationResources.ResourceManager.GetString($"{nameof(ItemType)}_{value}");
		if (modeString is not null)
		{
			CurrentModeString = modeString;
		}
	}

	private void ToolItemButton_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button { Tag: ToolItem item })
		{
			CurrentItemType = item.ItemType;
		}
	}
}
