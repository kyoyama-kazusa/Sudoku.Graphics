namespace Sudoku.Graphics.UI.ValueConverters;

public sealed class TemplateModeToVisibilityConverter : IValueConverter
{
	/// <inheritdoc/>
	public object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
		=> (value, parameter) is (CreateCanvasMode left, CreateCanvasMode right)
			? left == right ? Visibility.Visible : Visibility.Collapsed
			: throw new NotSupportedException();

	/// <inheritdoc/>
	public object? ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture)
		=> throw new NotImplementedException();
}
