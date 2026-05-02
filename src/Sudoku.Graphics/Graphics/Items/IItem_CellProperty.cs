namespace Sudoku.Graphics.Items;

/// <summary>
/// Represents a type that includes <see cref="Cell"/> property.
/// </summary>
public interface IItem_CellProperty : IItem_LocatorProperty
{
	/// <summary>
	/// Indicates the cell to be drawn, of absolute cell index.
	/// For <see cref="Relative"/> cell indices, you can use <see cref="Relative.ToAbsolute(PointMapper)"/>
	/// to create absolute cells.
	/// </summary>
	/// <seealso cref="Relative"/>
	/// <seealso cref="Relative.ToAbsolute(PointMapper)"/>
	Absolute Cell { get; init; }

	/// <inheritdoc/>
	Locator IItem_LocatorProperty.Locator
	{
		get => Cell;

		init
			=> Cell = value is Absolute a
				? a
				: throw new ArgumentException($"Type mismatches - expected '{nameof(Absolute)}'.", nameof(value));
	}
}
