namespace Sudoku.Graphics.UI.Commands;

/// <summary>
/// Represents open about window command.
/// </summary>
internal sealed class MainWindowOpenAboutWindowCommand : ICommand
{
	/// <inheritdoc/>
	public event EventHandler? CanExecuteChanged;


	/// <inheritdoc/>
	public bool CanExecute(object? parameter) => true;

	/// <inheritdoc/>
	public void Execute(object? parameter)
	{
		if (parameter is not MainWindow)
		{
			return;
		}

		var aboutWindow = new AboutWindow();
		aboutWindow.ShowDialog();
	}
}
