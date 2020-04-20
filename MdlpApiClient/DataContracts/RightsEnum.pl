#!perl 

# Syntax: perl -w RightsEnum.pl RightsEnum.cs >RightsEnum2.cs

# Transforms this:
# public const string MANAGE_ACCOUNTS = "MANAGE_ACCOUNTS"; // Управление учетками

# Into this:
# /// <summary>
# /// Управление учетками
# /// </summary>
# public const string MANAGE_ACCOUNTS = "MANAGE_ACCOUNTS";

while (<>) {

	if (/(\s+[^\/]+)\/\/([^\/]+.+?)\s*$/) {
		print "
        /// <summary>
        ///$2
        /// </summary>\n";
		print "$1\n";
		next;
	}

	print;
}