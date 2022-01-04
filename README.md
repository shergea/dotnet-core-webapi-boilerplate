# RESTful API .Net 6.0 Boilerplate
A boilerplate/starter project for quickly building RESTful APIs using .Net 5.0 with Entity Framework.

# Quick Start

Clone the repo:
```bash
git clone https://github.com/shergea/dotnet-core-webapi-boilerplate
cd dotnet-core-webapi-boilerplate
```
Remove .git folder:
```bash
# Linux
rm -rf .git
# Windows
rd /s /q .git
```
Install the dependencies:
```bash
cd WebApi
dotnet restore
```

Install dotnet ef tool
```bash
dotnet tool install --global dotnet-ef
```

Set the environment variables:
```bash
# Linux
cp appsettings.json.example appsettings.json
# Windows
copy appsettings.json.example appsettings.json
```

Set your Connection string:
```bash
Set your ConnectionString in appsettings.json
```

Everything is okay, we can run project
```bash
# without watcher
dotnet run
# with watcher
dotnet watch run
```

# API Documentation
To view the list of available APIs, run the server and go to http://localhost:5000/swagger in your browser.
