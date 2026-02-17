namespace Sudoku.Graphics;

/// <summary>
/// Represents a canvas object that allows you drawing items onto it.
/// </summary>
/// <param name="mapper"><inheritdoc cref="Mapper" path="/summary"/></param>
/// <param name="options"><inheritdoc cref="Options" path="/summary"/></param>
public sealed partial class Canvas(PointMapper mapper, CanvasDrawingOptions? options = null) : ICanvas<CanvasDrawingOptions>
{
	/// <summary>
	/// Indicates the backing surface.
	/// </summary>
	private readonly SKSurface _surface = SKSurface.Create(mapper.FullCanvasDrawingSize);


	/// <inheritdoc cref="PointMapper.Margin"/>
	public float Margin => Mapper.Margin;

	/// <inheritdoc cref="PointMapper.CellSize"/>
	public float CellSize => Mapper.CellSize;

	/// <inheritdoc cref="PointMapper.RowsCount"/>
	public Absolute RowsCount => Mapper.RowsCount;

	/// <inheritdoc cref="PointMapper.ColumnsCount"/>
	public Absolute ColumnsCount => Mapper.ColumnsCount;

	/// <inheritdoc cref="PointMapper.Vector"/>
	public DirectionVector Vector => Mapper.Vector;

	/// <summary>
	/// Indicates the mapper instance.
	/// </summary>
	public PointMapper Mapper { get; } = mapper;

	/// <inheritdoc/>
	public CanvasDrawingOptions Options { get; } = options ?? CanvasDrawingOptions.Default;

	/// <inheritdoc/>
	SKCanvas ICanvas<CanvasDrawingOptions>.BackingCanvas => BackingCanvas;

	/// <inheritdoc cref="ICanvas{TDrawingOptions}.BackingCanvas"/>
	private SKCanvas BackingCanvas => _surface.Canvas;


	public partial void FillBackground();
	public partial void FillBackground(SKColor color);
	public partial void DrawLines();
	public partial void DrawBigText(string text, Relative cell, SKColor color);
	public partial void DrawBigText(string text, Absolute cell, SKColor color);
	public partial void DrawSmallText(string text, Relative cell, int innerPosition, int splitSize, SKColor color);
	public partial void DrawSmallText(string text, Absolute cell, int innerPosition, int splitSize, SKColor color);
	public partial void Dispose();
	public partial void Export(string path, CanvasExportingOptions? options = null);
	public partial void Export<TOptions>(string path, TOptions? options) where TOptions : notnull, IOptionsProvider<TOptions>, new();
}
