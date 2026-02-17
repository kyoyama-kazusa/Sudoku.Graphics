namespace Sudoku.Graphics;

public partial class Canvas
{
	/// <inheritdoc/>
	public partial void DrawLines() => Options.GridLineTemplate.DrawLines(Mapper, BackingCanvas, Options);
}
