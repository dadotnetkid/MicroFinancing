﻿using Hangfire.Dashboard;

namespace MicroFinancing.Core;

public class HangfireAuthorizationFilter: IDashboardAuthorizationFilter
{
    private readonly string[] _roles;

    public HangfireAuthorizationFilter(params string[] roles)
    {
        _roles = roles;
    }

    public bool Authorize(DashboardContext context)
    {
        return true; //I'am returning true for simplicity
    }
}
