@ECHO OFF

IF "%CONFIGURATION%"=="" SET CONFIGURATION=Debug

star --resourcedir="%~dp0src\Products\wwwroot" "%~dp0src/Products/bin/%CONFIGURATION%/Products.exe"