namespace Sudoku.Graphics;

public partial class Canvas
{
	/// <inheritdoc/>
	public partial void DrawLines()
	{
		foreach (var template in Templates)
		{
			template.DrawLines(BackingCanvas, Options);
		}
	}
}
