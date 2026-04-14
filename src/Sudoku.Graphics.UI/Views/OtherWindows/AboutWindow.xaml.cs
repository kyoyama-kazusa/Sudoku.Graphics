namespace Sudoku.Graphics.UI.Views.OtherWindows;

/// <summary>
/// Interaction logic for AboutWindow.xaml.
/// </summary>
public partial class AboutWindow : Window
{
	public static readonly Uri GitHubPage = new(LocalizationResources.ResourceManager.GetString("AboutWindow_GitHubPage", null)!);

	public static readonly Uri BilibiliPage = new(LocalizationResources.ResourceManager.GetString("AboutWindow_BilibiliPage", null)!);


	public AboutWindow() => InitializeComponent();


	private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
	{
		Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
		e.Handled = true;
	}
}
