namespace Sudoku.Graphics.UI;

public partial class MainWindow
{
	/// <summary>
	/// Represents a snapshot on creation.
	/// </summary>
	/// <param name="BackgroundFill">The background fill item.</param>
	/// <param name="TemplateLine">The template line item.</param>
	/// <param name="Template">The template.</param>
	/// <param name="Grid">The grid.</param>
	private sealed record Snapshot(BackgroundFillItem BackgroundFill, TemplateLineItem TemplateLine, Template Template, SudokuGrid Grid);
}
