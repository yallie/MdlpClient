# run unit tests and gather coverage information
$args = "test MdlpApiClient.Tests --logger=trx;LogFileName=TestResults.trx"
$filter = "+[*]* -[*Test*]* -[*]MdlpApiClient.Xsd*"
OpenCover.Console.exe -returntargetcode -register:administrator -target:dotnet.exe "-targetargs:$args" "-filter:$filter" -output:MdlpCoverage.xml
$exit = $lastexitcode

# upload reports to the codecov.io server
codecov -t "064fad14-0241-4b7a-9c86-ab7b5ed067a3" -f MdlpCoverage.xml 

# convert trx reports to JUnit format so Gitlab can parse them
trx2junit MdlpApiClient.Tests\TestResults\TestResults.trx
& "$PSScriptRoot\TestReportPrefix.ps1" -inputFileName MdlpApiClient.Tests\TestResults\TestResults.xml -outputFileName MdlpApiClient.Tests\TestResults\TestResults.xml -prefix "Normal."

# make sure we don't fail on REST methods that need long timeouts
Wait-Event -Timeout 20

# run normal unit tests for the single-file version and ServiceStack5 without coverage
dotnet.exe test MdlpApiClient.Merged.Tests --logger="trx;LogFileName=TestResultsMerged.trx"
$exit2 = $lastexitcode
trx2junit MdlpApiClient.Merged.Tests\TestResults\TestResultsMerged.trx
& "$PSScriptRoot\TestReportPrefix.ps1" -inputFileName MdlpApiClient.Merged.Tests\TestResults\TestResultsMerged.xml -outputFileName MdlpApiClient.Merged.Tests\TestResults\TestResultsSingleFile.trx -prefix "Merged."

# return dotnet test exit code
if (($exit + $exit2) -ne 0)
{
	exit ($exit + $exit2)
}
