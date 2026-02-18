namespace Sudoku.Graphics.LineTemplates;

/// <summary>
/// Represents Sujiken (halfdoku) line template.
/// </summary>
/// <param name="uniformSize">The uniformed value.</param>
public sealed class SujikenLineTemplate(Relative uniformSize) : IndividualBlockLineTemplate
{
	/// <summary>
	/// Provides extra lookup dictionary.
	/// </summary>
	private Dictionary<int, Relative>? _rowCellIndicesLookup, _columnCellIndicesLookup;


	/// <summary>
	/// Indicates the number of rows and columns in a block.
	/// </summary>
	public Relative UniformBlockSize { get; } = uniformSize;


	/// <inheritdoc/>
	protected override void GuardStatements(PointMapper mapper, SKCanvas canvas, CanvasDrawingOptions options)
	{
		ArgumentException.Assert(mapper.RowsCount % UniformBlockSize == 0);
		ArgumentException.Assert(mapper.ColumnsCount % UniformBlockSize == 0);
		ArgumentException.Assert(mapper.RowsCount == mapper.ColumnsCount);
	}

	/// <inheritdoc/>
	[MemberNotNull(nameof(_rowCellIndicesLookup), nameof(_columnCellIndicesLookup))]
	protected override void DrawBorderRectangle(PointMapper mapper, SKCanvas canvas, CanvasDrawingOptions options)
	{
		_rowCellIndicesLookup = [];
		_columnCellIndicesLookup = [];

		var path = new SKPath();
		var iteratingCellIndex = 0;
		var iteratingPoint = mapper.GetPoint(mapper.GetAbsoluteIndex(iteratingCellIndex), CellAlignment.TopLeft);
		var diagonalPoints = new List<SKPoint> { iteratingPoint };
		for (var i = 0; i < mapper.RowsCount - 1; i++)
		{
			iteratingCellIndex++;
			diagonalPoints.Add(mapper.GetPoint(mapper.GetAbsoluteIndex(iteratingCellIndex), CellAlignment.TopLeft));
			_rowCellIndicesLookup.Add(i, iteratingCellIndex - 1);

			iteratingCellIndex += mapper.ColumnsCount;
			diagonalPoints.Add(mapper.GetPoint(mapper.GetAbsoluteIndex(iteratingCellIndex), CellAlignment.TopLeft));
			_columnCellIndicesLookup.Add(i, iteratingCellIndex);
		}

		_rowCellIndicesLookup.Add(mapper.RowsCount - 1, mapper.RowsCount * mapper.ColumnsCount - 1);
		_columnCellIndicesLookup.Add(mapper.RowsCount - 1, mapper.RowsCount * mapper.ColumnsCount - 1);

		var lastCellIndex = mapper.GetAbsoluteIndex(mapper.RowsCount * mapper.ColumnsCount - 1);
		var firstCellIndexInLastRow = mapper.GetAbsoluteIndex((mapper.RowsCount - 1) * mapper.ColumnsCount);

		path.AddPoly(
			[
				.. diagonalPoints,
				mapper.GetPoint(lastCellIndex, CellAlignment.TopRight),
				mapper.GetPoint(lastCellIndex, CellAlignment.BottomRight),
				mapper.GetPoint(firstCellIndexInLastRow, CellAlignment.BottomLeft)
			],
			true
		);
		using var borderPaint = CreateThickLinesPaint(mapper, options);
		canvas.DrawPath(path, borderPaint);
	}

	/// <inheritdoc/>
	protected override void DrawGridLines(PointMapper mapper, SKCanvas canvas, CanvasDrawingOptions options)
	{
		Debug.Assert(_rowCellIndicesLookup is not null);
		Debug.Assert(_columnCellIndicesLookup is not null);

		using var thickLinePaint = CreateThickLinesPaint(mapper, options);
		using var thinLinePaint = CreateThinLinesPaint(mapper, options);

		// Horizontal lines.
		for (var i = 0; i < mapper.RowsCount; i++)
		{
			var a = mapper.GetPoint(mapper.GetAbsoluteIndex(i * mapper.ColumnsCount), CellAlignment.TopLeft);
			var b = mapper.GetPoint(mapper.GetAbsoluteIndex(_rowCellIndicesLookup[i]), CellAlignment.TopLeft);
			canvas.DrawLine(a, b, i % UniformBlockSize == 0 ? thickLinePaint : thinLinePaint);
		}

		// Vertical lines.
		for (var i = 1; i < mapper.ColumnsCount; i++)
		{
			var a = mapper.GetPoint(mapper.GetAbsoluteIndex(_columnCellIndicesLookup[i - 1]), CellAlignment.TopLeft);
			var b = mapper.GetPoint(mapper.GetAbsoluteIndex(mapper.RowsCount * mapper.ColumnsCount + i), CellAlignment.TopLeft);
			canvas.DrawLine(a, b, i % UniformBlockSize == 0 ? thickLinePaint : thinLinePaint);
		}
	}
}
