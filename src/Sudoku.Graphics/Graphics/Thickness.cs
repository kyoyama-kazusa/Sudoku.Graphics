namespace Sudoku.Graphics;

/// <summary>
/// Represents thickness.
/// </summary>
/// <param name="Left">Indicates the left offset size.</param>
/// <param name="Top">Indicates the top offset size.</param>
/// <param name="Right">Indicates the right offset size.</param>
/// <param name="Bottom">Indicates the bottom size.</param>
public readonly record struct Thickness(float Left, float Top, float Right, float Bottom)
{
	/// <summary>
	/// Initializes a <see cref="Thickness"/> instance.
	/// </summary>
	/// <param name="uniform">The uniform value.</param>
	public Thickness(float uniform) : this(uniform, uniform, uniform, uniform)
	{
	}
}
