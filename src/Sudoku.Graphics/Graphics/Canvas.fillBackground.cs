namespace Sudoku.Graphics;

public partial class Canvas
{
	/// <inheritdoc/>
	public partial void FillBackground() => FillBackground(Options.BackgroundColor.Resolve(Options));

	/// <inheritdoc/>
	public partial void FillBackground(SKColor color) => BackingCanvas.Clear(color);
}
