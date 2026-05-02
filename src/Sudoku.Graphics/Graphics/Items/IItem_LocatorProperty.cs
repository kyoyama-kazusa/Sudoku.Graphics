namespace Sudoku.Graphics.Items;

/// <summary>
/// Represents a type that includes <see cref="Locator"/> property.
/// </summary>
public interface IItem_LocatorProperty
{
	/// <summary>
	/// Indicates the cell or candidate to be drawn.
	/// </summary>
	Locator Locator { get; init; }
}
