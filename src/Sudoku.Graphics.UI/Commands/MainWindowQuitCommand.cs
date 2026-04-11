namespace Sudoku.Graphics.UI.Commands;

/// <summary>
/// Represents a quit command.
/// </summary>
public sealed class MainWindowQuitCommand : ICommand
{
	/// <inheritdoc/>
	public event EventHandler? CanExecuteChanged;


	/// <inheritdoc/>
	public bool CanExecute(object? parameter) => true;

	/// <inheritdoc/>
	public void Execute(object? parameter)
	{
		if (parameter is not Window window)
		{
			return;
		}

		window.Close();
	}
}
