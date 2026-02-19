namespace Sudoku.Graphics.GridTemplates;

/// <summary>
/// Represents Sujiken (halfdoku) grid template.
/// </summary>
public sealed class SujikenGridTemplate : IndividualGridTemplate
{
	/// <summary>
	/// Provides extra lookup dictionary.
	/// </summary>
	private Dictionary<int, Relative>? _rowCellIndicesLookup, _columnCellIndicesLookup;


	/// <summary>
	/// Initializes a <see cref="SujikenGridTemplate"/> instance via the specified mapper.
	/// </summary>
	/// <param name="mapper">The mapper.</param>
	/// <exception cref="ArgumentException">
	/// Throws when the mapper doesn't represents a grid with standard <c>n * n</c> size.
	/// </exception>
	[SetsRequiredMembers]
	public SujikenGridTemplate(PointMapper mapper) : base(mapper)
	{
		var linesCount = mapper.RowsCount;
		var squareRootOfLinesCount = (int)Math.Sqrt(linesCount);
		UniformBlockSize = squareRootOfLinesCount * squareRootOfLinesCount == linesCount
			? squareRootOfLinesCount
			: throw new ArgumentException(message_InvalidCount(nameof(UniformBlockSize)));


		static string message_InvalidCount(string propertyName)
			=> $"The argument '{propertyName}' isn't a square number when it is uninitialized.";
	}

	/// <summary>
	/// Initializes a <see cref="SujikenGridTemplate"/> instance via the specified mapper and uniform block size.
	/// </summary>
	/// <param name="uniformSize">The uniformed value.</param>
	/// <param name="mapper">The mapper.</param>
	[JsonConstructor]
	[SetsRequiredMembers]
	public SujikenGridTemplate(Relative uniformSize, PointMapper mapper) : this(mapper) => UniformBlockSize = uniformSize;


	/// <summary>
	/// Indicates the number of rows and columns in a block.
	/// </summary>
	public required Relative UniformBlockSize { get; init; }


	/// <inheritdoc/>
	protected override void GuardStatements(SKCanvas canvas, CanvasDrawingOptions options)
	{
		ArgumentException.Assert(Mapper.RowsCount % UniformBlockSize == 0);
		ArgumentException.Assert(Mapper.ColumnsCount % UniformBlockSize == 0);
		ArgumentException.Assert(Mapper.RowsCount == Mapper.ColumnsCount);
	}

	/// <inheritdoc/>
	[MemberNotNull(nameof(_rowCellIndicesLookup), nameof(_columnCellIndicesLookup))]
	protected override void DrawBorderRectangle(SKCanvas canvas, CanvasDrawingOptions options)
	{
		_rowCellIndicesLookup = [];
		_columnCellIndicesLookup = [];

		var path = new SKPath();
		var iteratingCellIndex = 0;
		var iteratingPoint = Mapper.GetPoint(Mapper.GetAbsoluteIndex(iteratingCellIndex), CellAlignment.TopLeft);
		var diagonalPoints = new List<SKPoint> { iteratingPoint };
		for (var i = 0; i < Mapper.RowsCount - 1; i++)
		{
			iteratingCellIndex++;
			diagonalPoints.Add(Mapper.GetPoint(Mapper.GetAbsoluteIndex(iteratingCellIndex), CellAlignment.TopLeft));
			_rowCellIndicesLookup.Add(i, iteratingCellIndex - 1);

			iteratingCellIndex += Mapper.ColumnsCount;
			diagonalPoints.Add(Mapper.GetPoint(Mapper.GetAbsoluteIndex(iteratingCellIndex), CellAlignment.TopLeft));
			_columnCellIndicesLookup.Add(i, iteratingCellIndex);
		}

		_rowCellIndicesLookup.Add(Mapper.RowsCount - 1, Mapper.RowsCount * Mapper.ColumnsCount - 1);
		_columnCellIndicesLookup.Add(Mapper.RowsCount - 1, Mapper.RowsCount * Mapper.ColumnsCount - 1);

		var lastCellIndex = Mapper.GetAbsoluteIndex(Mapper.RowsCount * Mapper.ColumnsCount - 1);
		var firstCellIndexInLastRow = Mapper.GetAbsoluteIndex((Mapper.RowsCount - 1) * Mapper.ColumnsCount);

		path.AddPoly(
			[
				.. diagonalPoints,
				Mapper.GetPoint(lastCellIndex, CellAlignment.TopRight),
				Mapper.GetPoint(lastCellIndex, CellAlignment.BottomRight),
				Mapper.GetPoint(firstCellIndexInLastRow, CellAlignment.BottomLeft)
			],
			true
		);
		using var borderPaint = CreateThickLinesPaint(options);
		canvas.DrawPath(path, borderPaint);
	}

	/// <inheritdoc/>
	protected override void DrawGridLines(SKCanvas canvas, CanvasDrawingOptions options)
	{
		Debug.Assert(_rowCellIndicesLookup is not null);
		Debug.Assert(_columnCellIndicesLookup is not null);

		using var thickLinePaint = CreateThickLinesPaint(options);
		using var thinLinePaint = CreateThinLinesPaint(options);

		// Horizontal lines.
		for (var i = 0; i < Mapper.RowsCount; i++)
		{
			var a = Mapper.GetPoint(Mapper.GetAbsoluteIndex(i * Mapper.ColumnsCount), CellAlignment.TopLeft);
			var b = Mapper.GetPoint(Mapper.GetAbsoluteIndex(_rowCellIndicesLookup[i]), CellAlignment.TopLeft);
			canvas.DrawLine(a, b, i % UniformBlockSize == 0 ? thickLinePaint : thinLinePaint);
		}

		// Vertical lines.
		for (var i = 1; i < Mapper.ColumnsCount; i++)
		{
			var a = Mapper.GetPoint(Mapper.GetAbsoluteIndex(_columnCellIndicesLookup[i - 1]), CellAlignment.TopLeft);
			var b = Mapper.GetPoint(Mapper.GetAbsoluteIndex(Mapper.RowsCount * Mapper.ColumnsCount + i), CellAlignment.TopLeft);
			canvas.DrawLine(a, b, i % UniformBlockSize == 0 ? thickLinePaint : thinLinePaint);
		}
	}
}
