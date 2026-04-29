namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents cell triangle display item. This type will be used by creating
/// both types <see cref="CellTriangleMarkItem"/> and <see cref="CellArrowTriangleMarkItem"/>.
/// </summary>
/// <seealso cref="CellTriangleMarkItem"/>
/// <seealso cref="CellArrowTriangleMarkItem"/>
public sealed class CellTriagleDisplayItem : IIconDisplayItem
{
	/// <summary>
	/// Indicates the direction.
	/// </summary>
	public Direction8 Direction { get; set; }

	/// <inheritdoc/>
	public ImageSource? Icon { get; set; }
}
