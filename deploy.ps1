param(
  [String]$remoteName = 'origin'
)

function ExitOnError {
  if ($LASTEXITCODE -ne 0) { 
      throw "Command returned non-zero exit code"
  }
}

$buildOutputDir = ".\build-tmp"

git worktree add $buildOutputDir $remoteName/master 2>&1; ExitOnError
try {
	Remove-Item $buildOutputDir -Exclude .git -Recurse -Force
	Copy-Item .\public\** $buildOutputDir -Force -Recurse
	$commitHash = git rev-parse HEAD; ExitOnError

	Push-Location $buildOutputDir
	try {
	  $isDirty = git status -s
	  if ($isDirty) {
		Write-Host "Work tree dirty, committing changes"
		git add .; ExitOnError
		git commit -m "Build $commitHash"; ExitOnError
		git push $remoteName HEAD:master 2>&1; ExitOnError
	  }
	  else {
		Write-Host "Work tree clean, no commit necessary"
	  }
	}
	finally {
	  Pop-Location
	}
finally {
	git worktree remove $buildOutputDir --force; ExitOnError
}
