// ============================================================
// 1. GENERAL CONCEPT
// ============================================================
//
// EF Core Migrations
// ============================================================
//
// Migrations are used to keep the database schema
// synchronized with the C# Entity Classes.
//
// Instead of manually changing the database,
// we change the C# model and EF Core creates
// the required database changes.
//
// Main idea:
//
// C# Entity Changes ===> Add Migration ===> Migration File ===> Update Database ===> Database Schema Updated
// ============================================================


// ============================================================
// 2. DATABASE VERSIONS
// ============================================================
//
// Every Migration represents a version of the database.
//
// Example:
//
// Version 1: InitialCreate
//
// Version 2: AddProduct
//
// Version 3: AddCategory
//
// Version 4: AddProductCategoryRelationship
//
// Database:
// Version 1 ===> Version 2 ===> Version 3 ===> Version 4
//
// EF Core keeps track of applied migrations
// using the __EFMigrationsHistory table.
//
//
// This allows EF Core to know:
//
//     Which migrations are already applied?
//
//     Which migrations are still pending?
//
//
// ============================================================


// ============================================================
// 3. BASIC MIGRATION WORKFLOW
// ============================================================
//
// Step #1: Create or modify Entity Classes
//
// Step #2: Create a Migration
//
// Step #3: Review the Migration
//
// Step #4: Update the Database
//
//
// Example:
//
// Add a new property:
//
// public decimal Price { get; set; }
//
//
// Create Migration:
//
// Add-Migration AddProductPrice
//
//
// Apply Migration:
//
// Update-Database
//
//
// ============================================================


// ============================================================
// 4. USING PACKAGE MANAGER CONSOLE (PMC)
// ============================================================
//
// Open:
//
// Tools
//   → NuGet Package Manager
//   → Package Manager Console
//
//
// ============================================================
//
// Create a Migration:
//
// Add-Migration InitialCreate
//
//
// Apply Migrations:
//
// Update-Database
//
//
// List Migrations:
//
// Get-Migration
//
//
// Remove Last Migration:
//
// Remove-Migration
//
//
// Rollback to a Specific Migration:
//
// Update-Database MigrationName
//
//
// Generate SQL Script:
//
// Script-Migration
//
// Note: Add-Migration and Remove-Migration do no not change in the data base 
// it change the migration files and update the `__EFMigrationsHistory` table
// and to applay this change to the database we use `Update-Database` command
// 
// ============================================================


// ============================================================
// 5. USING .NET CLI
// ============================================================
//
// Open PowerShell / Terminal
//
// Navigate to the project folder:
//
// cd .\YourProject
//
//
// ============================================================
//
// Install EF Core CLI Tool:
//
// dotnet tool install --global dotnet-ef
//
//
// Check Version:
//
// dotnet ef --version
//
//
// ============================================================
//
// Create a Migration:
//
// dotnet ef migrations add InitialCreate
//
//
// Apply Migrations:
//
// dotnet ef database update
//
//
// List Migrations:
//
// dotnet ef migrations list
//
//
// Remove Last Migration:
//
// dotnet ef migrations remove
//
//
// Rollback to a Specific Migration:
//
// dotnet ef database update MigrationName
//
//
// Generate SQL Script:
//
// dotnet ef migrations script
//
// Note: `dotnet ef migrations add` and `dotnet ef migrations remove` do no not change in the data base 
// it change the migration files and update the `__EFMigrationsHistory` table
// and to applay this change to the database we use `dotnet ef database update` command
//
// ============================================================


// ============================================================
// 6. PMC VS .NET CLI
// ============================================================
//
// Package Manager Console:
//
// Add-Migration
// Update-Database
// Get-Migration
// Remove-Migration
// Script-Migration
//
//
// .NET CLI:
//
// dotnet ef migrations add
// dotnet ef database update
// dotnet ef migrations list
// dotnet ef migrations remove
// dotnet ef migrations script
//
//
// Both approaches perform the same EF Core
// Migration operations.
//
//
// ============================================================


// ============================================================
// 7. IMPORTANT NOTES
// ============================================================
//
// Add-Migration
//     Creates the migration files.
//
// Update-Database
//     Applies pending migrations to the database.
//
// Remove-Migration
//     Removes the last migration from the project.
//
//
// Important:
//
// Add-Migration ≠ Update-Database
//
// Add-Migration
//     → Creates Migration
//
// Update-Database
//     → Applies Migration
//
//
// ============================================================


// ============================================================
// 8. SIMPLE EXAMPLE
// ============================================================
//
// Initial Model:
//
// Product
//     Id
//     Name
//
//
// Create:
//
// Add-Migration InitialCreate
// Update-Database
//
//
// Later:
//
// Product
//     Id
//     Name
//     Price
//
//
// Create:
//
// Add-Migration AddProductPrice
// Update-Database
//
//
// Database Versions:
//
// InitialCreate
//       ↓
// AddProductPrice
//
// ============================================================