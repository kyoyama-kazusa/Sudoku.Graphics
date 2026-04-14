namespace Sudoku.Graphics.UI.ViewModels;

internal sealed partial class MainWindowViewModel : ObservableObject
{
	public MainWindowViewModel(
		ICloseService closeService,
		IDialogService aboutDialogService,
		ICreateCanvasDialogService canvasCreateDialogService,
		IImageGeneratorService imageGeneratorService,
		ISaveFileDialogService saveFileDialogService,
		IImageSaveService imageSaveService
	)
	{
		QuitCommand = new RelayCommand(closeService.Close);
		OpenAboutWindowCommand = new RelayCommand(aboutDialogService.ShowDialog);
		CloseCommand = new RelayCommand(() => RenderedImage = null);
		OpenCreateNewCanvasCommand = new RelayCommand(() => OpenCreateCanvasWindowAndUpdate(canvasCreateDialogService, imageGeneratorService));
		SaveImageCommand = new RelayCommand(() => imageSaveService.Save(saveFileDialogService, RenderedImage as WriteableBitmap));
	}


	[ObservableProperty]
	public partial ImageSource? RenderedImage { get; set; }

	public ICommand QuitCommand { get; }

	public ICommand CloseCommand { get; }

	public ICommand OpenAboutWindowCommand { get; }

	public ICommand OpenCreateNewCanvasCommand { get; }

	public ICommand SaveImageCommand { get; }


	private void OpenCreateCanvasWindowAndUpdate(ICreateCanvasDialogService canvasCreateDialogService, IImageGeneratorService imageGeneratorService)
	{
		if (imageGeneratorService.Generate(canvasCreateDialogService) is { } image)
		{
			RenderedImage = image;
		}
	}
}
