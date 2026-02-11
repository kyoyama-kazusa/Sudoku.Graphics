namespace Sudoku.Graphics;

/// <summary>
/// Represents a canvas object that allows you drawing items onto it.
/// </summary>
/// <param name="mapper"><inheritdoc cref="Mapper" path="/summary"/></param>
/// <param name="drawingOptions"><inheritdoc cref="DrawingOptions" path="/summary"/></param>
/// <param name="exportingOptions"><inheritdoc cref="ExportingOptions" path="/summary"/></param>
public sealed class Canvas(
	PointMapper mapper,
	CanvasDrawingOptions? drawingOptions = null,
	CanvasExportingOptions? exportingOptions = null
) : ICanvas<CanvasDrawingOptions, CanvasExportingOptions>
{
	/// <summary>
	/// Indicates the backing surface.
	/// </summary>
	private readonly SKSurface _surface = SKSurface.Create(mapper.FullCanvasSize);

	/// <summary>
	/// Indicates whether the object has already been disposed.
	/// </summary>
	private bool _isDisposed;


	/// <summary>
	/// Initializes a <see cref="Canvas"/> object via the specified information.
	/// </summary>
	/// <param name="cellSize"><inheritdoc cref="CellSize" path="/summary"/></param>
	/// <param name="margin"><inheritdoc cref="Margin" path="/summary"/></param>
	/// <param name="rowsCount"><inheritdoc cref="RowsCount" path="/summary"/></param>
	/// <param name="columnsCount"><inheritdoc cref="ColumnsCount" path="/summary"/></param>
	/// <param name="vector"><inheritdoc cref="Vector" path="/summary"/></param>
	/// <param name="drawingOptions"><inheritdoc cref="DrawingOptions" path="/summary"/></param>
	/// <param name="exportingOptions"><inheritdoc cref="ExportingOptions" path="/summary"/></param>
	public Canvas(
		float cellSize,
		float margin,
		Absolute rowsCount,
		Absolute columnsCount,
		DirectionVector vector,
		CanvasDrawingOptions? drawingOptions = null,
		CanvasExportingOptions? exportingOptions = null
	) : this(new(cellSize, margin, rowsCount, columnsCount, vector), drawingOptions, exportingOptions)
	{
	}


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
	public CanvasDrawingOptions DrawingOptions { get; } = drawingOptions ?? CanvasDrawingOptions.Default;

	/// <inheritdoc/>
	public CanvasExportingOptions ExportingOptions { get; } = exportingOptions ?? CanvasExportingOptions.Default;

	/// <inheritdoc/>
	SKCanvas ICanvas<CanvasDrawingOptions, CanvasExportingOptions>.BackingCanvas => BackingCanvas;

	/// <inheritdoc cref="ICanvas{TDrawingOptions, TExportingOptions}.BackingCanvas"/>
	private SKCanvas BackingCanvas => _surface.Canvas;


	/// <inheritdoc/>
	public void FillBackground() => FillBackground(DrawingOptions.BackgroundColor);

	/// <inheritdoc/>
	public void FillBackground(SKColor color) => BackingCanvas.Clear(color);

	/// <inheritdoc/>
	public void DrawLines() => DrawingOptions.GridLineTemplate.DrawLines(Mapper, BackingCanvas, DrawingOptions);

	/// <inheritdoc/>
	public void Dispose()
	{
		ObjectDisposedException.ThrowIf(_isDisposed, this);

		_surface.Dispose();
		_isDisposed = true;
	}

	/// <inheritdoc/>
	public void Export(string path)
	{
		var extension = Path.GetExtension(path);
		using var image = _surface.Snapshot();
		using var data = image.Encode(GetFormatFromExtension(extension), ExportingOptions.Quality);
		using var stream = new MemoryStream(data.ToArray());
		using var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
		stream.CopyTo(fileStream);
	}


	/// <summary>
	/// Returns <see cref="SKEncodedImageFormat"/> from extension string.
	/// </summary>
	/// <param name="extension">The file extesnsion.</param>
	/// <returns>The target format.</returns>
	/// <exception cref="NotSupportedException">Throws when the target format is not supported.</exception>
	private static SKEncodedImageFormat GetFormatFromExtension(string extension)
		=> extension switch
		{
			".jpg" => SKEncodedImageFormat.Jpeg,
			".png" => SKEncodedImageFormat.Png,
			".gif" => SKEncodedImageFormat.Gif,
			".bmp" => SKEncodedImageFormat.Bmp,
			".webp" => SKEncodedImageFormat.Webp,
			_ => throw new NotSupportedException()
		};
}
