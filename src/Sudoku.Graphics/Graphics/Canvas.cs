namespace Sudoku.Graphics;

/// <summary>
/// Represents a canvas object that allows you drawing items onto it.
/// </summary>
public sealed class Canvas : IDisposable
{
	/// <summary>
	/// Indicates whether the object has already been disposed.
	/// </summary>
	private bool _isDisposed;


	/// <summary>
	/// Initializes a <see cref="Canvas"/> instance via the specified template.
	/// </summary>
	/// <param name="template">The template to be drawn.</param>
	public Canvas(Template template)
	{
		Template = template;
		GlobalTemplateSize = new()
		{
			RowsCount = template.Mapper.AbsoluteRowsCount,
			ColumnsCount = template.Mapper.AbsoluteColumnsCount
		};
		Surface = SKSurface.Create(
			new SKSizeI(
				(int)(template.Mapper.CellSize * GlobalTemplateSize.AbsoluteColumnsCount + 2 * template.Mapper.Margin),
				(int)(template.Mapper.CellSize * GlobalTemplateSize.AbsoluteRowsCount + 2 * template.Mapper.Margin)
			)
		);
	}


	/// <summary>
	/// Indicates the global template size.
	/// </summary>
	public GridTemplateSize GlobalTemplateSize { get; }

	/// <summary>
	/// Indicates The ordering on rendering items.
	/// </summary>
	public ItemTypeOrdering Ordering { get; init; } = ItemTypeOrdering.Default;

	/// <summary>
	/// Indicates the target template to draw.
	/// </summary>
	public Template Template { get; }

	/// <summary>
	/// Indicates the backing surface.
	/// </summary>
	public SKSurface Surface { get; }

	/// <summary>
	/// Indicates the target mapper.
	/// </summary>
	public PointMapper Mapper => Template.Mapper;

	/// <summary>
	/// Indicates backing canvas.
	/// </summary>
	internal SKCanvas BackingCanvas => Surface.Canvas;


	/// <summary>
	/// Try to draw the specified item onto the current canvas.
	/// </summary>
	/// <param name="item">The item to draw.</param>
	public void DrawItem(Item item) => item.DrawTo(this);

	/// <summary>
	/// Try to draw the specified list of items onto the current canvas.
	/// </summary>
	/// <param name="items">The items to draw.</param>
	public void DrawItems(params ItemSet items)
	{
		var typesSorted = new SortedSet<ItemType>(Comparer<ItemType>.Create((left, right) => Ordering[left] - Ordering[right]));
		foreach (var itemType in items.Types)
		{
			typesSorted.Add(itemType);
		}
		foreach (var type in typesSorted)
		{
			foreach (var item in items[type])
			{
				item.DrawTo(this);
			}
		}
	}

	/// <inheritdoc/>
	public void Dispose()
	{
		ObjectDisposedException.ThrowIf(_isDisposed, this);

		Surface.Dispose();
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
		using var image = Surface.Snapshot();
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


	/// <summary>
	/// Returns a sample canvas.
	/// </summary>
	/// <param name="cellSize">Indicates the cell size.</param>
	/// <param name="margin">The margin.</param>
	/// <returns>The sample canvas.</returns>
	public static Canvas GetSampleCanvas(float cellSize, float margin)
		=> new(new SpecifiedTemplate(new() { CellSize = cellSize, Margin = margin, TemplateSize = new() { RowsCount = 1, ColumnsCount = 1 } }));
}
