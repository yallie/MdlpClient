# Push changes to the public mirror

param (
	[parameter(Mandatory=$true)] $dir,
	[parameter(Mandatory=$true)] $repo,
	[parameter(Mandatory=$true)] $url,
	[parameter(Mandatory=$true)] $user,
	[parameter(Mandatory=$true)] $remote,
	[parameter(Mandatory=$true)] $token
)

# create mirror if not exists
if (-not (Test-Path $dir))
{
	echo "Mirror directory doesn't exits, create $dir..."
	md $dir
	pushd $dir

	echo "Cloning..."
	git clone --mirror $repo .

	echo "Building url..."
	$uri = [System.UriBuilder]$url
	$uri.UserName = $user
	$uri.Password = $token
	$url = $uri.ToString()

	echo "Adding new remote..."
	git remote add $remote $url
	popd
}

# update mirror and push to remote
pushd $dir
echo "Fetching updates to the mirror..."
git fetch

echo "Pushing to $remote..."
git push $remote
popd
