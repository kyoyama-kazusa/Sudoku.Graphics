namespace Sudoku.Graphics.Primitives;

/// <summary>
/// Represents a canvas object that supports exporting methods.
/// </summary>
public interface ICanvasExport
{
	/// <summary>
	/// Export the current canvas into target file.
	/// </summary>
	/// <typeparam name="TOptions">The type of options.</typeparam>
	/// <param name="path">The file path. The extension specified will be used as output file format.</param>
	/// <param name="options">The options.</param>
	void Export<TOptions>(string path, TOptions? options) where TOptions : notnull, IOptionsProvider<TOptions>, new();
}
