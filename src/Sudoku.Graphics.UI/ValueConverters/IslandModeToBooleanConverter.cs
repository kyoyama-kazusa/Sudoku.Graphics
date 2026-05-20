namespace Sudoku.Graphics.UI.ValueConverters;

/// <summary>
/// Represents a value converter that converts <see cref="IslandConnectorMode"/> value into <see cref="bool"/> result.
/// </summary>
/// <seealso cref="IslandConnectorMode"/>
public sealed class IslandModeToBooleanConverter : IValueConverter
{
	/// <summary>
	/// Indicates whether the value should be reverted.
	/// </summary>
	public bool IsReverted { get; set; }


	/// <inheritdoc/>
	public object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
		=> (value, parameter) is (IslandConnectorMode left, IslandConnectorMode right)
			? IsReverted ? left != right : left == right
			: throw new NotSupportedException();

	/// <inheritdoc/>
	public object? ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture)
		=> throw new NotImplementedException();
}
