using MicroFinancing.Core.Enumeration;

namespace MicroFinancing.DataTransferModel;

public class BranchDto
{
    public BranchEnum.Branch Branch { get; set; }
    public string BranchName { get; set; }
}
