namespace MicroFinancing.DataTransferModel;

public class BaseResultDto<TDto>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public TDto Data { get; set; }

    public static BaseResultDto<TDto> Success(TDto data)
    {
        return new BaseResultDto<TDto>
        {
            IsSuccess = true,
            Data = data
        };
    }
    public static BaseResultDto<TDto> Fail(string message)
    {
        return new BaseResultDto<TDto>
        {
            IsSuccess = false,
            Message = message
        };
    }
}
