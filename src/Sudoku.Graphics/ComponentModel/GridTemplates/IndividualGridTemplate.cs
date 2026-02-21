namespace Sudoku.ComponentModel.GridTemplates;

/// <summary>
/// Represents a grid template that draws thick and thin lines in a normal way.
/// </summary>
public abstract partial class IndividualGridTemplate : GridTemplate
{
	/// <summary>
	/// Indicates thick line width.
	/// </summary>
	public Scale ThickLineWidth { get; init; }

	/// <summary>
	/// Indicates thin line width.
	/// </summary>
	public Scale ThinLineWidth { get; init; }

	/// <summary>
	/// Indicates thick line color.
	/// </summary>
	public SerializableColor ThickLineColor { get; init; }

	/// <summary>
	/// Indicates thin line color.
	/// </summary>
	public SerializableColor ThinLineColor { get; init; }

	/// <summary>
	/// Indicates thick line dash sequence. By default it's an empty sequence, meaning no dash enabled.
	/// </summary>
	public LineDashSequence ThickLineDashSequence { get; init; } = [];

	/// <summary>
	/// Indicates thin line dash sequence. By default it's an empty sequence, meaning no dash enabled.
	/// </summary>
	public LineDashSequence ThinLineDashSequence { get; init; } = [];


	/// <inheritdoc/>
	public sealed override void DrawLines(SKCanvas canvas)
	{
		GuardStatements(canvas);
		DrawBorderRectangle(canvas);
		DrawGridLines(canvas);
	}

	/// <summary>
	/// Enumerates all cell indices in template, only containing in-grid cells.
	/// </summary>
	/// <returns>An <see cref="CellIndexEnumerator"/> instance.</returns>
	public CellIndexEnumerator EnumerateGridCellIndices() => new(this);

	/// <summary>
	/// Provides guard statements. If failed, an exception instance of type <see cref="ArgumentException"/> will be thrown.
	/// </summary>
	/// <param name="canvas">The target canvas to draw.</param>
	/// <exception cref="ArgumentException">Throws when any assertion defined in this method is failed.</exception>
	protected abstract void GuardStatements(SKCanvas canvas);

	/// <summary>
	/// Try to draw border rectangle.
	/// </summary>
	/// <param name="canvas">The target canvas to draw.</param>
	/// <inheritdoc cref="DrawLines(SKCanvas)"/>
	protected abstract void DrawBorderRectangle(SKCanvas canvas);

	/// <summary>
	/// Try to draw grid lines.
	/// </summary>
	/// <param name="canvas">The target canvas to draw.</param>
	/// <inheritdoc cref="DrawLines(SKCanvas)"/>
	protected abstract void DrawGridLines(SKCanvas canvas);

	/// <summary>
	/// Creates an <see cref="SKPaint"/> instance that will be used
	/// in method <see cref="DrawGridLines(SKCanvas)"/>.
	/// </summary>
	/// <remarks>
	/// The return value of this method should be modified with keyword <see langword="using"/>,
	/// in order to keep disposal of this instance.
	/// </remarks>
	/// <seealso cref="DrawGridLines(SKCanvas)"/>.
	protected SKPaint CreateThickLinesPaint()
		=> new()
		{
			Style = SKPaintStyle.Stroke,
			Color = ThickLineColor,
			StrokeWidth = ThickLineWidth.Measure(Mapper.CellSize),
			StrokeCap = SKStrokeCap.Round,
			StrokeJoin = SKStrokeJoin.Round,
			IsAntialias = true,
			PathEffect = ThickLineDashSequence.IsEmpty ? null : ThickLineDashSequence
		};

	/// <summary>
	/// Creates an <see cref="SKPaint"/> instance that will be used
	/// in method <see cref="DrawGridLines(SKCanvas)"/>.
	/// </summary>
	/// <remarks>
	/// <inheritdoc cref="CreateThickLinesPaint()" path="/remarks"/>
	/// </remarks>
	/// <seealso cref="DrawGridLines(SKCanvas)"/>.
	protected SKPaint CreateThinLinesPaint()
		=> new()
		{
			Style = SKPaintStyle.Stroke,
			Color = ThinLineColor,
			StrokeWidth = ThinLineWidth.Measure(Mapper.CellSize),
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
