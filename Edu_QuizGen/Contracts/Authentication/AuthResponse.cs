﻿namespace Edu_QuizGen.Contracts.Authentication;

public record AuthResponse
    (
    string Id,
    string Email,
    string FirstName,
    string LastName,
    string Token,
    int ExpiresIn,
    string RefreshTokem,
    int RefreshTokenExpiration
    );
