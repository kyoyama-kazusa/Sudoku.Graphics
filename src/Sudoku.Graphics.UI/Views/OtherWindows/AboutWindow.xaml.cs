namespace Sudoku.Graphics.UI.Views.OtherWindows;

/// <summary>
/// Interaction logic for AboutWindow.xaml.
/// </summary>
public partial class AboutWindow : Window
{
	private readonly AboutWindowViewModel _vm;


	/// <summary>
	/// Initializes an <see cref="AboutWindow"/> instance.
	/// </summary>
	public AboutWindow()
	{
		InitializeComponent();

		_vm = new(new CloseService(this));
		DataContext = _vm;
	}


	private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
	{
		Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
		e.Handled = true;
	}
}

file sealed class CloseService(AboutWindow _window) : ICloseService
{
	public void Close() => _window.Close();
}
