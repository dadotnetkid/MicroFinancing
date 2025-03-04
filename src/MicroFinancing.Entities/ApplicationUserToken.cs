﻿using Microsoft.AspNetCore.Identity;

namespace MicroFinancing.Entities;

public  class ApplicationUserToken : IdentityUserToken<string>
{
    public virtual ApplicationUser? User { get; set; }
}