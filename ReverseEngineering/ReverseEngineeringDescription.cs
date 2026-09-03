// ============================================================
// 1. GENERAL CONCEPT
// ============================================================
//
// Scaffolding & Reverse Engineering
// ============================================================
// We use Reverse Engineering & Scaffolding when we already have a database and want to generate C# code from it.
// Reverse Engineering:
// Analyzing an existing database and understanding its structure.
//
// Scaffolding:
// Automatically generating C# code from the database structure.
//
// In EF Core, Reverse Engineering + Scaffolding generate:
//   - Entity Classes
//   - DbContext
//   - Relationships
//   - Database configurations
//
// This is called the Database-First approach.
//
//
// Database-First:
//
//     Existing Database
//            ↓
//     Reverse Engineering
//            ↓
//        Scaffolding
//            ↓
//     Entity Classes + DbContext
//
//
// Code-First is the opposite:
//
//     C# Entity Classes
//            ↓
//          EF Core
//            ↓
//         Database
//
// ============================================================



// ============================================================
// 2. USING PACKAGE MANAGER CONSOLE (PMC)
// ============================================================
//
// Step #1: Open Package Manager Console
//
// Tools
//   → NuGet Package Manager
//   → Package Manager Console
//
//
// Step #2: Install EF Core Tools
//
// Install-Package Microsoft.EntityFrameworkCore.Tools
//
//
// Step #3: Install EF Core Design Package
//
// Install-Package Microsoft.EntityFrameworkCore.Design
//
//
// Step #4: Install the Database Provider
//
// Install-Package Microsoft.EntityFrameworkCore.SqlServer
//
//
// Step #5: Run Scaffold-DbContext
//
// Basic syntax:
//
// Scaffold-DbContext "Connection String" Provider
//
//
// Example:
//
// Scaffold-DbContext
// "Server=(localdb)\MSSQLLocalDB;Database=TechTalk;Integrated Security=True;TrustServerCertificate=True;"
// Microsoft.EntityFrameworkCore.SqlServer
//
//
// Example with Options:
//
// Scaffold-DbContext
// "Server=(localdb)\MSSQLLocalDB;Database=TechTalk;Integrated Security=True;TrustServerCertificate=True;"
// Microsoft.EntityFrameworkCore.SqlServer
// -DataAnnotations
// -ContextDir Data
// -OutputDir Entities
// -Force
//
//
// Important Options:
//
// -DataAnnotations
//     Generates Data Annotations in Entity Classes.
//
// -ContextDir
//     Specifies where the DbContext will be generated.
//
// -OutputDir
//     Specifies where the Entity Classes will be generated.
//
// -Context
//     Specifies the name of the DbContext.
//
// -Tables
//     Scaffolds only specific tables.
//
// -Schemas
//     Scaffolds only specific schemas.
//
// -Force
//     Overwrites existing generated files.
//
// -NoOnConfiguring
//     Prevents the connection string from being generated
//     inside OnConfiguring.
//
// ============================================================
//Scaffold-DbContext 'Server=(localdb)\MSSQLLocalDB;Database=TechTalk;Integrated Security=True;TrustServerCertificate=True;' Microsoft.EntityFrameworkCore.SqlServer -DataAnnotations -ContextDir Data -OutputDir Entities -Force


// ============================================================
// 3. USING .NET CLI
// ============================================================
//
// Step #1: Install EF Core CLI Tool
//
// dotnet tool install --global dotnet-ef
//
//
// Check the installed version:
//
// dotnet ef --version
//
//
// Step #2: Install EF Core Design Package
//
// dotnet add package Microsoft.EntityFrameworkCore.Design
//
//
// Step #3: Install the Database Provider
//
// dotnet add package Microsoft.EntityFrameworkCore.SqlServer
//
//
// Step #4: Run Reverse Engineering
//
// Basic syntax:
//
// dotnet ef dbcontext scaffold "Connection String" Provider
//
//
// Example:
//
// dotnet ef dbcontext scaffold
// "Server=(localdb)\MSSQLLocalDB;Database=TechTalk;Integrated Security=True;TrustServerCertificate=True;"
// Microsoft.EntityFrameworkCore.SqlServer
//
//
// Example with Options:
//
// dotnet ef dbcontext scaffold
// "Server=(localdb)\MSSQLLocalDB;Database=TechTalk;Integrated Security=True;TrustServerCertificate=True;"
// Microsoft.EntityFrameworkCore.SqlServer
// --data-annotations
// --context-dir Data
// --output-dir Entities
// --force
//
//
// Common CLI Options:
//
// --data-annotations
//     Generates Data Annotations in Entity Classes.
//
// --context-dir
//     Specifies where the DbContext will be generated.
//
// --output-dir
//     Specifies where the Entity Classes will be generated.
//
// --context
//     Specifies the name of the DbContext.
//
// --table
//     Scaffolds only a specific table.
//
// --schema
//     Scaffolds only a specific schema.
//
// --force
//     Overwrites existing generated files.
//
// --no-onconfiguring
//     Prevents the connection string from being generated
//     inside OnConfiguring.
//
// ============================================================
//
// PMC vs .NET CLI
// ============================================================
//
// Package Manager Console:
//
//     Scaffold-DbContext
//     -OutputDir
//     -ContextDir
//     -Force
//
//
// .NET CLI:
//
//     dotnet ef dbcontext scaffold
//     --output-dir
//     --context-dir
//     --force
//
// Both approaches perform the same EF Core
// Reverse Engineering / Scaffolding process.
//
// ============================================================
//open new powershall terminal and navigate to the project folder
//cd .\ReverseEngineering
//dotnet ef dbcontext scaffold "Server=(localdb)\MSSQLLocalDB;Database=TechTalk;Integrated Security=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer --data-annotations --context-dir Data --output-dir Entities --force