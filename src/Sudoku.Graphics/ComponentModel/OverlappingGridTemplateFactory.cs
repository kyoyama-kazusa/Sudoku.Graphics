namespace Sudoku.ComponentModel;

#pragma warning disable CS1572
/// <summary>
/// Provides a way to create overlapping grid templates.
/// </summary>
/// <shared-doc-comments>
/// <param name="blockRowsCount">The number of rows in a block.</param>
/// <param name="blockColumnsCount">The number of columns in a block.</param>
/// <param name="baseMapper">
/// The base mapper instance that contains basic information of point mapping rules, like margin (in pixels).
/// </param>
/// <returns>An array of <see cref="Template"/> instances.</returns>
/// <exception cref="ArgumentException">
/// Throws when the target template is not enough to create offsets, in order to form a valid such grid type.
/// </exception>
/// </shared-doc-comments>
#pragma warning restore CS1572
public static partial class OverlappingGridTemplateFactory
{
	/// <summary>
	/// Throws an exception <see cref="ArgumentException"/>.
	/// </summary>
	/// <returns>This method always throws and don't return anything.</returns>
	/// <exception cref="ArgumentException">Always throws.</exception>
	[DoesNotReturn]
	private static Template[] ThrowsArgumentException()
		=> throw new ArgumentException("The current block rows or columns count is invalid - The target grid must hold at least 3 parts to be split, in order to form such overlapping grid template.");
}
