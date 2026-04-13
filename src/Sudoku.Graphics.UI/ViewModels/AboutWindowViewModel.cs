namespace Sudoku.Graphics.UI.ViewModels;

internal sealed class AboutWindowViewModel : ObservableObject
{
	public AboutWindowViewModel(ICloseService closeService) => CloseCommand = new RelayCommand(closeService.Close);


	[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
	public Uri GitHubPage => new(LocalizationResources.ResourceManager.GetString("AboutWindow_AuthorGitHubPage", null)!);

	public ICommand CloseCommand { get; }
}
