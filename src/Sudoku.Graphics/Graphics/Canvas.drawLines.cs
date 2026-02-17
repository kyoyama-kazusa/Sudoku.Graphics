namespace Sudoku.Graphics;

public partial class Canvas
{
	/// <inheritdoc/>
	public partial void DrawLines() => DrawingOptions.GridLineTemplate.DrawLines(Mapper, BackingCanvas, DrawingOptions);
}
