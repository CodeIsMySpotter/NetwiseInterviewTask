# NetwiseInterviewTask


## What is it about
This repository contains a finished simple web application. The app connects to specific endpoint, read the response and then write the response to .txt file


## Technologies used
- **.NET 8** (C# 12)
- **ASP.NET Core Web API**
- **Docker & Docker Compose** for containerization
- **Swagger / OpenAPI** for API documentation

## How to run

### Using Docker (Recommended)
You can easily spin up the application using Docker Compose from the root directory:
```bash
docker-compose up --build -d
```

### Using .NET CLI
To run the application locally without Docker:
```bash
cd WebApp
dotnet run
```
Once the application is running, you can access the **Swagger UI** to test the endpoints by navigating to `/swagger` in your browser.

## API Endpoints
- **`POST /facts/ask`**: Fetches a random cat fact from an external API and appends it to a local text file.
- **`GET /facts/file`**: Reads the local text file and returns all previously saved facts.
