namespace Sudoku.Graphics;

/// <summary>
/// Represents a canvas object that allows you drawing items onto it.
/// </summary>
public sealed class Canvas : IDisposable
{
	/// <summary>
	/// Indicates the backing surface.
	/// </summary>
	private readonly SKSurface _surface;

	/// <summary>
	/// Indicates whether the object has already been disposed.
	/// </summary>
	private bool _isDisposed;


	/// <summary>
	/// Initializes a <see cref="Canvas"/> instance via the specified values.
	/// </summary>
	/// <param name="templates">The templates to be drawn.</param>
	public Canvas(params GridTemplate[] templates)
	{
		Templates = templates;
		GlobalTemplateSize = GridTemplateSize.Create(templates);
		_surface = SKSurface.Create(
			new SKSizeI(
				(int)(templates[0].Mapper.CellSize * GlobalTemplateSize.AbsoluteColumnsCount + 2 * templates[0].Mapper.Margin),
				(int)(templates[0].Mapper.CellSize * GlobalTemplateSize.AbsoluteRowsCount + 2 * templates[0].Mapper.Margin)
			)
		);
	}


	/// <summary>
	/// Indicates the global template size.
	/// </summary>
	public GridTemplateSize GlobalTemplateSize { get; }

	/// <summary>
	/// Indicates all templates.
	/// </summary>
	public GridTemplate[] Templates { get; }

	/// <summary>
	/// Indicates backing canvas.
	/// </summary>
	internal SKCanvas BackingCanvas => _surface.Canvas;


	/// <summary>
	/// Try to draw the specified item onto the current canvas.
	/// </summary>
	/// <param name="item">The item to draw.</param>
	public void DrawItem(Item item) => item.DrawTo(this);

	/// <summary>
	/// Try to draw the specified list of items onto the current canvas.
	/// </summary>
	/// <param name="items">The items to draw.</param>
	public void DrawItems(params ItemSet items) => items.ForEach(item => item.DrawTo(this));

	/// <inheritdoc/>
	public void Dispose()
	{
		ObjectDisposedException.ThrowIf(_isDisposed, this);

		_surface.Dispose();
		_isDisposed = true;
	}

	/// <summary>
	/// Export the current canvas into target file.
	/// </summary>
	/// <param name="path">The file path. The extension specified will be used as output file format.</param>
	/// <param name="options">The options.</param>
	public void Export(string path, CanvasExportingOptions? options)
	{
		options ??= CanvasExportingOptions.Default;

		var extension = Path.GetExtension(path);
		using var image = _surface.Snapshot();
		using var data = image.Encode(getFormatFromExtension(extension), options.Quality);
		using var stream = new MemoryStream(data.ToArray());
		using var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
		stream.CopyTo(fileStream);


		static SKEncodedImageFormat getFormatFromExtension(string extension)
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
}
