@echo off
mode con: cols=160 lines=78
set root=%cd%

cd %root%/src/backend/API 
echo Publishing the API
dotnet publish -c Release -r win-x64 --output %root%/Deploy/Api

cd %root%/src/front/huelette
echo Creating front-app build
start /B /wait "Building Front" cmd /c "npm update" 
start /B /wait "Building Front" cmd /c "npm run build" 
echo Moving build to %root%/Deploy/Front
move %cd%/build %root%/Deploy/Front
pause
