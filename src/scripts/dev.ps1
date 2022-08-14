#!/usr/bin/env pwsh

# This script sets Commit as the global editor to write commit messages.
#
# Usage:
#   scripts/dev.ps1 <configuration> <framework>
#
# Example:
#   scripts/dev.ps1 release maccatalyst

$configuration = (Get-Culture).TextInfo.ToTitleCase($args[0])
$framework = $args[1]
$cwd = Get-Location
$filePath = "$cwd/Commit.Desktop/bin/$configuration/net6.0-$framework/Commit.Desktop.app/Contents/MacOS/Commit.Desktop"

write-host "Set global core.editor to Commit $filePath"
git config --global core.editor $filePath
