# Edu_QuizGen

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- An IDE (Visual Studio 2022 or Visual Studio Code)
- Git (optional, for version control)

## Setup Instructions

1. **Install .NET 8.0 SDK**
   - Download and install the .NET 8.0 SDK from the [official website](https://dotnet.microsoft.com/download/dotnet/8.0)
   - Verify installation by opening a terminal and running:
     ```bash
     dotnet --version
     ```
   - You should see version 8.0.x.x

2. **Clone the Repository**
   ```bash
   git clone https://github.com/Rafek86/Edu_QuizGen.git
   cd Edu_QuizGen
   ```

3. **Restore Dependencies**
   ```bash
   dotnet restore
   ```

## Running the Application

1. **Development Mode**
   ```bash
   dotnet run
   ```

2. **Production Build**
   ```bash
   dotnet build
   dotnet run --configuration Release
   ```

3. **Using Visual Studio**
   - Open the solution file (.sln) in Visual Studio
   - Press F5 or click the "Start" button to run the application

## Additional Information

- The application will run on:
  - HTTP: `http://localhost:5237`
  - HTTPS: `https://localhost:7039`
  - IIS Express: `http://localhost:14389` (HTTPS: `https://localhost:44345`)
- Swagger UI is available at `/swagger` endpoint
- For development, hot reload is enabled by default
- Check the console output for any error messages or startup information

## Troubleshooting

If you encounter any issues:

1. Ensure .NET 8.0 SDK is properly installed
2. Clear the NuGet cache:
   ```bash
   dotnet nuget locals all --clear
   ```
3. Delete the `bin` and `obj` folders and rebuild:
   ```bash
   dotnet clean
   dotnet restore
   dotnet build
   ```

## Support

For additional help or questions, please [open an issue](your-repository-issues-url) in the repository.
