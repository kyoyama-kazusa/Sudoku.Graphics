namespace Sudoku.Graphics.UI.ViewModels;

internal sealed class MainWindowViewModel : INotifyPropertyChanged
{
	public MainWindowQuitCommand QuitCommand
	{
		get;

		set
		{
			field = value;
			PropertyChanged?.Invoke(this, new(nameof(QuitCommand)));
		}
	} = new();

	public MainWindowOpenAboutWindowCommand OpenAboutWindowCommand
	{
		get;

		set
		{
			field = value;
			PropertyChanged?.Invoke(this, new(nameof(OpenAboutWindowCommand)));
		}
	} = new();


	/// <inheritdoc/>
	public event PropertyChangedEventHandler? PropertyChanged;
}
