dotnet publish -c Release -r linux-x64 --self-contained false
Compress-Archive -Path .\bin\Release\net8.0\linux-x64\publish\* -DestinationPath .\Dist\ODC-NanoID.zip -Force