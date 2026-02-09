namespace Sudoku.Graphics;

/// <summary>
/// Represents a canvas object that allows you drawing items onto it.
/// </summary>
/// <param name="mapper"><inheritdoc cref="Mapper" path="/summary"/></param>
public sealed class Canvas(PointMapper mapper) : ICanavs
{
	/// <summary>
	/// Indicates the backing surface.
	/// </summary>
	private readonly SKSurface _surface = SKSurface.Create(new SKImageInfo(mapper.FullSizeInteger.Width, mapper.FullSizeInteger.Height));

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
	public Canvas(float cellSize, float margin, int rowsCount, int columnsCount) :
		this(new(cellSize, margin, rowsCount, columnsCount))
	{
	}


	/// <inheritdoc cref="PointMapper.Margin"/>
	public float Margin => Mapper.Margin;

	/// <inheritdoc cref="PointMapper.CellSize"/>
	public float CellSize => Mapper.CellSize;

	/// <inheritdoc cref="PointMapper.RowsCount"/>
	public int RowsCount => Mapper.RowsCount;

	/// <inheritdoc cref="PointMapper.ColumnsCount"/>
	public int ColumnsCount => Mapper.ColumnsCount;

	/// <summary>
	/// Indicates reserved number of cells reserved from upside of the grid.
	/// </summary>
	public int ReservedUpCellsCount { get; init; }

	/// <summary>
	/// Indicates reserved number of cells reserved from downside of the grid.
	/// </summary>
	public int ReservedDownCellsCount { get; init; }

	/// <summary>
	/// Indicates reserved number of cells reserved from leftside of the grid.
	/// </summary>
	public int ReservedLeftCellsCount { get; init; }

	/// <summary>
	/// Indicates reserved number of cells reserved from rightside of the grid.
	/// </summary>
	public int ReservedRightCellsCount { get; init; }

	/// <summary>
	/// Indicates the mapper instance.
	/// </summary>
	public PointMapper Mapper { get; } = mapper;

	/// <summary>
	/// Indicates the backing canvas.
	/// </summary>
	private SKCanvas BackingCanvas => _surface.Canvas;


	/// <inheritdoc/>
	public void FillBackground(SKColor color) => BackingCanvas.Clear(color);

	/// <inheritdoc/>
	public void FillBackground(CanvasDrawingOptions? options = null)
	{
		options ??= CanvasDrawingOptions.Default;
		BackingCanvas.Clear(options.BackgroundColor);
	}

	/// <inheritdoc/>
	public void Dispose()
	{
		ObjectDisposedException.ThrowIf(_isDisposed, this);

		_surface.Dispose();
		_isDisposed = true;
	}

	/// <inheritdoc/>
	public void Export(string path, CanvasExportingOptions? options = null)
	{
		options ??= CanvasExportingOptions.Default;

		var extension = Path.GetExtension(path);
		using var image = _surface.Snapshot();
		using var data = image.Encode(GetFormatFromExtension(extension), options.Quality);
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
