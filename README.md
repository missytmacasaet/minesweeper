# 🧨 Minesweeper Console App (C#)

This is a simple console-based Minesweeper game built with **C#** in **.Net 9**, following **Clean Architecture**, **SOLID principles**, and **Test-Driven Development (TDD)** using **xUnit** for testing.

## 💻 Environment Requirements

- OS: Windows
- .NET SDK: [.NET 9.0 or later](https://dotnet.microsoft.com/download/dotnet)
- IDE: Visual Studio 2022+ or any compatible editor
- Test Framework: [xUnit](https://xunit.net/)


## Features

- Smart reveal logic
- Random mine placement
- Win/loss detection
- Unit-tested logic
- Clean Architecture: 
  - `Domain` contains models and interfaces.  
  - `Application` implements business logic.  
  - `Infrastructure`: Present to demonstrate structure and support separation of concerns. Currently not actively used but can be extended for external services (e.g., persistence, logging).
  - `ConsoleApp` handles user input and game flow.
- Input validation
- Replayability
- Dependency injection
- NSubstitute

---

### Installation & Setup

1. Clone the repository:
   ```sh
   git clone <repository-url>
   cd Minesweeper.ConsoleApp
   ```
2. Build the project:
   ```sh
   dotnet build
   ```
3. Run the application:
   ```sh
   dotnet run
   ```

### Running Unit Test

1. Build the project:
   ```sh
   dotnet build
   ```
3. Run the test:
   ```sh
   dotnet test
   ```

## Usage

Upon launching the application, You will be prompted to enter the grid size (e.g., 4 for a 4x4 grid).

Enter the number of mines (max 35% of total squares).

Select cells to reveal by entering coordinates like A1, B2, etc.

The game ends when you uncover a mine or successfully reveal all non-mine squares.
