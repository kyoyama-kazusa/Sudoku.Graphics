namespace Sudoku.Graphics;

/// <summary>
/// Represents an item to be drawn. The item can be anything the canvas can draw - cell background, canvas background,
/// candidate highlight, grid lines, and other basic items to draw.
/// </summary>
public abstract record Item : IEqualityOperators<Item, Item, bool>
{
	/// <summary>
	/// Indicates the type of item.
	/// </summary>
	public abstract ItemType Type { get; }


	/// <summary>
	/// Try to draw the current item onto the specified canvas.
	/// </summary>
	/// <param name="canvas">The canvas to draw.</param>
	protected internal abstract void DrawTo(Canvas canvas);
}
