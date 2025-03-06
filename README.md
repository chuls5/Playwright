# Playwright Tests Project

## Table of Contents
- [Introduction](#introduction)
- [Project Structure](#project-structure)
- [Installation](#installation)
- [Usage](#usage)
- [Features](#features)
- [Contributing](#contributing)
- [License](#license)
- [FAQ](#faq)

## Introduction
This project contains automated browser tests built with Playwright for .NET (running on .NET 8.0). The tests appear to be designed for testing web applications, with a focus on organized test execution and results reporting.

## Project Structure
The project follows a standard .NET project structure with Playwright integration:

```
PlaywrightTests/
├── bin/                      # Compiled binaries
├── node_modules/             # Node.js dependencies (Playwright requires Node.js)
│   ├── playwright/           # Main Playwright package
│   └── playwright-core/      # Core Playwright functionality
├── obj/                      # .NET build objects
├── PlaywrightTests/          # Main test code directory
└── TestResults/              # Output directory for test execution results
    └── Various timestamped runs
```

## Installation

### Prerequisites
- .NET 8.0 SDK
- Node.js (required for Playwright)

### Setup Steps
1. Clone the repository:
   ```
   git clone <repository-url>
   cd PlaywrightTests
   ```

2. Install .NET dependencies:
   ```
   dotnet restore
   ```

3. Install Playwright browsers:
   ```
   dotnet tool install --global Microsoft.Playwright.CLI
   playwright install
   ```

## Usage

### Running Tests
Run the tests using the .NET CLI:

```
dotnet test
```

### Test Configuration
Configure test parameters in your project settings or use command-line arguments:

```
dotnet test --settings=test.runsettings
```

### Viewing Test Results
Test results are stored in the `TestResults` directory, organized by timestamp and run identifier. Each test run contains:
- Input data (`In` directory)
- Output results (`Out` directory)

## Features
- Multi-browser testing (Chrome, Firefox, WebKit)
- Automated screenshot and trace capturing
- Test results organization with timestamps
- International language support (multiple localization folders present)
- Trace viewing and HTML reporting capabilities

## Contributing
1. Fork the repository
2. Create a feature branch: `git checkout -b feature/your-feature-name`
3. Commit your changes: `git commit -m 'Add some feature'`
4. Push to the branch: `git push origin feature/your-feature-name`
5. Open a pull request

## License
[Add appropriate license information here]

## FAQ

### How do I debug test failures?
Test traces and screenshots can be found in the TestResults directory. Use the Playwright Trace Viewer to analyze test execution:
```
playwright show-trace path/to/trace.zip
```

### How do I run tests in a specific browser?
Modify your test configuration or use command-line arguments:
```
dotnet test -- --browser=chromium
```

### What's the structure of the test results?
Each test run creates a timestamped directory with input and output folders, allowing for tracking of test data and results.
