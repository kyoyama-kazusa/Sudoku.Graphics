using System;
using SkiaSharp;

namespace Sudoku.Graphics;

/// <summary>
/// Represents a default grid line template.
/// </summary>
/// <param name="rowsCount"><inheritdoc cref="RowsCount" path="/summary"/></param>
/// <param name="columnsCount"><inheritdoc cref="ColumnsCount" path="/summary"/></param>
/// <param name="outsideBlank"><inheritdoc cref="OutsideBlank" path="/summary"/></param>
/// <param name="mapper"><inheritdoc cref="GridLineTemplate.Mapper" path="/summary"/></param>
public sealed class DefaultGridLineTemplate(PointMapper mapper, int rowsCount, int columnsCount, ReservedOutsideBlank outsideBlank) :
	GridLineTemplate(mapper)
{
	/// <summary>
	/// Indicates the number of rows.
	/// </summary>
	public int RowsCount { get; } = rowsCount;

	/// <summary>
	/// Indicates the number of columns.
	/// </summary>
	public int ColumnsCount { get; } = columnsCount;

	/// <summary>
	/// Indicates the outside blank.
	/// </summary>
	public ReservedOutsideBlank OutsideBlank { get; } = outsideBlank;


	/// <inheritdoc/>
	public override void DrawLines(SKCanvas canvas)
	{
		throw new NotImplementedException();
	}
}
