namespace Sudoku.Graphics.UI.Views.OtherWindows;

/// <summary>
/// Interaction logic for CreateNewCanvasWindow.xaml.
/// </summary>
public partial class CreateNewCanvasWindow : Window
{
	private readonly CreateNewCanvasWindowViewModel _vm;


	/// <summary>
	/// Initializes a <see cref="CreateNewCanvasWindow"/> instance.
	/// </summary>
	public CreateNewCanvasWindow()
	{
		InitializeComponent();

		_vm = new(
			new CloseService(this),
			(closeService, dialogResult) => { DialogResult = dialogResult; closeService.Close(); }
		);
		DataContext = _vm;
	}


	public CreateNewCanvasWindowResult? Result => DialogResult is true ? _vm.GetCanvasCreateResult() : null;
}

file sealed class CloseService(CreateNewCanvasWindow _window) : ICloseService
{
	public void Close() => _window.Close();
}
