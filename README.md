# 🚌 Trip Coordination System

A web-based platform for students and general users to find, create, and join shared trips to various destinations. Designed to reduce travel costs and improve coordination among travelers.

---

## 🚀 Features

- 🗓️ Browse and join upcoming trips based on location and date
- ✍️ Create a trip as a trip organizer with route, pickup, and destination details
- 🧍 Role-based access (Admin, Trip Organizer, Student)
- 👤 View your joined or upcoming trips
- ❌ Leave a trip if plans change
- 🧾 Trip request system for unlisted routes

---

## 🛠️ Tech Stack

- **Frontend**: ASP.NET Core MVC, Razor Views, Bootstrap 5
- **Backend**: C#, Entity Framework Core & Dapper, SQL Server
- **Database**: SQL Server with stored procedures
- **Architecture**: Multi-project layered architecture
  - `.UI` – Frontend Controllers & Views
  - `.Data` – Repositories (Interfaces & Dapper Implementations)
  - `.Common` – Domain Models

---


