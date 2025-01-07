# Variables
$repoUrl = "https://github.com/BrrrUzi/HSAA_Projektarbeit.git"
$targetFolder = "C:\ProgramData\Hunt"

# Ensure the target folder exists
if (!(Test-Path -Path $targetFolder)) {
    New-Item -ItemType Directory -Path $targetFolder
}

# Clone the repository
git clone $repoUrl "$targetFolder\ProjectName"

Write-Host "Repository cloned to $targetFolder\ProjectName"
