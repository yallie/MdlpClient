# Add or update git remote
#
# Examples:
#
# $remote = "gitlab"
# $url = "https://gitlab.ultima.ru/yakovlev.a/MdlpClient.git"
# $user = "yallie"
# $token = "sdfkhsdkfhskjfhsdf"

param (
	[parameter(Mandatory=$true)] $remote,
	[parameter(Mandatory=$true)] $url,
	[parameter(Mandatory=$true)] $user,
	[parameter(Mandatory=$true)] $token
)

$uri = [System.UriBuilder]$url
$uri.UserName = $user
$uri.Password = $token

$url = $uri.ToString()

echo "Adding remote..." 
git remote add $remote $url

if ($lastexitcode -ne 0)
{
	echo "Remote already exists, updating its url..."
	git remote set-url $remote $url
}

echo "Setting remote.$remote.mirror = true flag..."
git config remote.$remote.mirror true
