namespace Sudoku.ComponentModel.GridTemplates;

/// <summary>
/// Represents a standard (rectangular) grid template.
/// </summary>
public sealed class StandardGridTemplate : IndividualGridTemplate, IRoundRectangleCornerGridTemplate
{
	/// <inheritdoc/>
	public override required PointMapper Mapper
	{
		get;

		init
		{
			field = value;

			if (RowBlockSize != 0 && ColumnBlockSize != 0)
			{
				return;
			}

			var rowsCount = value.RowsCount;
			var squareRootOfRowsCount = (int)Math.Sqrt(rowsCount);
			RowBlockSize = squareRootOfRowsCount * squareRootOfRowsCount == rowsCount
				? squareRootOfRowsCount
				: throw new ArgumentException(message_InvalidCount(nameof(RowBlockSize)));

			var columnsCount = value.ColumnsCount;
			var squareRootOfColumnsCount = (int)Math.Sqrt(columnsCount);
			ColumnBlockSize = squareRootOfColumnsCount * squareRootOfColumnsCount == columnsCount
				? squareRootOfColumnsCount
				: throw new ArgumentException(message_InvalidCount(nameof(ColumnBlockSize)));

			ArgumentException.Assert(rowsCount % squareRootOfRowsCount == 0);
			ArgumentException.Assert(columnsCount % squareRootOfColumnsCount == 0);


			static string message_InvalidCount(string propertyName)
				=> $"The argument '{propertyName}' isn't a square number when it is uninitialized.";
		}
	}


	/// <inheritdoc/>
	public bool IsBorderRoundedRectangle { get; init; } = true;

	/// <summary>
	/// Indicates the number of rows in a rectangular block.
	/// If you want to assign this field, please assign it ahead of assigning <see cref="Mapper"/>.
	/// </summary>
	/// <seealso cref="Mapper"/>
	public Relative RowBlockSize { get; init; }

	/// <summary>
	/// Indicates the number of columns in a rectangular block.
	/// If you want to assign this field, please assign it ahead of assigning <see cref="Mapper"/>.
	/// </summary>
	/// <seealso cref="Mapper"/>
	public Relative ColumnBlockSize { get; init; }


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
				Mapper.GridSize,
				IsBorderRoundedRectangle
					? options.GridBorderRoundedRectangleCornerRadius.Resolve(options).Measure(Mapper.CellSize)
					: 0
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
			var a = Mapper.GetPoint(Mapper.Vector.Up + i, (Absolute)Mapper.Vector.Left, Alignment.TopLeft);
			var b = a + new SKPoint(Mapper.ColumnsCount * Mapper.CellSize, 0);
			canvas.DrawLine(a, b, i % RowBlockSize == 0 ? thickLinePaint : thinLinePaint);
		}

		// Vertical lines.
		for (var i = 1; i < Mapper.ColumnsCount; i++)
		{
			var a = Mapper.GetPoint((Absolute)Mapper.Vector.Up, Mapper.Vector.Left + i, Alignment.TopLeft);
			var b = a + new SKPoint(0, Mapper.RowsCount * Mapper.CellSize);
			canvas.DrawLine(a, b, i % ColumnBlockSize == 0 ? thickLinePaint : thinLinePaint);
		}
	}
}
