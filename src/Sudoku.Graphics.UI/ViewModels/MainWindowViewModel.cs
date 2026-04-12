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
		CloseCommand = new RelayCommand(() => PreviewImage = null);
		OpenCreateNewCanvasCommand = new RelayCommand(() => PreviewImage = imageGeneratorService.Generate(canvasCreateDialogService));
		SaveImageCommand = new RelayCommand(() => imageSaveService.Save(saveFileDialogService, PreviewImage as WriteableBitmap));
	}


	[ObservableProperty]
	public partial ImageSource? PreviewImage { get; set; }

	public ICommand QuitCommand { get; }

	public ICommand CloseCommand { get; }

	public ICommand OpenAboutWindowCommand { get; }

	public ICommand OpenCreateNewCanvasCommand { get; }

	public ICommand SaveImageCommand { get; }
}
