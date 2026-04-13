namespace Sudoku.Graphics.UI;

/// <summary>
/// Interaction logic for MainWindow.xaml.
/// </summary>
public partial class MainWindow : Window
{
	private readonly MainWindowViewModel _vm;


	/// <summary>
	/// Initializes <see cref="MainWindow"/> class.
	/// </summary>
	public MainWindow()
	{
		InitializeComponent();

		_vm = new(
			new CloseService(this),
			new AboutDialogService(() => new() { Owner = this }),
			new CanvasCreateDialogService(() => new() { Owner = this }),
			new ImageGeneratorService(dialogService => dialogService.ShowDialog()),
			new SaveFileDialogService(),
			new ImageExportService()
		);
		DataContext = _vm;
	}
}

file sealed class ImageGeneratorService(Func<ICreateCanvasDialogService, CreateNewCanvasWindowResult?> _resultCreator) : IImageGeneratorService
{
	public ImageSource? Generate(ICreateCanvasDialogService dialogService)
	{
		var result = _resultCreator(dialogService);
		if (result is null)
		{
			return null;
		}

		var template = result.CreateTemplate();
		var canvas = new Canvas(template);
		canvas.DrawItems(
			new BackgroundFillItem { Color = SKColors.White },//Config
			new TemplateLineItem()
		);

		using var image = canvas.Surface.Snapshot();
		return image.ToWriteableBitmap();
	}
}

file sealed class CloseService(MainWindow mainWindow) : ICloseService
{
	public void Close() => mainWindow.Close();
}

file sealed class AboutDialogService(Func<AboutWindow> _windowCreator) : IDialogService
{
	public void ShowDialog() => _windowCreator().ShowDialog();
}

file sealed class CanvasCreateDialogService(Func<CreateNewCanvasWindow?> _windowCreator) : ICreateCanvasDialogService
{
	public CreateNewCanvasWindowResult? ShowDialog()
	{
		var dialog = _windowCreator();
		if (dialog is null)
		{
			return null;
		}

		var dialogResult = dialog.ShowDialog();
		return dialogResult is not true ? null : dialog.Result;
	}
}

file sealed class ImageExportService : IImageSaveService
{
	public void Save(ISaveFileDialogService saveFileDialogService, WriteableBitmap? image)
	{
		if (image is null)
		{
			return;
		}
		if (saveFileDialogService.ShowDialog() is not { } filePath)
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

		encoder.Frames.Add(BitmapFrame.Create(image));
		using var stream = File.Create(filePath);
		encoder.Save(stream);
	}
}

file sealed class SaveFileDialogService : ISaveFileDialogService
{
	public string? ShowDialog()
	{
		var dialog = new SaveFileDialog
		{
			Title = LocalizationResources.SaveFileDialog_Title,
			Filter = LocalizationResources.SaveFileDialog_Filters,
			DefaultExt = ".png",
			AddExtension = true
		};
		return dialog.ShowDialog() is true ? dialog.FileName : null;
	}
}
