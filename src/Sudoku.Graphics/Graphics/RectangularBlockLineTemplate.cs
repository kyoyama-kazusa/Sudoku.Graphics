namespace Sudoku.Graphics;

/// <summary>
/// Represents a default (rectangular) block line template.
/// </summary>
/// <param name="rowBlockSize"><inheritdoc cref="RowBlockSize" path="/summary"/></param>
/// <param name="columnBlockSize"><inheritdoc cref="ColumnBlockSize" path="/summary"/></param>
public sealed class RectangularBlockLineTemplate(int rowBlockSize, int columnBlockSize) : BlockLineTemplate
{
	/// <summary>
	/// Indicates the number of rows in a rectangular block.
	/// </summary>
	public int RowBlockSize { get; } = rowBlockSize;

	/// <summary>
	/// Indicates the number of columns in a rectangular block.
	/// </summary>
	public int ColumnBlockSize { get; } = columnBlockSize;


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
