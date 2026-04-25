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
					BorderCornerRadius = ResolveProperty(() => App.UserPreferences.Template_BorderCornerRadius),
					ThickLineColor = ResolveProperty(() => App.UserPreferences.Template_ThickLineColor),
					ThickLineDashSequence = ResolveProperty(() => App.UserPreferences.Template_ThickLineDashSequence),
					ThickLineWidth = ResolveProperty(() => App.UserPreferences.Template_ThickLineWidth),
					ThinLineColor = ResolveProperty(() => App.UserPreferences.Template_ThinLineColor),
					ThinLineDashSequence = ResolveProperty(() => App.UserPreferences.Template_ThinLineDashSequence),
					ThinLineWidth = ResolveProperty(() => App.UserPreferences.Template_ThinLineWidth)
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
					BorderCornerRadius = ResolveProperty(() => App.UserPreferences.Template_BorderCornerRadius),
					DrawBordersAsThickLines = DrawBordersAsThickLines,
					ThickLineColor = ResolveProperty(() => App.UserPreferences.Template_ThickLineColor),
					ThickLineDashSequence = ResolveProperty(() => App.UserPreferences.Template_ThickLineDashSequence),
					ThickLineWidth = ResolveProperty(() => App.UserPreferences.Template_ThickLineWidth),
					ThinLineColor = ResolveProperty(() => App.UserPreferences.Template_ThinLineColor),
					ThinLineDashSequence = ResolveProperty(() => App.UserPreferences.Template_ThinLineDashSequence),
					ThinLineWidth = ResolveProperty(() => App.UserPreferences.Template_ThinLineWidth)
				};
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
}
