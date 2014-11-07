
$ErrorActionPreference = "stop"

#Note: All the Settings are defined in the Sitecore.Octopus.ContentPackageGenerator.exe.config file in your tools folder.
$base_dir = "C:\Projects\Sitecore.Octopus";
$packageDestination = "C:\Projects\Sitecore.Octopus\GeneratedArtifacts";
$source = "$base_dir\data\currentserialization" 

# Arguments are:  seralization folder, Where you want package moved to, and git gommit hash of current build ( can use GetCommitHash task)

# It will generate 3 files. 
# 1) ItemsToPublish.json ( for use with sitecore.ship )
# 2) GeneratedContentPackage.update ( for use with sitecore.ship )
# 3) ReleaseNotes.txt (for passing into Octopus Deploy)

task GenerateSitecorePackage {
     exec { & "$base_dir\Sitecore.Octopus.ContentPackageGenerator\bin\Debug\Sitecore.Octopus.ContentPackageGenerator.exe" "$source" "$packageDestination" "1e5b544554a5fbbb6d793721dc45fc2eca5439c9"}
}

task GetCommitHash {
    exec { git rev-parse --short HEAD } | set-variable short_hash -scope global
    write-host "Git commit hash: $short_hash"
}

