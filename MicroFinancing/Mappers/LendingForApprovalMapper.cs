using AutoMapper;

using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;

namespace MicroFinancing.Mappers;

public class LendingForApprovalMapper : Profile
{

    public LendingForApprovalMapper()
    {
        CreateMapForApprovalGrid();
        CreateMapCreateNewLoan();

        CreateMapForLendingRelease();
    }

    private void CreateMapForLendingRelease()
    {
        CreateMap<Lending, LendingForApproval>().ReverseMap();
    }

    private void CreateMapCreateNewLoan()
    {
        CreateMap<CreateLendingForApprovalDTM, LendingForApproval>().ReverseMap();
    }

    private void CreateMapForApprovalGrid()
    {
        CreateMap<LendingForApproval, LendingForApprovalGridDTM>()
            .ForMember(dst => dst.AvailableAmountToRelease, opt => opt.MapFrom(
                                                                          src => (src.Amount - src.PreviousBalance) - (src.DailyDueAmount) - (src.Amount * 0.00125M)))

            .ForMember(dst => dst.Amount
                       , opt => opt.MapFrom(src => src.Amount + src.ItemAmount))
            .ForMember(dst => dst.CustomerName
                       , opt => opt.MapFrom(src => $"{src.Customers.FirstName} {src.Customers.LastName}"))
            .ForMember(dst => dst.Collector
                       , opt => opt.MapFrom(src => src.CollectorUser.FirstName + " " + src.CollectorUser.LastName));
    }
}
