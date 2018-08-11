yarn install --frozen-lockfile
dotnet restore
Push-Location .\src\Client
dotnet fable yarn-build
Pop-Location
