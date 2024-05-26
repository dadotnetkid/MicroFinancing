using MicroFinancing.DataTransferModel;
using Syncfusion.Blazor.Grids;

namespace MicroFinancing.Pages.Maintenance.Terms;

public partial class Index
{
    public SfGrid<TermDto> TermGrid { get; set; }
    public AddEditTerm AddEditTermRef { get; set; }

    private void AddTerm()
    {
        AddEditTermRef.Show();
    }
}