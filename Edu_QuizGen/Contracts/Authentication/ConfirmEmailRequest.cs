﻿namespace Edu_QuizGen.Contracts.Authentication;

public record ConfirmEmailRequest
(
    string UserId,
    string Code
);
