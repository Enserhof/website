function ExitOnError {
    if ($LastExitCode -ne 0) {
        throw "Command returned non-zero exit code"
    }
}

yarn install --frozen-lockfile; ExitOnError
dotnet restore; ExitOnError
yarn webpack; ExitOnError
Copy-Item .\src\Server\api,staticwebapp.config.json .\deploy -Recurse -Force
