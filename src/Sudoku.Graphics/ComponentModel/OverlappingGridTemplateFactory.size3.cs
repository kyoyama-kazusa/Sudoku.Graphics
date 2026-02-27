namespace Sudoku.ComponentModel;

public partial class OverlappingGridTemplateFactory
{
	/// <summary>
	/// Creates a <see cref="Template"/> array of Gattai-3, with the specified rows and columns size of a block.
	/// </summary>
	/// <inheritdoc cref="OverlappingGridTemplateFactory" path="/shared-doc-comments"/>
	public static Template[] Gattai3(int blockRowsCount, int blockColumnsCount, PointMapper baseMapper)
		=> (baseMapper.RowsCount / blockRowsCount, baseMapper.ColumnsCount / blockRowsCount) is var (rowSplitPartsCount, columnSplitPartsCount)
		&& (rowSplitPartsCount < 3 || columnSplitPartsCount < 3)
			? ThrowsArgumentException()
			: [
				new StandardTemplate(blockRowsCount, blockColumnsCount, baseMapper.AddOffset(new(0, 0, blockColumnsCount, 0)))
				{
					IsBorderRoundedRectangle = false
				},
				new StandardTemplate(
					blockRowsCount,
					blockColumnsCount,
					baseMapper.AddOffset(new(blockRowsCount, 0, blockColumnsCount * (columnSplitPartsCount - 1), 0))
				)
				{
					IsBorderRoundedRectangle = false
				},
				new StandardTemplate(
					blockRowsCount,
					blockColumnsCount,
					baseMapper.AddOffset(new(blockRowsCount * (rowSplitPartsCount - 1), 0, 0, 0))
				)
				{
					IsBorderRoundedRectangle = false
				}
			];

	/// <summary>
	/// Creates a <see cref="Template"/> array of Wing, with the specified rows and columns size of a block.
	/// </summary>
	/// <inheritdoc cref="OverlappingGridTemplateFactory" path="/shared-doc-comments"/>
	public static Template[] Wing(int blockRowsCount, int blockColumnsCount, PointMapper baseMapper)
		=> (baseMapper.RowsCount / blockRowsCount, baseMapper.ColumnsCount / blockRowsCount) is var (rowSplitPartsCount, columnSplitPartsCount)
		&& (rowSplitPartsCount < 3 || columnSplitPartsCount < 3)
			? ThrowsArgumentException()
			: [
				new StandardTemplate(blockRowsCount, blockColumnsCount, baseMapper.AddOffset(DirectionVector.Zero))
				{
					IsBorderRoundedRectangle = false
				},
				new StandardTemplate(
					blockRowsCount,
					blockColumnsCount,
					baseMapper.AddOffset(
						new(blockRowsCount * (rowSplitPartsCount - 1), 0, blockColumnsCount * (columnSplitPartsCount - 1), 0)
					)
				)
				{
					IsBorderRoundedRectangle = false
				},
				new StandardTemplate(
					blockRowsCount,
					blockColumnsCount,
					baseMapper.AddOffset(new(0, 0, blockRowsCount * 2 * (blockColumnsCount - 1), 0))
				)
				{
					IsBorderRoundedRectangle = false
				}
			];
}
