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
	public partial int RowsCount { get; set; } = 9;

	[ObservableProperty]
	public partial int ColumnsCount { get; set; } = 9;

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


	public Template CreateTemplate()
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
						RowsCount = RowsCount,
						ColumnsCount = ColumnsCount,
						Vector = new(VectorLeft, VectorTop, VectorRight, VectorBottom)
					}
				};
				return new StandardTemplate(BlockRowsCount, BlockColumnsCount, mapper)
				{
					IsBorderRoundedRectangle = IsBorderRoundedRectangle,
					BorderCornerRadius = App.UserPreferences.Template_BorderCornerRadius.Resolve(App.UserPreferences),
					ThickLineColor = App.UserPreferences.Template_ThickLineColor.Resolve(App.UserPreferences),
					ThickLineDashSequence = App.UserPreferences.Template_ThickLineDashSequence.Resolve(App.UserPreferences),
					ThickLineWidth = App.UserPreferences.Template_ThickLineWidth.Resolve(App.UserPreferences),
					ThinLineColor = App.UserPreferences.Template_ThinLineColor.Resolve(App.UserPreferences),
					ThinLineDashSequence = App.UserPreferences.Template_ThinLineDashSequence.Resolve(App.UserPreferences),
					ThinLineWidth = App.UserPreferences.Template_ThinLineWidth.Resolve(App.UserPreferences)
				};
			}
			case CurrentCreateTemplateType.DefaultTemplate:
			{
				return new DefaultTemplate
				{
					Mapper = new()
					{
						CellSize = RenderedCellSize,
						Margin = RenderedGridMargin,
						TemplateSize = new()
						{
							RowsCount = RowsCount,
							ColumnsCount = ColumnsCount,
							Vector = new(VectorLeft, VectorTop, VectorRight, VectorBottom)
						}
					},
					IsBorderRoundedRectangle = IsBorderRoundedRectangle,
					BorderCornerRadius = App.UserPreferences.Template_BorderCornerRadius.Resolve(App.UserPreferences),
					DrawBordersAsThickLines = DrawBordersAsThickLines,
					ThickLineColor = App.UserPreferences.Template_ThickLineColor.Resolve(App.UserPreferences),
					ThickLineDashSequence = App.UserPreferences.Template_ThickLineDashSequence.Resolve(App.UserPreferences),
					ThickLineWidth = App.UserPreferences.Template_ThickLineWidth.Resolve(App.UserPreferences),
					ThinLineColor = App.UserPreferences.Template_ThinLineColor.Resolve(App.UserPreferences),
					ThinLineDashSequence = App.UserPreferences.Template_ThinLineDashSequence.Resolve(App.UserPreferences),
					ThinLineWidth = App.UserPreferences.Template_ThinLineWidth.Resolve(App.UserPreferences)
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
