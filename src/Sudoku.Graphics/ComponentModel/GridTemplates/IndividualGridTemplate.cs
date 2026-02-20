namespace Sudoku.ComponentModel.GridTemplates;

/// <summary>
/// Represents a grid template that draws thick and thin lines in a normal way.
/// </summary>
public abstract partial class IndividualGridTemplate : GridTemplate
{
	/// <summary>
	/// Indicates thick line dash sequence. By default it's an empty sequence, meaning no dash enabled.
	/// </summary>
	public LineDashSequence ThickLineDashSequence { get; set; } = [];

	/// <summary>
	/// Indicates thin line dash sequence. By default it's an empty sequence, meaning no dash enabled.
	/// </summary>
	public LineDashSequence ThinLineDashSequence { get; set; } = [];


	/// <inheritdoc/>
	public sealed override void DrawLines(SKCanvas canvas, CanvasDrawingOptions options)
	{
		GuardStatements(canvas, options);
		DrawBorderRectangle(canvas, options);
		DrawGridLines(canvas, options);
	}

	/// <summary>
	/// Enumerates all cell indices in template, only containing in-grid cells.
	/// </summary>
	/// <returns>An <see cref="AbsoluteCellIndexEnumerator"/> instance.</returns>
	public AbsoluteCellIndexEnumerator EnumerateGridCellIndices() => new(this);

	/// <summary>
	/// Provides guard statements. If failed, an exception instance of type <see cref="ArgumentException"/> will be thrown.
	/// </summary>
	/// <exception cref="ArgumentException">Throws when any assertion defined in this method is failed.</exception>
	protected abstract void GuardStatements(SKCanvas canvas, CanvasDrawingOptions options);

	/// <summary>
	/// Try to draw border rectangle.
	/// </summary>
	/// <inheritdoc cref="DrawLines(SKCanvas, CanvasDrawingOptions)"/>
	protected abstract void DrawBorderRectangle(SKCanvas canvas, CanvasDrawingOptions options);

	/// <summary>
	/// Try to draw grid lines.
	/// </summary>
	/// <inheritdoc cref="DrawLines(SKCanvas, CanvasDrawingOptions)"/>
	protected abstract void DrawGridLines(SKCanvas canvas, CanvasDrawingOptions options);

	/// <summary>
	/// Creates an <see cref="SKPaint"/> instance that will be used
	/// in method <see cref="DrawGridLines(SKCanvas, CanvasDrawingOptions)"/>.
	/// </summary>
	/// <remarks>
	/// The return value of this method should be modified with keyword <see langword="using"/>,
	/// in order to keep disposal of this instance.
	/// </remarks>
	/// <seealso cref="DrawGridLines(SKCanvas, CanvasDrawingOptions)"/>.
	protected SKPaint CreateThickLinesPaint(CanvasDrawingOptions options)
		=> new()
		{
			Style = SKPaintStyle.Stroke,
			Color = options.ThickLineColor.Resolve(options),
			StrokeWidth = options.ThickLineWidth.Resolve(options).Measure(Mapper.CellSize),
			StrokeCap = SKStrokeCap.Round,
			StrokeJoin = SKStrokeJoin.Round,
			IsAntialias = true,
			PathEffect = ThickLineDashSequence.IsEmpty ? null : ThickLineDashSequence
		};

	/// <summary>
	/// Creates an <see cref="SKPaint"/> instance that will be used
	/// in method <see cref="DrawGridLines(SKCanvas, CanvasDrawingOptions)"/>.
	/// </summary>
	/// <remarks>
	/// <inheritdoc cref="CreateThickLinesPaint(CanvasDrawingOptions)" path="/remarks"/>
	/// </remarks>
	/// <seealso cref="DrawGridLines(SKCanvas, CanvasDrawingOptions)"/>.
	protected SKPaint CreateThinLinesPaint(CanvasDrawingOptions options)
		=> new()
		{
			Style = SKPaintStyle.Stroke,
			Color = options.ThinLineColor.Resolve(options),
			StrokeWidth = options.ThinLineWidth.Resolve(options).Measure(Mapper.CellSize),
			StrokeCap = SKStrokeCap.Round,
			StrokeJoin = SKStrokeJoin.Round,
			IsAntialias = true,
			PathEffect = ThinLineDashSequence.IsEmpty ? null : ThinLineDashSequence
		};


