# run unit tests and gather coverage information
$args = "test --logger=trx;LogFileName=TestResults.trx"
$filter = "+[*]* -[*Test*]*"
OpenCover.Console.exe -returntargetcode -register:administrator -target:dotnet.exe "-targetargs:$args" "-filter:$filter" -output:MdlpCoverage.xml
$exit = $lastexitcode

# upload reports to the codecov.io server
codecov -t "064fad14-0241-4b7a-9c86-ab7b5ed067a3" -f MdlpCoverage.xml 

# convert trx reports to JUnit format so Gitlab can parse them
trx2junit MdlpApiClient.Tests\TestResults\TestResults.trx

# return dotnet test exit code
# exit $exit