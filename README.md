# Task Management System

## 🖥️ Project Overview
This is a simple Task Management system (ToDo/Task Manager).  
It includes features such as adding, editing, deleting, and viewing tasks, with real-time notifications and API integration.

---

## 🛠️ Technologies in Use

- **Backend:** .NET Core 9.0, ASP.NET Core Web API  
- **Frontend:** Blazor WebAssembly, Bootstrap 5.3, HTML5, CSS3  
- **Architecture & Patterns:** Clean Architecture, Mediator, CQRS, Clean Code  
- **Libraries:** AutoMapper, FluentValidation, DataAnnotation Validators, MediatR, Worker Services  
- **Logging:** Serilog (Sink: Seq)  
- **Real-time Communication:** SignalR  
- **API Documentation:** Swagger (Swashbuckle)  
- **Authentication & Authorization:** JWT (JSON Web Token)  
- **Development Tools:** Visual Studio 2022  

---

## 📌 Project Features

- Add, edit, delete, and view tasks  
- Connect Blazor WASM frontend to Web API  
- Store data in SQL Server  
- JWT-based authentication  
- Server-side and client-side validation  
- Logging operations with Serilog to Seq  
- API documentation with Swagger  
- Mediator + CQRS pattern in API layer  
- Real-time notifications using SignalR (e.g., "New task added")

---

## 🔹 Authentication Module

- Blazor WASM login form  
- Generate JWT token in API and store in LocalStorage  

---

## 🔹 Task Management Module (CRUD)

- Task list page  
- Create/Edit task page with form validation  
- Use MediatR + CQRS for task operations  
- AutoMapper mapping between DTOs and Entities  
- Real-time notifications using SignalR

---


## 🎯 Technical Goals

- ✅ Implement Clean Architecture  
- ✅ Implement CQRS with MediatR  
- ✅ Design RESTful API with ASP.NET Core  
- ✅ Connect Blazor WASM to API  
- ✅ JWT Authentication  
- ✅ Use AutoMapper and FluentValidation  
- ✅ Implement SignalR for real-time updates  
- ✅ Logging with Serilog and Seq  
- ✅ Swagger API documentation  

---

## 📂 Project Structure
The project follows **Clean Architecture** and is organized into the following layers:

- **Domain:** Core business entities and rules  
- **Application:** Business logic, MediatR handlers, DTOs, Validators  
- **Blazor:** Blazor WebAssembly client project (Pages, Components, Forms)  
- **API:** ASP.NET Core Web API project, Controllers, Swagger, JWT Auth  
- **Infrastructure:** Database access, repositories, external services  
- **Shared:** Shared DTOs, constants, and utilities  

---

## 📖 How to Run

1. Clone the repository:

git clone https://github.com/saharakbari/Blazor-App.git


---


## 📌 Notes
   Made with ❤️ by [sahar]



