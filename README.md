![.NET 8.0](https://img.shields.io/badge/.NET-8.0-blue.svg)
![C#](https://img.shields.io/badge/language-C%23-blue.svg)

# Member

| Name            | Student Id     | 
|-----------------|--------------- |
|Nguyễn Hoàng Phúc| 3120410409     |
|Võ Ngọc Phú      | 3120410404     |

# E-Commerce Backend.
This project is an e-commerce backend API built using .NET 8.0 and C#. It provides a robust and scalable solution for managing an online store that sells model kits. The API handles various functionalities required for an e-commerce platform, including user management, product management, order processing, and payment integration.



# Features

- **User Management**: Register, login, verify email, forgot password and manage user accounts.
- **Product Management**: Add, update, delete, and view products.
- **Order Processing**: Create and manage customer orders..
- **Category Management**: Track and manage product category.
- **Shipping Management**: Manage shipping details and statuses.
- **Admin Panel**: Administrative functionalities for managing the store.

# Technologies Used

- **.NET 8.0**: The latest version of the .NET framework for building high-performance applications.
- **C#**: The primary programming language used for development.
- **Entity Framework Core**: ORM for database operations.
- **SQL Server**: Database for storing application data.
- **Swagger**: API documentation and testing.
- **Serilog**: Logging framework for capturing application logs.
- **JWT Authentication**: Secure authentication and authorization.
- Redis: A high-performance, in-memory data store used for caching frequently accessed data, improving application response time, and reducing database load.
- Docker: Using Docker for seamless deployment and management.

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- SQL Server
- Visual Studio or any other C# IDE

### Installation

1. Clone the repository:
   ```sh
   git clone https://github.com/yourusername/commerce-be.git
   cd commerce-be
   cd Restapi-net8

2. Setup the database:
- Update the connection string in appsettings.json to point to your SQL Server instance.
- Run the database migrations to create the necessary tables:

```dotnet ef database update```

- Build and run application: 

```dotnet watch run```

- That listen on port http://localhost:5299

#### With Docker
   ```git clone https://github.com/yourusername/commerce-be.git```
   ```cd commerce-be```
   ```cd Restapi-net8```
    
# Run Docker compose:

```docker-compose up -d```

- That listen on port http://localhost:5299