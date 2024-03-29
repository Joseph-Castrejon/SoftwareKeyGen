"Software Key Generator" by Joseph Castrejon

This is a small program aimed at creating product / license keys for software
registration or validation. It uses a psuedo-random number generator to create
a hyphenated 25 digit long code using all letters in the english alphabet as 
well as digits 0-9. The codes are not truely random and can be recreated using
a constant seed value (if running the same .NET framework version), which 
could be a benefit if a software product version can only use codes from a 
specific seed value. In the general case, the program will use the system 
clock to generate pseudo-random codes. The total number of unique codes that 
can be created from this approach is on the order of 9.31E+33, which can be 
found by calculating the number of permutations for 36 objects with 25 samples.

I have not created a validation process and leave that to whoever uses the 
codes generated by this program. There are many verification schemes out there 
and careful research about the pros/cons of each scheme must be done. I 
personally enjoy using free software, but understand the need of protecting
intellectual property from piracy. I am releasing this source code under the 
3-Clause BSD license. Please see the LICENSE.txt file for more information.

This program was made for the .NET framework. It will run natively on Windows
and can run on Mac / Linux if used with Mono.

=============================| GENERAL USAGE |=================================

-Creating a single key:

	To create a single key, simply run SoftwareKeyGen.exe from 
	the command line / terminal.
 
===============================================================================

-Creating many keys at once:

	To create "x" number of keys, run SoftwareKeyGen with the "/num" switch	

	For example, to create 1000 keys use:
 	
		SoftwareKeyGen.exe /num 1000

-Creating a key from a seed value:
	
	To create a key from a specific seed value, run SoftwareKeyGen with the
	"/seed" switch.

	For Example:

		SoftwareKeyGen.exe /seed 12345

-Creating keys with a specific seed value:

	You can specify how many keys you wish to create along with a given
	seed value, using both the "/num" and "/seed" switches in either order.
		
	For Example:
		
		SoftwareKeyGen.exe /seed 12345 /num 1000

	which will also be equivalent to
		
		SoftwareKeyGen.exe /num 1000 /seed 12345

===============================================================================