	/// <summary>
	/// Project cell index from source template to target template, aligning as top-left side of the canvas.
	/// </summary>
	/// <typeparam name="TTemplate">The type of template.</typeparam>
	/// <param name="source">The source template.</param>
	/// <param name="target">The target template.</param>
	/// <param name="sourceCellIndex">The source cell absolute index.</param>
	/// <param name="targetCellIndex">The target cell absolute index.</param>
	/// <returns>A <see cref="bool"/> result indicating whether the projection operation is successful.</returns>
	public static bool TryProjectCellIndex<TTemplate>(
		TTemplate source,
		TTemplate target,
		Absolute sourceCellIndex,
		out Absolute targetCellIndex
	)
		where TTemplate : IndividualGridTemplate
	{
		if (sourceCellIndex < 0 || sourceCellIndex >= source.Mapper.AbsoluteCellsCount)
		{
			goto ReturnFalse;
		}

		var columnsCount = source.Mapper.AbsoluteColumnsCount;
		var rowIndex = sourceCellIndex / columnsCount;
		var columnIndex = sourceCellIndex % columnsCount;

		if (rowIndex < target.Mapper.Vector.Up
			|| rowIndex >= target.Mapper.Vector.Up + target.Mapper.RowsCount
			|| columnIndex < target.Mapper.Vector.Left
			|| columnIndex >= target.Mapper.Vector.Left + target.Mapper.ColumnsCount)
		{
			goto ReturnFalse;
		}

		var targetCellIndexCalculated = rowIndex * target.Mapper.AbsoluteColumnsCount + columnIndex;
		if (targetCellIndexCalculated < 0 || targetCellIndexCalculated >= target.Mapper.AbsoluteCellsCount)
		{
			goto ReturnFalse;
		}

		targetCellIndex = targetCellIndexCalculated;
		return true;

	ReturnFalse:
		targetCellIndex = -1;
		return false;
	}

	/// <summary>
	/// Project cell index from source template to target template, aligning as top-left side of the canvas.
	/// </summary>
	/// <typeparam name="TTemplate">The type of template.</typeparam>
	/// <param name="source">The source template.</param>
	/// <param name="target">The target template.</param>
	/// <param name="sourceCellIndex">The source cell absolute index.</param>
	/// <returns>The target cell absolute index.</returns>
	/// <exception cref="ArgumentException">Throws when failed to project.</exception>
	public static Absolute ProjectCellIndex<TTemplate>(TTemplate source, TTemplate target, Absolute sourceCellIndex)
		where TTemplate : IndividualGridTemplate
		=> TryProjectCellIndex(source, target, sourceCellIndex, out var targetCellIndex)
			? targetCellIndex
			: throw new ArgumentException("Cannot project cell index because source cell index is invalid.", nameof(sourceCellIndex));

	/// <summary>
	/// Gets intersection of 2 templates.
	/// </summary>
	/// <typeparam name="TTemplate">The type of template.</typeparam>
	/// <param name="template1">The template 1.</param>
	/// <param name="template2">The template 2.</param>
	/// <returns>
	/// The intersection cells, representing with <see cref="Absolute"/> instances.
	/// Due to design of absolute cell indices, the return value will be referenced from <paramref name="template1"/>.
	/// </returns>
	/// <seealso cref="Absolute"/>
	public static Absolute[] GetIntersectionCellIndices<TTemplate>(TTemplate template1, TTemplate template2)
		where TTemplate : IndividualGridTemplate
	{
		var result = new HashSet<Absolute>();
		foreach (var index in template1.EnumerateGridCellIndices())
		{
			if (TryProjectCellIndex(template1, template2, index, out var targetIndex)
				&& template2.Mapper.GetRelativeIndex(targetIndex) is var temp
				&& temp >= 0 && temp < template2.Mapper.CellsCount)
			{
				result.Add(index);
			}
		}
		return [.. result];
	}
}
