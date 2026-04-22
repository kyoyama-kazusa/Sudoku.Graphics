namespace Sudoku.Graphics.UI.Views.Controls;

public sealed partial class IntegerBox : TextBox
{
	public static readonly DependencyProperty ValueProperty =
		DependencyProperty.Register(
			nameof(Value),
			typeof(int),
			typeof(IntegerBox),
			new FrameworkPropertyMetadata(
				0,
				FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
				static (d, e) => ((IntegerBox)d).Text = e.NewValue.ToString()
			)
		);

	public static readonly DependencyProperty MinValueProperty =
		DependencyProperty.Register(
			nameof(MinValue),
			typeof(int),
			typeof(IntegerBox),
			new FrameworkPropertyMetadata(int.MinValue, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
		);

	public static readonly DependencyProperty MaxValueProperty =
		DependencyProperty.Register(
			nameof(MaxValue),
			typeof(int),
			typeof(IntegerBox),
			new FrameworkPropertyMetadata(int.MaxValue, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
		);


	public IntegerBox() : base()
	{
		TextChanged += OnTextChanged;

		Text = 0.ToString();
	}


	public int Value
	{
		get => (int)GetValue(ValueProperty);

		set => SetValue(ValueProperty, Math.Clamp(value, MinValue, MaxValue));
	}

	public int MinValue
	{
		get => (int)GetValue(MinValueProperty);

		set => SetValue(MinValueProperty, value);
	}

	public int MaxValue
	{
		get => (int)GetValue(MaxValueProperty);

		set => SetValue(MaxValueProperty, value);
	}


	private void OnTextChanged(object sender, TextChangedEventArgs e)
	{
		if (int.TryParse(Text, out var newValue))
		{
			Value = newValue;
		}
		else
		{
			Text = Value.ToString();
			CaretIndex = Text.Length;
		}
	}
}
