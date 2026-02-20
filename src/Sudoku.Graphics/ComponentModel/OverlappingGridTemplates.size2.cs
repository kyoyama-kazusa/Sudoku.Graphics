namespace Sudoku.ComponentModel;

public partial class OverlappingGridTemplates
{
	/// <summary>
	/// Creates a <see cref="GridTemplate"/> array of Overlapped sudoku, with the specified grid size.
	/// </summary>
	/// <inheritdoc cref="OverlappingGridTemplates" path="/shared-doc-comments"/>
	public static GridTemplate[] Overlapped(PointMapper baseMapper, LineDashSequence secondGridTemplateDashSequence)
		=> [
			new StandardGridTemplate { IsBorderRoundedRectangle = false, Mapper = baseMapper.AddOffset(DirectionVector.Zero) },
			new StandardGridTemplate
			{
				IsBorderRoundedRectangle = false,
				Mapper = baseMapper.AddOffset(new(1, 0)),
				ThickLineDashSequence = secondGridTemplateDashSequence
			}
		];

	/// <summary>
	/// Creates a <see cref="GridTemplate"/> array of Double-doku, with the specified rows and columns size of a block.
	/// </summary>
	/// <inheritdoc cref="OverlappingGridTemplates" path="/shared-doc-comments"/>
	public static GridTemplate[] DoubleDoku(int blockRowsCount, int blockColumnsCount, PointMapper baseMapper)
		=> [
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
				Mapper = baseMapper.AddOffset(new(blockRowsCount, 0, blockColumnsCount, 0))
			}
		];

	/// <summary>
	/// Creates a <see cref="GridTemplate"/> array of Sensei, with the specified rows and columns size of a block.
	/// </summary>
	/// <inheritdoc cref="OverlappingGridTemplates" path="/shared-doc-comments"/>
	public static GridTemplate[] Sensei(int blockRowsCount, int blockColumnsCount, PointMapper baseMapper)
		=> baseMapper.RowsCount / blockRowsCount is var rowSplitPartsCount && rowSplitPartsCount < 3
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
					Mapper = baseMapper.AddOffset(new(blockRowsCount * (rowSplitPartsCount - 1), 0, blockColumnsCount, 0))
				}
			];

	/// <summary>
	/// Creates a <see cref="GridTemplate"/> array of Gattai-2, with the specified rows and columns size of a block.
	/// </summary>
	/// <inheritdoc cref="OverlappingGridTemplates" path="/shared-doc-comments"/>
	public static GridTemplate[] Gattai2(int blockRowsCount, int blockColumnsCount, PointMapper baseMapper)
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
				}
			];
}
