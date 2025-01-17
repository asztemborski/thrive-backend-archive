﻿namespace Thrive.Modules.Identity.Shared.DTO;

public sealed record CurrentUserDto
{
    public required Guid Id { get; init; }
    public required string Username { get; init; }
    public required string Email { get; init; }
}