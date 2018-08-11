param(
  [String]$remoteName = 'origin'
)

$ErrorActionPreference = "Stop"

function ExitOnError {
  if ($LastExitCode -ne 0) { 
      throw "Command returned non-zero exit code"
  }
}

$buildOutputDir = ".\build-tmp"

git worktree add $buildOutputDir $remoteName/master; ExitOnError
Remove-Item $buildOutputDir -Exclude .git -Recurse -Force
Copy-Item .\public\** $buildOutputDir -Force -Recurse
$commitHash = git rev-parse HEAD; ExitOnError

Push-Location $buildOutputDir
try {
  git add .; ExitOnError
  git status; ExitOnError
  git commit -m "Build $commitHash"; ExitOnError
  git push $remoteName HEAD:master; ExitOnError
}
finally {
  Pop-Location
}

git worktree remove $buildOutputDir; ExitOnError
