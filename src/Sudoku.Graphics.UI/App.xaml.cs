namespace Sudoku.Graphics.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	/// <inheritdoc/>
	protected override void OnStartup(StartupEventArgs e)
	{
		var culture = new CultureInfo("zh-CN");
		Thread.CurrentThread.CurrentUICulture = culture;
		Thread.CurrentThread.CurrentCulture = culture;

		base.OnStartup(e);
	}
}
