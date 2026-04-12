namespace Sudoku.Graphics.UI.Services;

public interface IDialogService
{
	void ShowDialog();
}

public interface IDialogService<TResult> where TResult : notnull
{
	TResult? ShowDialog();
}
