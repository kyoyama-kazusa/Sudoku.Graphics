namespace Sudoku.Graphics;

/// <summary>
/// Provides extension members on <see cref="PointMapper"/>.
/// </summary>
/// <seealso cref="PointMapper"/>
public static class PointMapperExtensions
{
	/// <param name="this">The current instance.</param>
	extension(PointMapper @this)
	{
		/// <summary>
		/// Indicates the number of cells the template can accommodate, excluding outside cells.
		/// </summary>
		public Absolute CellsCount => @this.RowsCount * @this.ColumnsCount;

		/// <summary>
		/// Indicates the number of cells the template can accommodate, including outside cells.
		/// </summary>
		public Absolute AbsoluteCellsCount => @this.AbsoluteRowsCount * @this.AbsoluteColumnsCount;

		/// <summary>
		/// Indicates the size of grid.
		/// </summary>
		public SKRect GridSize
			=> SKRect.Create(
				@this.Margin + @this.CellSize * @this.Vector.Left,
				@this.Margin + @this.CellSize * @this.Vector.Up,
				@this.CellSize * @this.ColumnsCount,
				@this.CellSize * @this.RowsCount
			);
	}
}
