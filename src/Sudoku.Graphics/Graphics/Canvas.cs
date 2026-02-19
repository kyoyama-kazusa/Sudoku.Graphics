namespace Sudoku.Graphics;

/// <summary>
/// Represents a canvas object that allows you drawing items onto it.
/// </summary>
public sealed partial class Canvas : ICanvas<CanvasDrawingOptions>
{
	/// <summary>
	/// Indicates the backing surface.
	/// </summary>
	private readonly SKSurface _surface;


	/// <summary>
	/// Initializes a <see cref="Canvas"/> instance via the specified values.
	/// </summary>
	/// <param name="templates">The templates to be drawn.</param>
	/// <param name="options">The drawing options.</param>
	public Canvas(GridTemplate[] templates, CanvasDrawingOptions? options = null)
	{
		GlobalTemplateSize = GridTemplateSize.Create(templates);
		_surface = SKSurface.Create(
			new SKSizeI(
				(int)(templates[0].Mapper.CellSize * GlobalTemplateSize.AbsoluteColumnsCount + 2 * templates[0].Mapper.Margin),
				(int)(templates[0].Mapper.CellSize * GlobalTemplateSize.AbsoluteRowsCount + 2 * templates[0].Mapper.Margin)
			)
		);
		Options = options ?? CanvasDrawingOptions.Default;
		Templates = templates;
	}


	/// <inheritdoc/>
	public CanvasDrawingOptions Options { get; }

	/// <summary>
	/// Indicates all templates.
	/// </summary>
	public GridTemplate[] Templates { get; }

	/// <summary>
	/// Indicates the global template size.
	/// </summary>
	public GridTemplateSize GlobalTemplateSize { get; }

	/// <inheritdoc/>
	SKCanvas ICanvas<CanvasDrawingOptions>.BackingCanvas => BackingCanvas;

	/// <inheritdoc cref="ICanvas{TDrawingOptions}.BackingCanvas"/>
	private SKCanvas BackingCanvas => _surface.Canvas;


	public partial void FillBackground();
	public partial void FillBackground(SKColor color);
	public partial void DrawLines();
	public partial void DrawBigText(GridTemplate template, string text, Relative cell, SKColor color);
	public partial void DrawBigText(GridTemplate template, string text, Absolute cell, SKColor color);
	public partial void DrawSmallText(GridTemplate template, string text, Relative cell, int innerPosition, int splitSize, SKColor color);
	public partial void DrawSmallText(GridTemplate template, string text, Absolute cell, int innerPosition, int splitSize, SKColor color);
	public partial void Dispose();
	public partial void Export(string path, CanvasExportingOptions? options = null);
	public partial void Export<TOptions>(string path, TOptions? options) where TOptions : notnull, IOptionsProvider<TOptions>, new();
}
