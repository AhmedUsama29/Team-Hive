# 🐝 Task-Hive

Task-Hive is a modern team and task management application built with ASP.NET Core, Entity Framework (EF) Core, LINQ, JWT Token Authentication, and Onion Architecture. It’s designed to streamline team collaboration and task tracking with a focus on scalability, security, and clean code.

## Project Overview
Task-Hive empowers teams to manage tasks and members efficiently. The project follows the Onion Architecture for a modular, testable design, with layers like Domain, Application, Infrastructure, and Presentation. It leverages cutting-edge .NET technologies to deliver a robust solution.

## Features
- **Task Management:** Create, assign, and track tasks seamlessly  
- **Team Collaboration:** Manage team members and roles with recent enhancements  
- **Secure Access:** Implements JWT Token Authentication for user authentication and authorization  
- **Efficient Data Operations:** Uses EF Core and LINQ with patterns like Generic Repository, Unit of Work, and Specification Pattern  
- **Scalable Design:** Built with Onion Architecture for maintainability and growth  

## Repository Structure
- **Domain:** Core business logic, models, and contracts  
- **Data/Infrastructure:** EF Core context, repositories, and authentication services  
- **Presentation:** API controllers (e.g., TasksController, AuthenticationController)  
- **Shared:** Reusable utilities and services  

## Technologies Used
- **Backend:** ASP.NET Core, C#  
- **Database:** EF Core, LINQ  
- **Authentication:** JWT Token Authentication  
- **Architecture:** Onion Architecture  
- **Tools:** Git, Visual Studio Code, Postman
