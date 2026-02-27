namespace Sudoku.ComponentModel;

public partial class OverlappingGridTemplateFactory
{
	/// <summary>
	/// Creates a <see cref="Template"/> array of Overlapped sudoku, with the specified grid size.
	/// </summary>
	/// <inheritdoc cref="OverlappingGridTemplateFactory" path="/shared-doc-comments"/>
	public static Template[] Overlapped(PointMapper baseMapper, LineDashSequence secondGridTemplateDashSequence)
		=> [
			new StandardTemplate(-1, -1, baseMapper.AddOffset(DirectionVector.Zero)) { IsBorderRoundedRectangle = false },
			new StandardTemplate(-1, -1, baseMapper.AddOffset(new(1, 0)))
			{
				IsBorderRoundedRectangle = false,
				ThickLineDashSequence = secondGridTemplateDashSequence
			}
		];

	/// <summary>
	/// Creates a <see cref="Template"/> array of Double-doku, with the specified rows and columns size of a block.
	/// </summary>
	/// <inheritdoc cref="OverlappingGridTemplateFactory" path="/shared-doc-comments"/>
	public static Template[] DoubleDoku(int blockRowsCount, int blockColumnsCount, PointMapper baseMapper)
		=> [
			new StandardTemplate(blockRowsCount, blockColumnsCount, baseMapper.AddOffset(DirectionVector.Zero))
			{
				IsBorderRoundedRectangle = false
			},
			new StandardTemplate(blockRowsCount, blockColumnsCount, baseMapper.AddOffset(new(blockRowsCount, 0, blockColumnsCount, 0)))
			{
				IsBorderRoundedRectangle = false
			}
		];

	/// <summary>
	/// Creates a <see cref="Template"/> array of Sensei, with the specified rows and columns size of a block.
	/// </summary>
	/// <inheritdoc cref="OverlappingGridTemplateFactory" path="/shared-doc-comments"/>
	public static Template[] Sensei(int blockRowsCount, int blockColumnsCount, PointMapper baseMapper)
		=> baseMapper.RowsCount / blockRowsCount is var rowSplitPartsCount && rowSplitPartsCount < 3
			? ThrowsArgumentException()
			: [
				new StandardTemplate(blockRowsCount, blockColumnsCount, baseMapper.AddOffset(DirectionVector.Zero))
				{
					IsBorderRoundedRectangle = false
				},
				new StandardTemplate(blockRowsCount, blockColumnsCount,baseMapper.AddOffset(new(blockRowsCount * (rowSplitPartsCount - 1), 0, blockColumnsCount, 0)) )
				{
					IsBorderRoundedRectangle = false
				}
			];

	/// <summary>
	/// Creates a <see cref="Template"/> array of Gattai-2, with the specified rows and columns size of a block.
	/// </summary>
	/// <inheritdoc cref="OverlappingGridTemplateFactory" path="/shared-doc-comments"/>
	public static Template[] Gattai2(int blockRowsCount, int blockColumnsCount, PointMapper baseMapper)
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
				}
			];
}
