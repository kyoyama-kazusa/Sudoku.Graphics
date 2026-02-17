namespace Sudoku.Graphics.LineTemplates;

/// <summary>
/// Represents aggreated line template. The template can merge multiple individual <see cref="LineTemplate"/> instances to be drawn.
/// </summary>
/// <param name="templates">The templates.</param>
public sealed class AggregatedLineTemplate(params LineTemplate[] templates) : LineTemplate
{
	/// <summary>
	/// Indicates all templates.
	/// </summary>
	public LineTemplate[] Templates { get; } = templates;


	/// <inheritdoc/>
	public override void DrawLines(PointMapper mapper, SKCanvas canvas, CanvasDrawingOptions options)
		=> Array.ForEach(Templates, template => template.DrawLines(mapper, canvas, options));
}
