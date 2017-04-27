@ECHO OFF

IF "%CONFIGURATION%"=="" SET CONFIGURATION=Release

star --resourcedir="%~dp0OrganizationManagementApp\wwwroot" "%~dp0OrganizationManagementApp/bin/%CONFIGURATION%/OrganizationManagementApp.exe"
