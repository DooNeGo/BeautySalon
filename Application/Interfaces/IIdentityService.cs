﻿using BeautySalon.Domain;

namespace BeautySalon.Application.Interfaces;

public interface IIdentityService
{
    public User? CurrentUser { get; }

    public event Action<User>? LoginSuccesful;

    public Task<bool> AuthorizeAsync(string username, string password);
}
