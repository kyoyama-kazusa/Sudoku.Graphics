namespace Sudoku.Graphics.UI.Views.OtherWindows;

/// <summary>
/// Interaction logic for CreateNewCanvasWindow.xaml.
/// </summary>
public partial class CreateNewCanvasWindow : Window
{
	/// <summary>
	/// Initializes a <see cref="CreateNewCanvasWindow"/> instance.
	/// </summary>
	public CreateNewCanvasWindow()
	{
		InitializeComponent();

		((CreateNewCanvasWindowViewModel)DataContext).CloseAction = result => { DialogResult = result; Close(); };
	}
}
