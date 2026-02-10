namespace Sudoku.Graphics;

/// <summary>
/// Represents a default grid line template.
/// </summary>
/// <param name="rowsCount"><inheritdoc cref="RowsCount" path="/summary"/></param>
/// <param name="columnsCount"><inheritdoc cref="ColumnsCount" path="/summary"/></param>
/// <param name="rowBlockSize"><inheritdoc cref="RowBlockSize" path="/summary"/></param>
/// <param name="columnBlockSize"><inheritdoc cref="ColumnBlockSize" path="/summary"/></param>
/// <param name="vector"><inheritdoc cref="Vector" path="/summary"/></param>
public sealed class DefaultGridLineTemplate(
	int rowsCount,
	int columnsCount,
	int rowBlockSize,
	int columnBlockSize,
	DirectionVector vector
) : GridLineTemplate
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
	/// Indicates the number of rows in a rectangular block.
	/// </summary>
	public int RowBlockSize { get; } = rowBlockSize;

	/// <summary>
	/// Indicates the number of columns in a rectangular block.
	/// </summary>
	public int ColumnBlockSize { get; } = columnBlockSize;

	/// <summary>
	/// Indicates the vector as outside blank.
	/// </summary>
	public DirectionVector Vector { get; } = vector;


	/// <inheritdoc/>
	public override void DrawLines(PointMapper mapper, SKCanvas canvas, CanvasDrawingOptions options)
	{
		// Draw border rectangle.
		var path = new SKPath();
		path.AddRoundRect(
			new(
				SKRect.Create(
					mapper.Margin,
					mapper.Margin,
					mapper.GridSize.Width,
					mapper.GridSize.Height
				),
				options.GridBorderRoundedRectangleCornerRadius.Measure(mapper.CellWidthAndHeight)
			)
		);
		using var borderPaint = new SKPaint
		{
			Style = SKPaintStyle.Stroke,
			Color = options.ThickLineColor,
			StrokeWidth = options.ThickLineWidth.Measure(mapper.CellWidthAndHeight),
			StrokeCap = SKStrokeCap.Round,
			IsAntialias = true
		};
		canvas.DrawPath(path, borderPaint);
	}
}
