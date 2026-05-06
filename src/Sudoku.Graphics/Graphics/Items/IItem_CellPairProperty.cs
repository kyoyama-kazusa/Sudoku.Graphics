namespace Sudoku.Graphics.Items;

/// <summary>
/// Represents a type that includes properties <see cref="Cell1"/> and <see cref="Cell2"/>.
/// </summary>
public interface IItem_CellPairProperty
{
	/// <summary>
	/// Indicates the first cell.
	/// </summary>
	Absolute Cell1 { get; init; }

	/// <summary>
	/// Indicates the second cell.
	/// </summary>
	Absolute Cell2 { get; init; }
}
