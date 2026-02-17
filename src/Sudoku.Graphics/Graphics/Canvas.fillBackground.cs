namespace Sudoku.Graphics;

public partial class Canvas
{
	/// <inheritdoc/>
	public partial void FillBackground() => FillBackground(DrawingOptions.BackgroundColor.Resolve(DrawingOptions));

	/// <inheritdoc/>
	public partial void FillBackground(SKColor color) => BackingCanvas.Clear(color);
}
