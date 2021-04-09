@echo off

set root=%cd%

dotnet restore %root%/src/backend
start "API" cmd /c "dotnet run --project %root%/src/backend/API/API.csproj & pause"
start "Front" cmd /c "npm start --prefix %root%/src/front/huelette & pause"