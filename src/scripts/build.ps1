#!/usr/bin/env pwsh

# This script publishes Commit for the given configuration and framework.
#
# Usage:
#   scripts/build.ps1 <configuration> <framework>
#
# Example:
#   scripts/build.ps1 release maccatalyst

$configuration = (Get-Culture).TextInfo.ToTitleCase($args[0])
$framework = $args[1]

write-host "Clean $configuration..."
dotnet clean -c $configuration

write-host "Publish $configuration for $framework..."

dotnet build -f:net6.0-$framework -c:$configuration
