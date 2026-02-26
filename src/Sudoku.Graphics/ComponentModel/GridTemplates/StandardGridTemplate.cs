namespace Sudoku.ComponentModel.GridTemplates;

/// <summary>
/// Represents a standard (rectangular) grid template.
/// </summary>
public sealed class StandardGridTemplate : GridTemplate, IGridTemplate_RoundedRectangleRelatedProperties
{
	/// <summary>
	/// Initializes a <see cref="StandardGridTemplate"/> instance.
	/// </summary>
	/// <param name="rowBlockSize">The block size of rows.</param>
	/// <param name="columnBlockSize">The block size of columns.</param>
	/// <param name="mapper">The mapper instance.</param>
	[JsonConstructor]
	[SetsRequiredMembers]
	public StandardGridTemplate(Relative rowBlockSize, Relative columnBlockSize, PointMapper mapper)
	{
		Mapper = mapper;

		var rowsCount = mapper.RowsCount;
		var columnsCount = mapper.ColumnsCount;

		ArgumentException.Assert(rowsCount % (int)Math.Sqrt(rowsCount) == 0);
		ArgumentException.Assert(columnsCount % (int)Math.Sqrt(columnsCount) == 0);

		RowBlockSize = rowBlockSize;
		ColumnBlockSize = columnBlockSize;
	}


	/// <inheritdoc/>
	public bool IsBorderRoundedRectangle { get; init; } = true;

	/// <inheritdoc/>
	public Scale BorderCornerRadius { get; init; }

	/// <summary>
	/// Indicates the number of rows in a rectangular block.
	/// If you want to assign this field, please assign it ahead of assigning <see cref="GridTemplate.Mapper"/>.
	/// </summary>
	/// <seealso cref="GridTemplate.Mapper"/>
	public Relative RowBlockSize { get; init; }

	/// <summary>
	/// Indicates the number of columns in a rectangular block.
	/// If you want to assign this field, please assign it ahead of assigning <see cref="GridTemplate.Mapper"/>.
	/// </summary>
	/// <seealso cref="GridTemplate.Mapper"/>
	public Relative ColumnBlockSize { get; init; }


	/// <inheritdoc/>
	protected override void DrawBorderRectangle(SKCanvas canvas)
	{
		var path = new SKPath();
		path.AddRoundRect(new(Mapper.GridSize, IsBorderRoundedRectangle ? BorderCornerRadius.Measure(Mapper.CellSize) : 0));
		using var borderPaint = CreateThickLinesPaint();
		canvas.DrawPath(path, borderPaint);
	}

	/// <inheritdoc/>
	protected override void DrawGridLines(SKCanvas canvas)
	{
		using var thickLinePaint = CreateThickLinesPaint();
		using var thinLinePaint = CreateThinLinesPaint();

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
