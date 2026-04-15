namespace Sudoku.Graphics.UI.ValueConverters;

public sealed class ThreeStateBooleanToBooleanConverter : IValueConverter
{
	public bool TreatNullAsFalse { get; set; }


	/// <inheritdoc/>
	public object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
		=> value switch
		{
			null when TreatNullAsFalse => false,
			null => throw new NotSupportedException(),
			false => false,
			true => true,
			_ => throw new NotSupportedException()
		};

	/// <inheritdoc/>
	public object? ConvertBack(object? value, Type? targetType, object? parameter, CultureInfo? culture)
		=> value switch
		{
			true => true,
			false => false,
			_ => (bool?)null
		};
}
