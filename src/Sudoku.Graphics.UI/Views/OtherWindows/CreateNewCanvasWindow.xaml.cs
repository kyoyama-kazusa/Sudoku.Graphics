namespace Sudoku.Graphics.UI.Views.OtherWindows;

/// <summary>
/// Interaction logic for CreateNewCanvasWindow.xaml.
/// </summary>
[INotifyPropertyChanged]
public partial class CreateNewCanvasWindow : Window
{
	/// <summary>
	/// Initializes a <see cref="CreateNewCanvasWindow"/> instance.
	/// </summary>
	public CreateNewCanvasWindow()
	{
		InitializeComponent();

		DataContext = this;
	}


	/// <summary>
	/// Indicates whether the current mode is standard.
	/// </summary>
	public bool IsStandardMode => CreateCanvasMode == CurrentCreateTemplateType.StandardTemplate;

	/// <summary>
	/// Indicates whether the current mode is default.
	/// </summary>
	public bool IsDefaultMode => CreateCanvasMode == CurrentCreateTemplateType.DefaultTemplate;

	/// <summary>
	/// Indicates whether the current mode is empty.
	/// </summary>
	public bool IsEmptyMode => CreateCanvasMode == CurrentCreateTemplateType.EmptyTemplate;

	[ObservableProperty]
	public partial bool IsBorderRoundedRectangle { get; set; } = false;

	[ObservableProperty]
	public partial bool DrawBordersAsThickLines { get; set; } = true;

	[ObservableProperty]
	public partial int RenderedCellSize { get; set; } = 120;

	[ObservableProperty]
	public partial int RenderedGridMargin { get; set; } = 15;

	[ObservableProperty]
	public partial int RowsAndColumnsCount { get; set; } = 9;

	[ObservableProperty]
	public partial int BlockRowsCount { get; set; } = 3;

	[ObservableProperty]
	public partial int BlockColumnsCount { get; set; } = 3;

	[ObservableProperty]
	public partial int VectorTop { get; set; } = 0;

	[ObservableProperty]
	public partial int VectorBottom { get; set; } = 0;

	[ObservableProperty]
	public partial int VectorLeft { get; set; } = 0;

	[ObservableProperty]
	public partial int VectorRight { get; set; } = 0;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(IsStandardMode))]
	[NotifyPropertyChangedFor(nameof(IsDefaultMode))]
	[NotifyPropertyChangedFor(nameof(IsEmptyMode))]
	public partial CurrentCreateTemplateType CreateCanvasMode { get; set; } = CurrentCreateTemplateType.StandardTemplate;


	/// <summary>
	/// Creates a template via the current-configured values.
	/// </summary>
	/// <param name="grid">The grid created.</param>
	/// <returns>The template created.</returns>
	/// <exception cref="NotSupportedException">Throws when the current mode is not supported.</exception>
	public Template CreateTemplate(out SudokuGrid grid)
	{
		switch (CreateCanvasMode)
		{
			case CurrentCreateTemplateType.StandardTemplate:
			{
				var mapper = new PointMapper
				{
					CellSize = RenderedCellSize,
					Margin = RenderedGridMargin,
					TemplateSize = new()
					{
						RowsCount = RowsAndColumnsCount,
						ColumnsCount = RowsAndColumnsCount,
						Vector = new(VectorLeft, VectorTop, VectorRight, VectorBottom)
					}
				};

				grid = new(mapper.AbsoluteRowsCount, mapper.AbsoluteColumnsCount, mapper.RowsCount);
				return new StandardTemplate(BlockRowsCount, BlockColumnsCount, mapper)
				{
					IsBorderRoundedRectangle = IsBorderRoundedRectangle,
					BorderCornerRadius = ResolveProperty(() => App.UserPreferences.TemplateBorderCornerRadiusScale),
					ThickLineColor = ResolveProperty(() => App.UserPreferences.TemplateThickLineColor),
					ThickLineDashSequence = ResolveProperty(() => App.UserPreferences.TemplateThickLineDashSequence),
					ThickLineWidth = ResolveProperty(() => App.UserPreferences.TemplateThickLineWidthScale),
					ThinLineColor = ResolveProperty(() => App.UserPreferences.TemplateThinLineColor),
					ThinLineDashSequence = ResolveProperty(() => App.UserPreferences.TemplateThinLineDashSequence),
					ThinLineWidth = ResolveProperty(() => App.UserPreferences.TemplateThinLineWidthScale)
				};
			}
			case CurrentCreateTemplateType.DefaultTemplate:
			{
				var mapper = new PointMapper
				{
					CellSize = RenderedCellSize,
					Margin = RenderedGridMargin,
					TemplateSize = new()
					{
						RowsCount = RowsAndColumnsCount,
						ColumnsCount = RowsAndColumnsCount,
						Vector = new(VectorLeft, VectorTop, VectorRight, VectorBottom)
					}
				};
				grid = new(mapper.AbsoluteRowsCount, mapper.AbsoluteColumnsCount, RowsAndColumnsCount);
				return new DefaultTemplate
				{
					Mapper = mapper,
					IsBorderRoundedRectangle = IsBorderRoundedRectangle,
					BorderCornerRadius = ResolveProperty(() => App.UserPreferences.TemplateBorderCornerRadiusScale),
					DrawBordersAsThickLines = DrawBordersAsThickLines,
					ThickLineColor = ResolveProperty(() => App.UserPreferences.TemplateThickLineColor),
					ThickLineDashSequence = ResolveProperty(() => App.UserPreferences.TemplateThickLineDashSequence),
					ThickLineWidth = ResolveProperty(() => App.UserPreferences.TemplateThickLineWidthScale),
					ThinLineColor = ResolveProperty(() => App.UserPreferences.TemplateThinLineColor),
					ThinLineDashSequence = ResolveProperty(() => App.UserPreferences.TemplateThinLineDashSequence),
					ThinLineWidth = ResolveProperty(() => App.UserPreferences.TemplateThinLineWidthScale)
				};
			}
			case CurrentCreateTemplateType.EmptyTemplate:
			{
				var mapper = new PointMapper
				{
					CellSize = RenderedCellSize,
					Margin = RenderedGridMargin,
					TemplateSize = new() { RowsCount = RowsAndColumnsCount, ColumnsCount = RowsAndColumnsCount }
				};
				grid = new(mapper.AbsoluteRowsCount, mapper.AbsoluteColumnsCount, RowsAndColumnsCount);
				return new SpecifiedTemplate(mapper);
			}
			default:
			{
				throw new NotSupportedException();
			}
		}
	}

	private void CreateButton_Click(object sender, RoutedEventArgs e)
	{
		DialogResult = true;
		Close();
	}

	private void CancelButton_Click(object sender, RoutedEventArgs e)
	{
		DialogResult = false;
		Close();
	}

	private void StandardTemplateRadioButton_Checked(object sender, RoutedEventArgs e)
		=> CreateCanvasMode = CurrentCreateTemplateType.StandardTemplate;

	private void DefaultTemplateRadioButton_Checked(object sender, RoutedEventArgs e)
		=> CreateCanvasMode = CurrentCreateTemplateType.DefaultTemplate;

	private void EmptyTemplateRadioButton_Checked(object sender, RoutedEventArgs e)
		=> CreateCanvasMode = CurrentCreateTemplateType.EmptyTemplate;
}
