# Add prefix to the test fixture names
# This step is necessary because GitLab seems to
# merge test reports based on their class names
#
# Usage:
#
# powershell /file TestReportPrefix.ps1 -inputFileName in.xml -outputFileName out.xml -prefix MyProject

# Powershell 5.0 is required for using:
#using assembly System.Xml.Linq
#using namespace System.Xml.Linq

param (
	[parameter(Mandatory = $true)] $inputFileName, 
	[parameter(Mandatory = $true)] $outputFileName, 
	[parameter(Mandatory = $true)] $prefix
)

$xmlLinq = [System.Reflection.Assembly]::LoadWithPartialName("System.Xml.Linq");

echo "Loading $inputFileName..."
$xml = [System.Xml.Linq.XDocument]::Load($inputFileName)

# add prefix to all test suite class names
ForEach ($suite in $xml.Root.Elements("testsuite")) 
{
	ForEach ($case in $suite.Elements("testcase"))
	{
		$name = $case.Attribute("classname")
		if ($name -ne $null)
		{
			$name.Value = $prefix + "." + $name.Value
		}
	}
}

echo "Saving $outputFileName..."
$xml.Save($outputFileName)
