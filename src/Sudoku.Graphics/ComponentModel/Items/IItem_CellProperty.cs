namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents a type that includes <see cref="Cell"/> property.
/// </summary>
public interface IItem_CellProperty
{
	/// <summary>
	/// Indicates the cell to be drawn, of absolute cell index.
	/// For <see cref="Relative"/> cell indices, you can use <see cref="PointMapper.GetAbsoluteIndex(Relative)"/>
	/// to create absolute cells.
	/// </summary>
	/// <seealso cref="Relative"/>
	/// <seealso cref="PointMapper.GetAbsoluteIndex(Relative)"/>
	Absolute Cell { get; init; }
}
