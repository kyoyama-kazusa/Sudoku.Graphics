namespace Sudoku.ComponentModel.GridTemplates;

/// <summary>
/// Represents a grid template that draws thick and thin lines in a normal way.
/// </summary>
/// <param name="mapper"><inheritdoc cref="GridTemplate(PointMapper)" path="/param[@name='mapper']"/></param>
public abstract class IndividualGridTemplate(PointMapper mapper) : GridTemplate(mapper)
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
}
