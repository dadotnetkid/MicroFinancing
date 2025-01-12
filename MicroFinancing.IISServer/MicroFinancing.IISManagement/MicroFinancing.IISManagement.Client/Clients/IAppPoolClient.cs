using MicroFinancing.IISManagement.Client.Contracts;
using Refit;

namespace MicroFinancing.IISManagement.Client.Clients;

public interface IAppPoolClient
{
    [Post($"/api/{Endpoints.AppPools.ControllerName}/{Endpoints.AppPools.StartApplicationPool}")]
    Task<IApiResponse> StartApplicationPool([Body] AppPoolPayloadRequest appPoolName);

    [Post($"/api/{Endpoints.AppPools.ControllerName}/{Endpoints.AppPools.StopApplicationPool}")]
    Task<IApiResponse> StopApplicationPool([Body] AppPoolPayloadRequest appPoolName);

    [Post($"/api/{Endpoints.AppPools.ControllerName}/{Endpoints.AppPools.GetApplicationPoolStatus}")]
    Task<IApiResponse<ObjectState>> GetApplicationPoolStatus([Body] AppPoolPayloadRequest appPoolName);
}
public enum ObjectState
{
    Starting,
    Started,
    Stopping,
    Stopped,
    Unknown,
}