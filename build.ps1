function ExitOnError {
    if ($LastExitCode -ne 0) { 
        throw "Command returned non-zero exit code"
    }
}

yarn install --frozen-lockfile; ExitOnError
dotnet restore; ExitOnError
Push-Location .\src\Client
try {
    dotnet fable yarn-build; ExitOnError
}
finally {
    Pop-Location
}
