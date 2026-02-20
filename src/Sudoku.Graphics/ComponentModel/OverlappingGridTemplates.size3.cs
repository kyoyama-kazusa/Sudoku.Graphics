namespace Sudoku.ComponentModel;

public partial class OverlappingGridTemplates
{
	/// <summary>
	/// Creates a <see cref="GridTemplate"/> array of Gattai-3, with the specified rows and columns size of a block.
	/// </summary>
	/// <inheritdoc cref="OverlappingGridTemplates" path="/shared-doc-comments"/>
	public static GridTemplate[] Gattai3(int blockRowsCount, int blockColumnsCount, PointMapper baseMapper)
		=> (baseMapper.RowsCount / blockRowsCount, baseMapper.ColumnsCount / blockRowsCount) is var (rowSplitPartsCount, columnSplitPartsCount)
		&& (rowSplitPartsCount < 3 || columnSplitPartsCount < 3)
			? ThrowsArgumentException()
			: [
				new StandardGridTemplate
				{
					IsBorderRoundedRectangle = false,
					RowBlockSize = blockRowsCount,
					ColumnBlockSize = blockColumnsCount,
					Mapper = baseMapper.AddOffset(new(0, 0, blockColumnsCount, 0))
				},
				new StandardGridTemplate
				{
					IsBorderRoundedRectangle = false,
					RowBlockSize = blockRowsCount,
					ColumnBlockSize = blockColumnsCount,
					Mapper = baseMapper.AddOffset(new(blockRowsCount, 0, blockColumnsCount * (columnSplitPartsCount - 1), 0))
				},
				new StandardGridTemplate
				{
					IsBorderRoundedRectangle = false,
					RowBlockSize = blockRowsCount,
					ColumnBlockSize = blockColumnsCount,
					Mapper = baseMapper.AddOffset(new(blockRowsCount * (rowSplitPartsCount - 1), 0, 0, 0))
				}
			];

	/// <summary>
	/// Creates a <see cref="GridTemplate"/> array of Wing, with the specified rows and columns size of a block.
	/// </summary>
	/// <inheritdoc cref="OverlappingGridTemplates" path="/shared-doc-comments"/>
	public static GridTemplate[] Wing(int blockRowsCount, int blockColumnsCount, PointMapper baseMapper)
		=> (baseMapper.RowsCount / blockRowsCount, baseMapper.ColumnsCount / blockRowsCount) is var (rowSplitPartsCount, columnSplitPartsCount)
		&& (rowSplitPartsCount < 3 || columnSplitPartsCount < 3)
			? ThrowsArgumentException()
			: [
				new StandardGridTemplate
				{
					IsBorderRoundedRectangle = false,
					RowBlockSize = blockRowsCount,
					ColumnBlockSize = blockColumnsCount,
					Mapper = baseMapper.AddOffset(DirectionVector.Zero)
				},
				new StandardGridTemplate
				{
					IsBorderRoundedRectangle = false,
					RowBlockSize = blockRowsCount,
					ColumnBlockSize = blockColumnsCount,
					Mapper = baseMapper.AddOffset(
						new(blockRowsCount * (rowSplitPartsCount - 1), 0, blockColumnsCount * (columnSplitPartsCount - 1), 0)
					)
				},
				new StandardGridTemplate
				{
					IsBorderRoundedRectangle = false,
					RowBlockSize = blockRowsCount,
					ColumnBlockSize = blockColumnsCount,
					Mapper = baseMapper.AddOffset(new(0, 0, blockRowsCount * 2 * (blockColumnsCount - 1), 0))
				}
			];
}
