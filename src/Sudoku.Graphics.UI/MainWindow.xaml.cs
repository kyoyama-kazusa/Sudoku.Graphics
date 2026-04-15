using System.IO;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

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

	public ICommand CreateCanvasCommand => new RelayCommand(RenderPicture);

	public ICommand CloseCanvasCommand => new RelayCommand(() => GridImageSource = null);

	public ICommand SaveCanvasCommand => new RelayCommand(SaveGridImageAsFile);

	public ICommand QuitCommand => new RelayCommand(Close);


	private void AboutMeMenuItem_Click(object sender, RoutedEventArgs e) => new AboutWindow { Owner = this }.ShowDialog();

	private void ToolItemButton_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button { Tag: ToolItem item })
		{
			CurrentItemType = item.ItemType;
		}
	}

	private void RenderPicture()
	{
		var window = new CreateNewCanvasWindow { Owner = this };
		var result = window.ShowDialog();
		if (result is not true)
		{
			return;
		}

		var template = window.CreateTemplate();
		var canvas = new Canvas(template);
		canvas.DrawItems(
			new BackgroundFillItem { Color = SKColors.White }, // Config
			new TemplateLineItem()
		);

		using var image = canvas.Surface.Snapshot();
		GridImageSource = image.ToWriteableBitmap();
	}

	private void SaveGridImageAsFile()
	{
		if (GridImageSource is null)
		{
			return;
		}

		var dialog = new SaveFileDialog
		{
			Title = LocalizationResources.SaveFileDialog_Title,
			Filter = LocalizationResources.SaveFileDialog_Filters,
			DefaultExt = ".png",
			AddExtension = true
		};
		var filePath = dialog.ShowDialog() is true ? dialog.FileName : null;
		if (filePath is null)
		{
			return;
		}

		var extension = Path.GetExtension(filePath);
		var encoder = (BitmapEncoder)(
			extension switch
			{
				".jpg" or ".jpeg" => new JpegBitmapEncoder(),
				".bmp" => new BmpBitmapEncoder(),
				_ => new PngBitmapEncoder()
			}
		);

		encoder.Frames.Add(BitmapFrame.Create((BitmapSource)GridImageSource));
		using var stream = File.Create(filePath);
		encoder.Save(stream);
	}

	partial void OnCurrentItemTypeChanged(ItemType value)
	{
		var modeString = LocalizationResources.ResourceManager.GetString($"{nameof(ItemType)}_{value}");
		if (modeString is not null)
		{
			CurrentModeString = modeString;
		}
	}
}
