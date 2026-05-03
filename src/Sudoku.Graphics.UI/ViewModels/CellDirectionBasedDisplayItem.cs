namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents cell direction-based display item. This type will be used by creating
/// types <see cref="CellTriangleMarkItem"/>, <see cref="CellArrowTriangleMarkItem"/>, <see cref="CellArrowMarkItem"/>
/// and <see cref="CellArrowTextMarkItem"/>.
/// </summary>
/// <seealso cref="CellTriangleMarkItem"/>
/// <seealso cref="CellArrowTriangleMarkItem"/>
/// <seealso cref="CellArrowMarkItem"/>
/// <seealso cref="CellArrowTextMarkItem"/>
public sealed class CellDirectionBasedDisplayItem : IIconDisplayItem
{
	/// <summary>
	/// Indicates the direction.
	/// </summary>
	public Direction8 Direction { get; set; }

	/// <inheritdoc/>
	public ImageSource? Icon { get; set; }
}
