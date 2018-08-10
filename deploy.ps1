param(
  [String]$remoteName = 'origin'
)

$buildOutputDir = ".\build-tmp"

git worktree add $buildOutputDir $remoteName/master
Remove-Item $buildOutputDir -Exclude .git -Recurse -Force
Copy-Item .\public\** $buildOutputDir -Force -Recurse
$commitHash = git rev-parse HEAD

Push-Location $buildOutputDir
git add .
git status
git commit -m "Build $commitHash"
git push $remoteName HEAD:master
Pop-Location

git worktree remove $buildOutputDir
