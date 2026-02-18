namespace Sudoku.Graphics.LineTemplates;

/// <summary>
/// Represents a standard (rectangular) line template.
/// </summary>
/// <param name="rowBlockSize"><inheritdoc cref="RowBlockSize" path="/summary"/></param>
/// <param name="columnBlockSize"><inheritdoc cref="ColumnBlockSize" path="/summary"/></param>
public sealed class StandardLineTemplate(Relative rowBlockSize, Relative columnBlockSize) : IndividualBlockLineTemplate
{
	/// <summary>
	/// Initializes a <see cref="StandardLineTemplate"/> instance,
	/// keeping <see cref="RowBlockSize"/> and <see cref="ColumnBlockSize"/> uninitialized -
	/// they will be replaced when <see cref="PointMapper"/> instances are given and known.
	/// </summary>
	/// <seealso cref="RowBlockSize"/>
	/// <seealso cref="ColumnBlockSize"/>
	/// <seealso cref="PointMapper"/>
	public StandardLineTemplate() : this(-1, -1)
	{
	}

	/// <summary>
	/// Initializes a <see cref="StandardLineTemplate"/> instance via the specified size as uniformed value.
	/// </summary>
	/// <param name="uniformSize">The uniformed value.</param>
	public StandardLineTemplate(Relative uniformSize) : this(uniformSize, uniformSize)
	{
	}


	/// <summary>
	/// Indicates the number of rows in a rectangular block. The value may be -1 if uninitialized.
	/// </summary>
	public Relative RowBlockSize { get; private set; } = rowBlockSize;

	/// <summary>
	/// Indicates the number of columns in a rectangular block. The value may be -1 if uninitialized.
	/// </summary>
	public Relative ColumnBlockSize { get; private set; } = columnBlockSize;


	/// <inheritdoc/>
	protected override void GuardStatements(PointMapper mapper, SKCanvas canvas, CanvasDrawingOptions options)
	{
		if (RowBlockSize == -1)
		{
			var n = mapper.RowsCount;
			var s = (int)Math.Sqrt(n);
			RowBlockSize = s * s == n ? s : throw new ArgumentException(message_InvalidCount(nameof(RowBlockSize)));
		}
		if (ColumnBlockSize == -1)
		{
			var n = mapper.ColumnsCount;
			var s = (int)Math.Sqrt(n);
			ColumnBlockSize = s * s == n ? s : throw new ArgumentException(message_InvalidCount(nameof(ColumnBlockSize)));
		}

		ArgumentException.Assert(mapper.RowsCount % RowBlockSize == 0);
		ArgumentException.Assert(mapper.ColumnsCount % ColumnBlockSize == 0);


		static string message_InvalidCount(string propertyName)
			=> $"The argument '{propertyName}' isn't a square number when it is uninitialized.";
	}

	/// <inheritdoc/>
	protected override void DrawBorderRectangle(PointMapper mapper, SKCanvas canvas, CanvasDrawingOptions options)
	{
		var path = new SKPath();
		path.AddRoundRect(
			new(
				SKRect.Create(
					mapper.Margin + mapper.CellSize * mapper.Vector.Left,
					mapper.Margin + mapper.CellSize * mapper.Vector.Up,
					mapper.GridDrawingSize.Width,
					mapper.GridDrawingSize.Height
				),
				options.GridBorderRoundedRectangleCornerRadius.Resolve(options).Measure(mapper.CellSize)
			)
		);
		using var borderPaint = CreateThickLinesPaint(mapper, options);
		canvas.DrawPath(path, borderPaint);
	}

	/// <inheritdoc/>
	protected override void DrawGridLines(PointMapper mapper, SKCanvas canvas, CanvasDrawingOptions options)
	{
		using var thickLinePaint = CreateThickLinesPaint(mapper, options);
		using var thinLinePaint = CreateThinLinesPaint(mapper, options);

		// Horizontal lines.
		for (var i = 1; i < mapper.RowsCount; i++)
		{
			var a = mapper.GetPoint(mapper.Vector.Up + i, (Absolute)mapper.Vector.Left, CellAlignment.TopLeft);
			var b = a + new SKPoint(mapper.ColumnsCount * mapper.CellSize, 0);
			canvas.DrawLine(a, b, i % RowBlockSize == 0 ? thickLinePaint : thinLinePaint);
		}

		// Vertical lines.
		for (var i = 1; i < mapper.ColumnsCount; i++)
		{
			var a = mapper.GetPoint((Absolute)mapper.Vector.Up, mapper.Vector.Left + i, CellAlignment.TopLeft);
			var b = a + new SKPoint(0, mapper.RowsCount * mapper.CellSize);
			canvas.DrawLine(a, b, i % ColumnBlockSize == 0 ? thickLinePaint : thinLinePaint);
		}
	}
}
