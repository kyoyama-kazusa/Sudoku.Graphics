namespace Sudoku.ComponentModel;

public partial class OverlappingGridTemplateFactory
{
	/// <summary>
	/// Creates a <see cref="GridTemplate"/> array of Overlapped sudoku, with the specified grid size.
	/// </summary>
	/// <inheritdoc cref="OverlappingGridTemplateFactory" path="/shared-doc-comments"/>
	public static GridTemplate[] Overlapped(PointMapper baseMapper, LineDashSequence secondGridTemplateDashSequence)
		=> [
			new StandardGridTemplate(-1, -1, baseMapper.AddOffset(DirectionVector.Zero)) { IsBorderRoundedRectangle = false },
			new StandardGridTemplate(-1, -1, baseMapper.AddOffset(new(1, 0)))
			{
				IsBorderRoundedRectangle = false,
				ThickLineDashSequence = secondGridTemplateDashSequence
			}
		];

	/// <summary>
	/// Creates a <see cref="GridTemplate"/> array of Double-doku, with the specified rows and columns size of a block.
	/// </summary>
	/// <inheritdoc cref="OverlappingGridTemplateFactory" path="/shared-doc-comments"/>
	public static GridTemplate[] DoubleDoku(int blockRowsCount, int blockColumnsCount, PointMapper baseMapper)
		=> [
			new StandardGridTemplate(blockRowsCount, blockColumnsCount, baseMapper.AddOffset(DirectionVector.Zero))
			{
				IsBorderRoundedRectangle = false
			},
			new StandardGridTemplate(blockRowsCount, blockColumnsCount, baseMapper.AddOffset(new(blockRowsCount, 0, blockColumnsCount, 0)))
			{
				IsBorderRoundedRectangle = false
			}
		];

	/// <summary>
	/// Creates a <see cref="GridTemplate"/> array of Sensei, with the specified rows and columns size of a block.
	/// </summary>
	/// <inheritdoc cref="OverlappingGridTemplateFactory" path="/shared-doc-comments"/>
	public static GridTemplate[] Sensei(int blockRowsCount, int blockColumnsCount, PointMapper baseMapper)
		=> baseMapper.RowsCount / blockRowsCount is var rowSplitPartsCount && rowSplitPartsCount < 3
			? ThrowsArgumentException()
			: [
				new StandardGridTemplate(blockRowsCount, blockColumnsCount, baseMapper.AddOffset(DirectionVector.Zero))
				{
					IsBorderRoundedRectangle = false
				},
				new StandardGridTemplate(blockRowsCount, blockColumnsCount,baseMapper.AddOffset(new(blockRowsCount * (rowSplitPartsCount - 1), 0, blockColumnsCount, 0)) )
				{
					IsBorderRoundedRectangle = false
				}
			];

	/// <summary>
	/// Creates a <see cref="GridTemplate"/> array of Gattai-2, with the specified rows and columns size of a block.
	/// </summary>
	/// <inheritdoc cref="OverlappingGridTemplateFactory" path="/shared-doc-comments"/>
	public static GridTemplate[] Gattai2(int blockRowsCount, int blockColumnsCount, PointMapper baseMapper)
		=> (baseMapper.RowsCount / blockRowsCount, baseMapper.ColumnsCount / blockRowsCount) is var (rowSplitPartsCount, columnSplitPartsCount)
		&& (rowSplitPartsCount < 3 || columnSplitPartsCount < 3)
			? ThrowsArgumentException()
			: [
				new StandardGridTemplate(blockRowsCount, blockColumnsCount, baseMapper.AddOffset(DirectionVector.Zero))
				{
					IsBorderRoundedRectangle = false
				},
				new StandardGridTemplate(
					blockRowsCount,
					blockColumnsCount,
					baseMapper.AddOffset(
						new(blockRowsCount * (rowSplitPartsCount - 1), 0, blockColumnsCount * (columnSplitPartsCount - 1), 0)
					)
				)
				{
					IsBorderRoundedRectangle = false
				}
			];
}
