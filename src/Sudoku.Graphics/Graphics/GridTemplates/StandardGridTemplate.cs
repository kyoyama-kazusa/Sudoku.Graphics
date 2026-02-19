namespace Sudoku.Graphics.GridTemplates;

/// <summary>
/// Represents a standard (rectangular) grid template.
/// </summary>
public sealed class StandardGridTemplate : IndividualGridTemplate
{
	/// <summary>
	/// Initializes a <see cref="PointMapper"/> instance via the specified mapper.
	/// </summary>
	/// <param name="mapper">The mapper.</param>
	/// <exception cref="ArgumentException">Throws when the rows and columns cannot be divisable by template size.</exception>
	[SetsRequiredMembers]
	public StandardGridTemplate(PointMapper mapper) : base(mapper)
	{
		var rowsCount = mapper.RowsCount;
		var squareRootOfRowsCount = (int)Math.Sqrt(rowsCount);
		RowBlockSize = squareRootOfRowsCount * squareRootOfRowsCount == rowsCount
			? squareRootOfRowsCount
			: throw new ArgumentException(message_InvalidCount(nameof(RowBlockSize)));

		var columnsCount = mapper.ColumnsCount;
		var squareRootOfColumnsCount = (int)Math.Sqrt(columnsCount);
		ColumnBlockSize = squareRootOfColumnsCount * squareRootOfColumnsCount == columnsCount
			? squareRootOfColumnsCount
			: throw new ArgumentException(message_InvalidCount(nameof(ColumnBlockSize)));

		ArgumentException.Assert(rowsCount % squareRootOfRowsCount == 0);
		ArgumentException.Assert(columnsCount % squareRootOfColumnsCount == 0);


		static string message_InvalidCount(string propertyName)
			=> $"The argument '{propertyName}' isn't a square number when it is uninitialized.";
	}

	/// <summary>
	/// Initializes a <see cref="StandardGridTemplate"/> instance via the specified number of rows and columns as block size,
	/// and the point mapper instance.
	/// </summary>
	/// <param name="rowBlockSize"><inheritdoc cref="RowBlockSize" path="/summary"/></param>
	/// <param name="columnBlockSize"><inheritdoc cref="ColumnBlockSize" path="/summary"/></param>
	/// <param name="mapper">The mapper.</param>
	[JsonConstructor]
	[SetsRequiredMembers]
	public StandardGridTemplate(Relative rowBlockSize, Relative columnBlockSize, PointMapper mapper) : base(mapper)
	{
		RowBlockSize = rowBlockSize;
		ColumnBlockSize = columnBlockSize;
	}

	/// <summary>
	/// Initializes a <see cref="StandardGridTemplate"/> instance via the specified size as uniformed value.
	/// </summary>
	/// <param name="uniformSize">The uniformed value.</param>
	/// <param name="mapper">The mapper.</param>
	[SetsRequiredMembers]
	public StandardGridTemplate(Relative uniformSize, PointMapper mapper) : this(uniformSize, uniformSize, mapper)
	{
	}


	/// <summary>
	/// Indicates the number of rows in a rectangular block.
	/// </summary>
	public required Relative RowBlockSize { get; init; }

	/// <summary>
	/// Indicates the number of columns in a rectangular block.
	/// </summary>
	public required Relative ColumnBlockSize { get; init; }


	/// <inheritdoc/>
	protected override void GuardStatements(SKCanvas canvas, CanvasDrawingOptions options)
	{
	}

	/// <inheritdoc/>
	protected override void DrawBorderRectangle(SKCanvas canvas, CanvasDrawingOptions options)
	{
		var path = new SKPath();
		path.AddRoundRect(
			new(
				SKRect.Create(
					Mapper.Margin + Mapper.CellSize * Mapper.Vector.Left,
					Mapper.Margin + Mapper.CellSize * Mapper.Vector.Up,
					Mapper.GridDrawingSize.Width,
					Mapper.GridDrawingSize.Height
				),
				options.GridBorderRoundedRectangleCornerRadius.Resolve(options).Measure(Mapper.CellSize)
			)
		);
		using var borderPaint = CreateThickLinesPaint(options);
		canvas.DrawPath(path, borderPaint);
	}

	/// <inheritdoc/>
	protected override void DrawGridLines(SKCanvas canvas, CanvasDrawingOptions options)
	{
		using var thickLinePaint = CreateThickLinesPaint(options);
		using var thinLinePaint = CreateThinLinesPaint(options);

		// Horizontal lines.
		for (var i = 1; i < Mapper.RowsCount; i++)
		{
			var a = Mapper.GetPoint(Mapper.Vector.Up + i, (Absolute)Mapper.Vector.Left, CellAlignment.TopLeft);
			var b = a + new SKPoint(Mapper.ColumnsCount * Mapper.CellSize, 0);
			canvas.DrawLine(a, b, i % RowBlockSize == 0 ? thickLinePaint : thinLinePaint);
		}

		// Vertical lines.
		for (var i = 1; i < Mapper.ColumnsCount; i++)
		{
			var a = Mapper.GetPoint((Absolute)Mapper.Vector.Up, Mapper.Vector.Left + i, CellAlignment.TopLeft);
			var b = a + new SKPoint(0, Mapper.RowsCount * Mapper.CellSize);
			canvas.DrawLine(a, b, i % ColumnBlockSize == 0 ? thickLinePaint : thinLinePaint);
		}
	}
}
