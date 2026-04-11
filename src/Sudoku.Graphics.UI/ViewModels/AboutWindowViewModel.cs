namespace Sudoku.Graphics.UI.ViewModels;

internal sealed class AboutWindowViewModel
{
	public Uri GitHubPage => new(LocalizationResources.ResourceManager.GetString("AboutWindow_AuthorGitHubPage", null)!);
}
