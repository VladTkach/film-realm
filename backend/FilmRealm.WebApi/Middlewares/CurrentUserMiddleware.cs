﻿using FilmRealm.BLL.Interfaces;
using FilmRealm.Common.JWT;

namespace FilmRealm.WebApi.Middlewares;

public class CurrentUserMiddleware
{
    private readonly RequestDelegate _next;

    public CurrentUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUserIdSetter userIdSetter)
    {
        var claimsUserId = context.User.Claims.FirstOrDefault(x => x.Type == JwtFactory.IdClaimName)?.Value;

        if (claimsUserId is not null && int.TryParse(claimsUserId, out var userId))
        {
            userIdSetter.SetCurrentUserId(userId);
        }

        await _next.Invoke(context);
    }
}