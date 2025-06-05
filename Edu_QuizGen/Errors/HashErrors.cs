namespace Edu_QuizGen.Errors;

public static class HashErrors
{
    public static readonly Error FileEmpty = new(
        "Hash.FileEmpty",
        "File is empty or null",
        StatusCodes.Status400BadRequest);

    public static readonly Error InvalidFileType = new(
        "Hash.InvalidFileType",
        "File is not a PDF",
        StatusCodes.Status400BadRequest);

    public static readonly Error HashCalculationFailed = new(
        "Hash.HashCalculationFailed",
        "Failed to calculate file hash",
        StatusCodes.Status500InternalServerError);

    public static readonly Error DuplicateCheckFailed = new(
        "Hash.DuplicateCheckFailed",
        "Error checking PDF duplicate",
        StatusCodes.Status500InternalServerError);


    public static readonly Error SaveFailed = new(
        "Hash.SaveFailed",
        "Error saving PDF",
        StatusCodes.Status500InternalServerError);


    public static readonly Error RetrievalFailed = new(
        "Hash.RetrievalFailed",
        "Failed to retrieve saved document",
        StatusCodes.Status500InternalServerError);


    public static readonly Error QuizNotFound = new(
        "Hash.QuizNotFound",
        "Quiz not found",
                StatusCodes.Status404NotFound);

    public static readonly Error DatabaseError = new(
        "Hash.DatabaseError",
        "Database operation failed",
        StatusCodes.Status500InternalServerError);
}