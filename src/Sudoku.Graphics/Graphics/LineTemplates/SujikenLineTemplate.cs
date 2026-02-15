namespace Sudoku.Graphics.LineTemplates;

/// <summary>
/// Represents Sujiken (halfdoku) line template.
/// </summary>
/// <param name="uniformSize">The uniformed value.</param>
public sealed class SujikenLineTemplate(Relative uniformSize) : LineTemplate
{
	/// <summary>
	/// Indicates the number of rows and columns in a block.
	/// </summary>
	public Relative UniformBlockSize { get; } = uniformSize;


	/// <inheritdoc/>
	public override void DrawLines(PointMapper mapper, SKCanvas canvas, CanvasDrawingOptions options)
	{
		ArgumentException.Assert(mapper.RowsCount % UniformBlockSize == 0);
		ArgumentException.Assert(mapper.ColumnsCount % UniformBlockSize == 0);
		ArgumentException.Assert(mapper.RowsCount == mapper.ColumnsCount);

		var rowCellIndicesLookup = new Dictionary<int, Relative>();
		var columnCellIndicesLookup = new Dictionary<int, Relative>();

		drawBorderRectangle();
		drawGridLines();


		void drawBorderRectangle()
		{
			var path = new SKPath();
			var iteratingCellIndex = 0;
			var iteratingPoint = mapper.GetPoint(mapper.ToAbsoluteIndex(iteratingCellIndex), CellCornerType.TopLeft);
			var diagonalPoints = new List<SKPoint> { iteratingPoint };
			for (var i = 0; i < mapper.RowsCount - 1; i++)
			{
				iteratingCellIndex++;
				diagonalPoints.Add(mapper.GetPoint(mapper.ToAbsoluteIndex(iteratingCellIndex), CellCornerType.TopLeft));
				rowCellIndicesLookup.Add(i, iteratingCellIndex - 1);

				iteratingCellIndex += mapper.ColumnsCount;
				diagonalPoints.Add(mapper.GetPoint(mapper.ToAbsoluteIndex(iteratingCellIndex), CellCornerType.TopLeft));
				columnCellIndicesLookup.Add(i, iteratingCellIndex);
			}

			rowCellIndicesLookup.Add(mapper.RowsCount - 1, mapper.RowsCount * mapper.ColumnsCount - 1);
			columnCellIndicesLookup.Add(mapper.RowsCount - 1, mapper.RowsCount * mapper.ColumnsCount - 1);

			var lastCellIndex = mapper.ToAbsoluteIndex(mapper.RowsCount * mapper.ColumnsCount - 1);
			var firstCellIndexInLastRow = mapper.ToAbsoluteIndex((mapper.RowsCount - 1) * mapper.ColumnsCount);

			path.AddPoly(
				[
					.. diagonalPoints,
					mapper.GetPoint(lastCellIndex, CellCornerType.TopRight),
					mapper.GetPoint(lastCellIndex, CellCornerType.BottomRight),
					mapper.GetPoint(firstCellIndexInLastRow, CellCornerType.BottomLeft)
				],
				true
			);
			using var borderPaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				Color = options.ThickLineColor.Resolve(options),
				StrokeWidth = options.ThickLineWidth.Resolve(options).Measure(mapper.CellSize),
				StrokeCap = SKStrokeCap.Round,
				StrokeJoin = SKStrokeJoin.Round,
				IsAntialias = true
			};
			canvas.DrawPath(path, borderPaint);
		}

		void drawGridLines()
		{
			using var thickLinePaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				Color = options.ThickLineColor.Resolve(options),
				StrokeWidth = options.ThickLineWidth.Resolve(options).Measure(mapper.CellSize),
				StrokeCap = SKStrokeCap.Round,
				IsAntialias = true
			};
			using var thinLinePaint = new SKPaint
			{
				Style = SKPaintStyle.Stroke,
				Color = options.ThinLineColor.Resolve(options),
				StrokeWidth = options.ThinLineWidth.Resolve(options).Measure(mapper.CellSize),
				StrokeCap = SKStrokeCap.Round,
				IsAntialias = true
			};

			// Horizontal lines.
			for (var i = 0; i < mapper.RowsCount; i++)
			{
				var a = mapper.GetPoint(mapper.ToAbsoluteIndex(i * mapper.ColumnsCount), CellCornerType.TopLeft);
				var b = mapper.GetPoint(mapper.ToAbsoluteIndex(rowCellIndicesLookup[i]), CellCornerType.TopLeft);
				canvas.DrawLine(a, b, i % UniformBlockSize == 0 ? thickLinePaint : thinLinePaint);
			}

			// Vertical lines.
			for (var i = 1; i < mapper.ColumnsCount; i++)
			{
				var a = mapper.GetPoint(mapper.ToAbsoluteIndex(columnCellIndicesLookup[i - 1]), CellCornerType.TopLeft);
				var b = mapper.GetPoint(mapper.ToAbsoluteIndex(mapper.RowsCount * mapper.ColumnsCount + i), CellCornerType.TopLeft);
				canvas.DrawLine(a, b, i % UniformBlockSize == 0 ? thickLinePaint : thinLinePaint);
			}
		}
	}
}
