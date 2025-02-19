# StringCalculatorSaaS

A SaaS-based string calculator application built with .NET Core, following Clean Architecture, CQRS, and MediatR patterns.

---

## Table of Contents

1. [Overview](#overview)
2. [Features](#features)
3. [Project Structure](#project-structure)

---

## Overview

The **StringCalculatorSaaS** project is a .NET Core-based web API that provides a service for calculating the sum of numbers provided in a string. The string can contain numbers separated by commas, newlines, or custom delimiters. The application follows Clean Architecture and uses the CQRS pattern with MediatR for handling commands and queries.

---

## Features

- **String Calculation**:
  - Supports numbers separated by commas (`,`), newlines (`\n`), or custom delimiters.
  - Handles invalid inputs (e.g., trailing delimiters).
  - Throws exceptions for negative numbers and displays them in the error message.
  - Ignores numbers greater than 1000.

- **Clean Architecture**:
  - Separates concerns into layers: API, Application, Domain, and Infrastructure.
  - Promotes maintainability and scalability.

- **CQRS and MediatR**:
  - Uses commands and queries to handle business logic.
  - Decouples the API layer from the application logic.

- **Unit Tests**:
  - Comprehensive unit tests using xUnit and Moq.
  - Ensures correctness and reliability of the application.

---

## Project Structure

StringCalculatorSaaS/

├── SimpleStringCalculator.API/ # ASP.NET Core Web API 

├── StringCalculator.API/ # ASP.NET Core Web API (Controller Layer)

├── StringCalculator.Application/ # Application Layer (CQRS, MediatR)

├── StringCalculator.Domain/ # Domain Layer (Business Logic, Interfaces)

├── StringCalculator.Infrastructure/ # Infrastructure Layer (Implementations)

├── StringCalculator.Tests/ # Unit Tests (xUnit, Moq)


---
SimpleStringCalculator.API
[
A simple .NET project that implements a string calculator with a method Add to sum numbers provided in a string. 
This project is designed to be added to an existing solution for easy code review and testing.
---
Handles empty strings.

Supports single and multiple numbers.

Allows custom delimiters.

Throws exceptions for invalid inputs and negative numbers.

Ignores numbers greater than 1000.

Features
---
Empty String: Returns 0 for an empty string.

Single Number: Returns the number itself.

Multiple Numbers: Returns the sum of numbers separated by commas or newlines.

Custom Delimiters: Supports custom single-character or multiple delimiters.

Negative Numbers: Throws an exception with a list of negative numbers.

Numbers Greater Than 1000: Ignores numbers greater than 1000.
]
