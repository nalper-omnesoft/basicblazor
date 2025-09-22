# DevOps Assignment

A modern .NET 9 microservices application built with .NET Aspire, featuring a web frontend, API service, and PostgreSQL database.

## Architecture Overview

This application demonstrates a distributed microservices architecture using:

- **.NET Aspire** - For cloud-native app orchestration and service discovery
- **Blazor Server** - Interactive web UI components
- **PostgreSQL** - Database with pgWeb admin interface
- **OpenTelemetry** - Distributed tracing and observability
- **Service Discovery** - Automatic HTTPS+HTTP service communication

## Project Structure

```
DevOpsAssignment/
├── DevOpsAssignment.AppHost/           # Aspire orchestration host
├── DevOpsAssignment.ApiService/        # Backend API service
├── DevOpsAssignment.Web/               # Blazor web frontend
└── DevOpsAssignment.ServiceDefaults/   # Shared service configurations
```

### Components

- **AppHost**: Orchestrates the entire application, manages service dependencies and health checks
- **ApiService**: REST API providing weather forecasts and greeting messages endpoints
- **Web**: Blazor Server application consuming the API service
- **ServiceDefaults**: Shared configurations for resilience, service discovery, and telemetry

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (for PostgreSQL container)
- [.NET Aspire workload](https://learn.microsoft.com/dotnet/aspire/fundamentals/setup-tooling)

Install .NET Aspire workload:
```bash
dotnet workload install aspire
```

## Getting Started

### Running the Application

1. Clone the repository:
```bash
git clone <repository-url>
cd DevOpsAssignment
```

2. Start the Aspire AppHost:
```bash
cd DevOpsAssignment.AppHost
dotnet run
```

This will:
- Start PostgreSQL with pgWeb admin interface
- Launch the API service
- Launch the web frontend
- Open the Aspire Dashboard in your browser

### Accessing the Application

Once running, you can access:
- **Aspire Dashboard**: https://localhost:17217 (port may vary)
- **Web Frontend**: https://localhost:5001 (check dashboard for exact URL)
- **API Service**: https://localhost:5002 (check dashboard for exact URL)
- **pgWeb**: Accessible via Aspire Dashboard

## API Endpoints

The API Service exposes the following endpoints:

- `GET /weatherforecast` - Returns 5-day weather forecast
- `GET /greetings` - Returns all greeting messages from database
- `GET /greeting` - Returns a single greeting message
- `GET /health` - Health check endpoint

## Development

### Building the Solution

```bash
dotnet build
```

### Running Tests

```bash
dotnet test
```

### Database Migrations

The application automatically creates the PostgreSQL database on startup. The database initialization script is defined in `DevOpsAssignment.AppHost/AppHost.cs`.

## Configuration

### Environment Variables

The application uses standard .NET configuration with support for:
- `appsettings.json`
- `appsettings.Development.json`
- User secrets (for local development)
- Environment variables

### Service Discovery

Services communicate using Aspire's service discovery mechanism. The web frontend discovers the API service using the `https+http://apiservice` scheme, which prefers HTTPS over HTTP.

## Observability

The application includes comprehensive observability features:

- **Distributed Tracing**: OpenTelemetry integration for request tracing across services
- **Health Checks**: HTTP health endpoints for monitoring service availability
- **Metrics**: Runtime instrumentation and custom metrics
- **Logging**: Structured logging with correlation IDs

View all telemetry data in the Aspire Dashboard.

## Deployment

### Container Support

Each service can be containerized using Docker. The Aspire AppHost manages container orchestration during development.

### Production Deployment

For production deployments, consider:
1. Exporting Aspire manifest for deployment
2. Using Azure Container Apps (recommended)
3. Kubernetes deployment with generated manifests
4. Docker Compose for simpler deployments

## Technology Stack

- **.NET 9**: Latest framework version with performance improvements
- **.NET Aspire 9.4.2**: Cloud-native app orchestration
- **PostgreSQL**: Relational database
- **Blazor Server**: Interactive web UI
- **OpenTelemetry**: Observability and tracing
- **HTTP Resilience**: Built-in retry and circuit breaker patterns

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

## License

[Specify your license here]

## Support

For issues and questions, please open an issue in the GitHub repository.