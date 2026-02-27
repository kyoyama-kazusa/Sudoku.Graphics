namespace Sudoku.ComponentModel.GridTemplates;

/// <summary>
/// Represents Sujiken (halfdoku) grid template.
/// </summary>
public sealed class SujikenTemplate : Template
{
	/// <summary>
	/// Provides extra lookup dictionary.
	/// </summary>
	private Dictionary<int, Relative>? _rowCellIndicesLookup, _columnCellIndicesLookup;


	/// <summary>
	/// Initializes a <see cref="SujikenTemplate"/> instance.
	/// </summary>
	/// <param name="uniformBlockSize">The uniform size.</param>
	/// <param name="mapper">The mapper instance.</param>
	[JsonConstructor]
	[SetsRequiredMembers]
	public SujikenTemplate(Relative uniformBlockSize, PointMapper mapper)
	{
		ArgumentException.Assert(mapper.RowsCount == mapper.ColumnsCount);

		Mapper = mapper;

		var linesCount = Mapper.RowsCount;
		ArgumentException.Assert(linesCount % uniformBlockSize == 0);

		if (uniformBlockSize * uniformBlockSize != linesCount)
		{
			throw new ArgumentException("Expect a square number.");
		}
		UniformBlockSize = uniformBlockSize;
	}


	/// <summary>
	/// Indicates the number of rows and columns in a block.
	/// </summary>
	public Relative UniformBlockSize { get; init; }


	/// <inheritdoc/>
	[MemberNotNull(nameof(_rowCellIndicesLookup), nameof(_columnCellIndicesLookup))]
	protected override void DrawBorderRectangle(SKCanvas canvas)
	{
		_rowCellIndicesLookup = [];
		_columnCellIndicesLookup = [];

		var path = new SKPath();
		var iteratingCellIndex = 0;
		var iteratingPoint = Mapper.GetPoint(Mapper.GetAbsoluteIndex(iteratingCellIndex), Alignment.TopLeft);
		var diagonalPoints = new List<SKPoint> { iteratingPoint };
		for (var i = 0; i < Mapper.RowsCount - 1; i++)
		{
			iteratingCellIndex++;
			diagonalPoints.Add(Mapper.GetPoint(Mapper.GetAbsoluteIndex(iteratingCellIndex), Alignment.TopLeft));
			_rowCellIndicesLookup.Add(i, iteratingCellIndex - 1);

			iteratingCellIndex += Mapper.ColumnsCount;
			diagonalPoints.Add(Mapper.GetPoint(Mapper.GetAbsoluteIndex(iteratingCellIndex), Alignment.TopLeft));
			_columnCellIndicesLookup.Add(i, iteratingCellIndex);
		}

		_rowCellIndicesLookup.Add(Mapper.RowsCount - 1, Mapper.RowsCount * Mapper.ColumnsCount - 1);
		_columnCellIndicesLookup.Add(Mapper.RowsCount - 1, Mapper.RowsCount * Mapper.ColumnsCount - 1);

		var lastCellIndex = Mapper.GetAbsoluteIndex(Mapper.RowsCount * Mapper.ColumnsCount - 1);
		var firstCellIndexInLastRow = Mapper.GetAbsoluteIndex((Mapper.RowsCount - 1) * Mapper.ColumnsCount);

		path.AddPoly(
			[
				.. diagonalPoints,
				Mapper.GetPoint(lastCellIndex, Alignment.TopRight),
				Mapper.GetPoint(lastCellIndex, Alignment.BottomRight),
				Mapper.GetPoint(firstCellIndexInLastRow, Alignment.BottomLeft)
			],
			true
		);
		using var borderPaint = CreateThickLinesPaint();
		canvas.DrawPath(path, borderPaint);
	}

	/// <inheritdoc/>
	protected override void DrawGridLines(SKCanvas canvas)
	{
		Debug.Assert(_rowCellIndicesLookup is not null);
		Debug.Assert(_columnCellIndicesLookup is not null);

		using var thickLinePaint = CreateThickLinesPaint();
		using var thinLinePaint = CreateThinLinesPaint();

		// Horizontal lines.
		for (var i = 0; i < Mapper.RowsCount; i++)
		{
			var a = Mapper.GetPoint(Mapper.GetAbsoluteIndex(i * Mapper.ColumnsCount), Alignment.TopLeft);
			var b = Mapper.GetPoint(Mapper.GetAbsoluteIndex(_rowCellIndicesLookup[i]), Alignment.TopLeft);
			canvas.DrawLine(a, b, i % UniformBlockSize == 0 ? thickLinePaint : thinLinePaint);
		}

		// Vertical lines.
		for (var i = 1; i < Mapper.ColumnsCount; i++)
		{
			var a = Mapper.GetPoint(Mapper.GetAbsoluteIndex(_columnCellIndicesLookup[i - 1]), Alignment.TopLeft);
			var b = Mapper.GetPoint(Mapper.GetAbsoluteIndex(Mapper.RowsCount * Mapper.ColumnsCount + i), Alignment.TopLeft);
			canvas.DrawLine(a, b, i % UniformBlockSize == 0 ? thickLinePaint : thinLinePaint);
		}
	}
}
