namespace Edu_QuizGen.Errors;

public static class HashErrors
{
    public static readonly Error FileEmpty = new(
        "Hash.FileEmpty",
        "File is empty or null",
        400);

    public static readonly Error InvalidFileType = new(
        "Hash.InvalidFileType",
        "File is not a PDF",
        400);

    public static readonly Error HashCalculationFailed = new(
        "Hash.HashCalculationFailed",
        "Failed to calculate file hash",
        500);

    public static readonly Error DuplicateCheckFailed = new(
        "Hash.DuplicateCheckFailed",
        "Error checking PDF duplicate",
        500);

    public static readonly Error SaveFailed = new(
        "Hash.SaveFailed",
        "Error saving PDF",
        500);

    public static readonly Error RetrievalFailed = new(
        "Hash.RetrievalFailed",
        "Failed to retrieve saved document",
        500);

    public static readonly Error QuizNotFound = new(
        "Hash.QuizNotFound",
        "Quiz not found",
        404);

    public static readonly Error DatabaseError = new(
        "Hash.DatabaseError",
        "Database operation failed",
        500);
}