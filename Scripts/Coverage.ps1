# run unit tests and gather coverage information
OpenCover.Console.exe -register:administrator -target:dotnet.exe -targetargs:"test --logger=trx;LogFileName=TestResults.trx" -filter:"+[*]* -[*Test*]*" -output:MdlpCoverage.xml

# upload reports to the codecov.io server
codecov -t "064fad14-0241-4b7a-9c86-ab7b5ed067a3" -f MdlpCoverage.xml 
