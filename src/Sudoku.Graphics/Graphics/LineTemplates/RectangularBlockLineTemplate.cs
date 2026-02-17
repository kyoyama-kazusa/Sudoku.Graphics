namespace Sudoku.Graphics.LineTemplates;

/// <summary>
/// Represents a line template that draws with a general way because the blocks are rectangular ones to be drawn.
/// </summary>
public abstract class RectangularBlockLineTemplate : LineTemplate
{
	/// <summary>
	/// Indicates whether thick lines will be applied with dash sequence or not.
	/// </summary>
	public bool EnableDashForThickLines { get; set; } = false;

	/// <summary>
	/// Indicates whether thin lines will be applied with dash sequence or not.
	/// </summary>
	public bool EnableDashForThinLines { get; set; } = false;


	/// <inheritdoc/>
	public sealed override void DrawLines(PointMapper mapper, SKCanvas canvas, CanvasDrawingOptions options)
	{
		GuardStatements(mapper, canvas, options);
		DrawBorderRectangle(mapper, canvas, options);
		DrawGridLines(mapper, canvas, options);
	}

	/// <summary>
	/// Provides guard statements. If failed, an exception instance of type <see cref="ArgumentException"/> will be thrown.
	/// </summary>
	/// <exception cref="ArgumentException">Throws when any assertion defined in this method is failed.</exception>
	protected abstract void GuardStatements(PointMapper mapper, SKCanvas canvas, CanvasDrawingOptions options);

	/// <summary>
	/// Try to draw border rectangle.
	/// </summary>
	/// <inheritdoc cref="DrawLines(PointMapper, SKCanvas, CanvasDrawingOptions)"/>
	protected abstract void DrawBorderRectangle(PointMapper mapper, SKCanvas canvas, CanvasDrawingOptions options);

	/// <summary>
	/// Try to draw grid lines.
	/// </summary>
	/// <inheritdoc cref="DrawLines(PointMapper, SKCanvas, CanvasDrawingOptions)"/>
	protected abstract void DrawGridLines(PointMapper mapper, SKCanvas canvas, CanvasDrawingOptions options);

	/// <summary>
	/// Creates an <see cref="SKPaint"/> instance that will be used
	/// in method <see cref="DrawGridLines(PointMapper, SKCanvas, CanvasDrawingOptions)"/>.
	/// </summary>
	/// <remarks>
	/// The return value of this method should be modified with keyword <see langword="using"/>,
	/// in order to keep disposal of this instance.
	/// </remarks>
	/// <seealso cref="DrawGridLines(PointMapper, SKCanvas, CanvasDrawingOptions)"/>.
	protected SKPaint CreateThickLinesPaint(PointMapper mapper, CanvasDrawingOptions options)
		=> new()
		{
			Style = SKPaintStyle.Stroke,
			Color = options.ThickLineColor.Resolve(options),
			StrokeWidth = options.ThickLineWidth.Resolve(options).Measure(mapper.CellSize),
			StrokeCap = SKStrokeCap.Round,
			IsAntialias = true,
			PathEffect = (EnableDashForThickLines, options.ThickLineDashSequence.Resolve(options)) switch
			{
				(true, { IsEmpty: false } sequence) => sequence,
				_ => null
			}
		};

	/// <summary>
	/// Creates an <see cref="SKPaint"/> instance that will be used
	/// in method <see cref="DrawGridLines(PointMapper, SKCanvas, CanvasDrawingOptions)"/>.
	/// </summary>
	/// <remarks>
	/// <inheritdoc cref="CreateThickLinesPaint(PointMapper, CanvasDrawingOptions)" path="/remarks"/>
	/// </remarks>
	/// <seealso cref="DrawGridLines(PointMapper, SKCanvas, CanvasDrawingOptions)"/>.
	protected SKPaint CreateThinLinesPaint(PointMapper mapper, CanvasDrawingOptions options)
		=> new()
		{
			Style = SKPaintStyle.Stroke,
			Color = options.ThinLineColor.Resolve(options),
			StrokeWidth = options.ThinLineWidth.Resolve(options).Measure(mapper.CellSize),
			StrokeCap = SKStrokeCap.Round,
			IsAntialias = true,
			PathEffect = (EnableDashForThinLines, options.ThinLineDashSequence.Resolve(options)) switch
			{
				(true, { IsEmpty: false } sequence) => sequence,
				_ => null
			}
		};
}
