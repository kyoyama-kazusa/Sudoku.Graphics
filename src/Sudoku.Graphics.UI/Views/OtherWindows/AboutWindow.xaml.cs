namespace Sudoku.Graphics.UI.Views.OtherWindows;

/// <summary>
/// Interaction logic for AboutWindow.xaml.
/// </summary>
public partial class AboutWindow : Window
{
	/// <summary>
	/// Initializes an <see cref="AboutWindow"/> instance.
	/// </summary>
	public AboutWindow() => InitializeComponent();


	private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
	{
		Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
		e.Handled = true;
	}
}
