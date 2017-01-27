param($scriptRoot)

$ErrorActionPreference = "Stop"

$programFilesx86 = ${Env:ProgramFiles(x86)}
$msBuild = "$programFilesx86\MSBuild\14.0\bin\msbuild.exe"
$nuGet = "$scriptRoot..\tools\NuGet.exe"
$solution = "$scriptRoot\..\Conjunction.sln"

& $nuGet restore $solution
& $msBuild $solution /p:Configuration=Release /t:Rebuild /m

$ConjunctionAssembly = Get-Item "$scriptRoot\..\src\Foundation\Core\Code\bin\Conjunction.Foundation.Core.dll" | Select-Object -ExpandProperty VersionInfo
$targetAssemblyVersion = $ConjunctionAssembly.ProductVersion

& $nuGet pack "$scriptRoot\Conjunction.nuget\Conjunction.nuspec" -version $targetAssemblyVersion
& $nuGet pack "$scriptRoot\..\src\Foundation\Core\Code\Conjunction.Foundation.Core.csproj" -Symbols -Prop "Configuration=Release"