Push-Location .\src\Client
try {
    dotnet fable yarn-start
}
finally {
    Pop-Location
}