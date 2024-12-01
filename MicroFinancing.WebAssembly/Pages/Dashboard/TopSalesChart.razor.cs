using System.Runtime.InteropServices;
using MicroFinancing.Core.Common;
using MicroFinancing.Core.Enumeration;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace MicroFinancing.WebAssembly.Pages.Dashboard;

public partial class TopSalesChart
{
    [Inject] IJSRuntime JSRuntime { get; set; }
    [Inject] ILogger<TopSalesChart> logger { get; set; }
    [Parameter] public DateTime? DateFrom { get; set; }
    [Parameter] public DateTime? DateTo { get; set; }
    [Inject] private IDashboardClient DashboardClient { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && (DateFrom is not null && DateTo is not null))
        {
            var renderChartResult =
                await DashboardClient.GetRenderChartAsync(DateFrom.GetValueOrDefault(), DateTo.GetValueOrDefault());

            var renderChart = renderChartResult.Data;

            if (renderChart is null || !renderChart.Any())
            {
                return;
            }

            await JSRuntime.InvokeVoidAsync("renderChart",
                new
                {
                    chartId = "myChart",
                    data = new
                    {
                        type = "bar",
                        data = new
                        {
                            labels = new[] { "Nueva Vizcaya", "Isabela" },
                            datasets = new[]
                            {
                                new
                                {
                                    label = "Total Collections",
                                    data = renderChart,
                                    borderWidth = 1
                                }
                            },
                            options = new
                            {
                                scales = new
                                {
                                    y = new
                                    {
                                        beginAtZero = "true"
                                    }
                                }
                            }
                        }
                    },
                    parameters = new
                    {
                        dateFrom = DateFrom,
                        dateTo = DateTo,
                    },
                    helper = DotNetObjectReference.Create(this),
                    callback = nameof(ChartOnClickCallback)
                });
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    public async Task Render(DateTime? dateFrom, DateTime? dateTo)
    {
        if (dateFrom == dateTo)
        {
            dateTo = dateTo?.AddDays(1);
        }

        var renderChartResult =
            await DashboardClient.GetRenderChartAsync(dateFrom.GetValueOrDefault(), dateTo.GetValueOrDefault());

        var renderChart = renderChartResult.Data;

        if (renderChart is null || !renderChart.Any())
        {
            return;
        }

        await JSRuntime.InvokeVoidAsync("renderChart",
            new
            {
                chartId = "myChart",
                data = new
                {
                    type = "bar",
                    data = new
                    {
                        labels = new[] { "Nueva Vizcaya", "Isabela" },
                        datasets = new[]
                        {
                            new
                            {
                                label = "Total Collections",
                                data = renderChart,
                                borderWidth = 1
                            }
                        },
                        options = new
                        {
                            scales = new
                            {
                                y = new
                                {
                                    beginAtZero = "false"
                                }
                            }
                        }
                    }
                },
                parameters = new
                {
                    dateFrom = DateFrom,
                    dateTo = DateTo,
                },
                helper = DotNetObjectReference.Create(this),
                callback = nameof(ChartOnClickCallback)
            });
    }

    [JSInvokable]
    public async Task ChartOnClickCallback(string branch, string parameters)
    {
        try
        {
            if (string.IsNullOrEmpty(parameters))
            {
                return;
            }

            var enumBranch = branch.GetValueFromDescription<Branch>();
            var obj = JsonConvert.DeserializeAnonymousType(parameters,
                new { dateFrom = default(DateTime), dateTo = default(DateTime) });

            var baseResult =
                await DashboardClient.GetRenderChartByBranchAndDateAsync(enumBranch, obj.dateFrom, obj.dateTo);

            var res = baseResult.Data;

            await JSRuntime.InvokeVoidAsync("renderChart",
                new
                {
                    chartId = "myChart",
                    data = new
                    {
                        type = "bar",
                        data = new
                        {
                            labels = res.Select(c => c.CollectorName),
                            datasets = new[]
                            {
                                new
                                {
                                    label = "Total Collections",
                                    data = res.Select(c => c.Amount),
                                    borderWidth = 1
                                }
                            },
                            options = new
                            {
                                scales = new
                                {
                                    y = new
                                    {
                                        beginAtZero = "false"
                                    }
                                }
                            }
                        }
                    },
                    helper = DotNetObjectReference.Create(this),
                    callback = nameof(ChartOnClickCallback)
                });
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
        }
    }
}