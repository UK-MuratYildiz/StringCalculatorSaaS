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
├── StringCalculator.API/ # ASP.NET Core Web API (Controller Layer)
├── StringCalculator.Application/ # Application Layer (CQRS, MediatR)
├── StringCalculator.Domain/ # Domain Layer (Business Logic, Interfaces)
├── StringCalculator.Infrastructure/ # Infrastructure Layer (Implementations)
├── StringCalculator.Tests/ # Unit Tests (xUnit, Moq)


---


