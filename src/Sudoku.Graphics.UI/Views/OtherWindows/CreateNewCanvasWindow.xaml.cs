namespace Sudoku.Graphics.UI.Views.OtherWindows;

/// <summary>
/// Interaction logic for CreateNewCanvasWindow.xaml.
/// </summary>
[INotifyPropertyChanged]
public partial class CreateNewCanvasWindow : Window
{
	public CreateNewCanvasWindow()
	{
		InitializeComponent();

		DataContext = this;
	}


	public bool IsStandardMode => CreateCanvasMode == CurrentCreateTemplateType.StandardTemplate;

	public bool IsDefaultMode => CreateCanvasMode == CurrentCreateTemplateType.DefaultTemplate;

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
					BorderCornerRadius = ResolveProperty(() => App.UserPreferences.TemplateBorderCornerRadius),
					ThickLineColor = ResolveProperty(() => App.UserPreferences.TemplateThickLineColor),
					ThickLineDashSequence = ResolveProperty(() => App.UserPreferences.TemplateThickLineDashSequence),
					ThickLineWidth = ResolveProperty(() => App.UserPreferences.TemplateThickLineWidth),
					ThinLineColor = ResolveProperty(() => App.UserPreferences.TemplateThinLineColor),
					ThinLineDashSequence = ResolveProperty(() => App.UserPreferences.TemplateThinLineDashSequence),
					ThinLineWidth = ResolveProperty(() => App.UserPreferences.TemplateThinLineWidth)
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
					BorderCornerRadius = ResolveProperty(() => App.UserPreferences.TemplateBorderCornerRadius),
					DrawBordersAsThickLines = DrawBordersAsThickLines,
					ThickLineColor = ResolveProperty(() => App.UserPreferences.TemplateThickLineColor),
					ThickLineDashSequence = ResolveProperty(() => App.UserPreferences.TemplateThickLineDashSequence),
					ThickLineWidth = ResolveProperty(() => App.UserPreferences.TemplateThickLineWidth),
					ThinLineColor = ResolveProperty(() => App.UserPreferences.TemplateThinLineColor),
					ThinLineDashSequence = ResolveProperty(() => App.UserPreferences.TemplateThinLineDashSequence),
					ThinLineWidth = ResolveProperty(() => App.UserPreferences.TemplateThinLineWidth)
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
