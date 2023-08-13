dotnet tool restore
yarn install --frozen-lockfile
dotnet fable watch --cwd .\src\Client\ --run yarn webpack serve
