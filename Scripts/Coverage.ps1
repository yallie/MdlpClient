# run unit tests and gather coverage information
$args = "test MdlpApiClient.Tests --logger=trx;LogFileName=TestResults.trx"
$filter = "+[*]* -[*Test*]* -[*]MdlpApiClient.Xsd*"
OpenCover.Console.exe -returntargetcode -register:administrator -target:dotnet.exe "-targetargs:$args" "-filter:$filter" -output:MdlpCoverage.xml
$exit = $lastexitcode

# upload reports to the codecov.io server
codecov -t "064fad14-0241-4b7a-9c86-ab7b5ed067a3" -f MdlpCoverage.xml 

# convert trx reports to JUnit format so Gitlab can parse them
trx2junit MdlpApiClient.Tests\TestResults\TestResults.trx

# make sure we don't fail on REST methods that need long timeouts
Wait-Event -Timeout 20

# run normal unit tests for the single-file version and ServiceStack5 without coverage
dotnet.exe test MdlpApiClient.Merged.Tests --logger="trx;LogFileName=TestResultsSingleFile.trx"
trx2junit MdlpApiClient.Merged.Tests\TestResults\TestResultsSingleFile.trx

# return dotnet test exit code
# exit $exit