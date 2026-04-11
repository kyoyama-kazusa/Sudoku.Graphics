namespace Sudoku.Graphics.UI.ViewModels;

internal sealed partial class MainWindowViewModel : ObservableObject
{
	public MainWindowViewModel()
	{
		QuitCommand = new MainWindowQuitCommand();
		OpenAboutWindowCommand = new MainWindowOpenAboutWindowCommand();
		CloseCommand = new RelayCommand(CloseCanvas);
		OpenCreateNewCanvasCommand = new RelayCommand(OpenCreateNewCanvasWindow);
	}


	[ObservableProperty]
	public partial ImageSource? PreviewImage { get; set; }

	public ICommand QuitCommand { get; }

	public ICommand CloseCommand { get; }

	public ICommand OpenAboutWindowCommand { get; }

	public ICommand OpenCreateNewCanvasCommand { get; }


	private void CloseCanvas() => PreviewImage = null;

	private void OpenCreateNewCanvasWindow()
	{
		var window = new CreateNewCanvasWindow();
		var dialogResult = window.ShowDialog();
		if (dialogResult is not true)
		{
			return;
		}

		var dataContext = (CreateNewCanvasWindowViewModel)window.DataContext;
		var result = dataContext.GetResult();
		if (result is null)
		{
			return;
		}

		var mapper = new PointMapper { CellSize = result.CellSize, Margin = result.Margin, TemplateSize = result.TemplateSize };
		var template = new StandardTemplate(result.BlockRowsCount, result.BlockColumnsCount, mapper)
		{
			ThickLineColor = SKColors.Black,//Config
			ThickLineWidth = 0.06M,//Config
			ThickLineDashSequence = [],//Config
			ThinLineColor = SKColors.Black,//Config
			ThinLineWidth = 0.0225M,//Config
			ThinLineDashSequence = []//Config
		};
		GenerateImage(template);
	}

	private void GenerateImage(Template template)
	{
		var canvas = new Canvas(template);
		canvas.DrawItems(
			new BackgroundFillItem { Color = SKColors.White },//Config
			new TemplateLineItem()
		);

		using var skImage = canvas.Surface.Snapshot();
		PreviewImage = skImage.ToWriteableBitmap();
	}
}
