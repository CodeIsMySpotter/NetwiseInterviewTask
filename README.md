# NetwiseInterviewTask

# NetwiseInterviewTask


## What is it about
This repository contains a finished simple web application. The app connects to specific endpoint, read the response and then write the response to .txt file


## Technologies used
- **.NET 9** (C# 12)
- **ASP.NET Core Web API**
- **Vanilla HTML/CSS/JS** for the frontend UI
- **Docker & Docker Compose** for containerization

## How to run

### Using Docker (Recommended)
You can easily spin up the application using Docker Compose from the root directory:
```bash
docker-compose up --build -d
```
This will start both the backend API and the frontend UI.
- **Frontend UI** will be available at: `http://localhost:3000`
- **Backend API** will be available at: `http://localhost:5205`

### Using .NET CLI
To run the application locally without Docker:
```bash
cd WebApp
dotnet run
```
Then simply open `FrontApp/index.html` in your browser to interact with the API.

## API Endpoints
- **`POST /facts/ask`**: Fetches a random cat fact from an external API and appends it to a local text file.
- **`GET /facts/file`**: Reads the local text file and returns all previously saved facts.
