namespace Sudoku.Graphics;

public partial class Canvas
{
	/// <inheritdoc cref="ICanvasExport.Export{TOptions}(string, TOptions)"/>
	public partial void Export(string path, CanvasExportingOptions? options)
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

	/// <inheritdoc/>
	public partial void Export<TOptions>(string path, TOptions? options) where TOptions : notnull, IOptionsProvider<TOptions>, new()
		=> Export(path, options as CanvasExportingOptions);
}
