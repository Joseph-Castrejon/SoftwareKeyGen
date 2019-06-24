//Author: Joseph Castrejon
//Assembly: SoftwareKeyGen
//Description: 25 Digit Key Generator for software registration or licensing.
//See README.txt for more information and LICENSE.txt for usage.

using System;
using System.Threading;

public class ProductKeyGen
{
	static public void Main(string[] args)
	{
		if(args.Length == 0){
			ProductKey NewKey = new ProductKey();
			Console.Write("Generating key...\nYour key is: {0}\n", NewKey.GenerateKey());
		}	
		else if(args.Length == 1 && (args[0] == "/?" || args[0] == "--help"))
		{
			Console.WriteLine("Generates a software key with a random number generator. (v.1.0)");
			Console.WriteLine("Usage: SoftwareKeyGen.exe [/num \"number\"] [/seed \"SeedValue\"]\n");
			Console.WriteLine("\t/num\tCreates the given number of product keys.");
			Console.WriteLine("\t/seed\tPasses a seed value to the random number generator.\n");
			Console.WriteLine("Author: Joseph Castrejon (Repository: http://www.github.com/Joseph-Castrejon/SoftwareKeyGen/)");
			Console.WriteLine("See README.txt for general usage and LICENSE.txt for licensing information.");
		}
		else if(args.Length == 2)
		{
			switch(args[0])
			{
				case "/num":
					try{
						ProductKey NewKey = new ProductKey();
						Int32 NumOfKeys = Convert.ToInt32(args[1]);
						if(NumOfKeys == 1)
							Console.WriteLine("Generating a product / license key...");
						else
							Console.WriteLine("Generating {0} keys...",args[1]);

						for(int x = 0; x < NumOfKeys;x++)
						{
							//Sleep the main thread for 10 milliseconds in order to generate distinct keys.
							Thread.Sleep(10);
							Console.WriteLine(NewKey.GenerateKey());
						}
					}
					catch(System.FormatException FE)
					{
						Console.ForegroundColor = ConsoleColor.DarkYellow;
						Console.WriteLine("PROGRAM EXCEPTION: {0}", FE.Message);
						Console.ResetColor();
					}
					break;
				case "/seed":
					try{				    	
						Int32 SeedValue = Convert.ToInt32(args[1]);
				    	   	Console.WriteLine("Generating product / license key from seed value : {0}", SeedValue);
						ProductKey SeededKey = new ProductKey();
						Console.WriteLine(SeededKey.GenerateKey(SeedValue));
					}
					catch(System.FormatException FE)
					{
						Console.ForegroundColor = ConsoleColor.DarkYellow;
						Console.WriteLine("PROGRAM EXCEPTION: {0}", FE.Message);
						Console.ResetColor();
					}
					break;
				default:
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Unrecognized command arguments.");
					Console.ResetColor();
					Environment.Exit(-1);
					break;
			}	
			
		}
		else if(args.Length == 4)
		{
			if(args[0] == "/num" && args[2] == "/seed")
			{
				try
				{
					ProductKey NewKey = new ProductKey();
					Int32 NumOfKeys = Convert.ToInt32(args[1]);
					Int32 SeedValue = Convert.ToInt32(args[3]);

					if(NumOfKeys == 1)
						Console.WriteLine("Generating a product / license key from seed value: {0}",SeedValue);
					else
						Console.WriteLine("Generating {0} keys with seed value : {1}",NumOfKeys,SeedValue);

					for(int x=0; x < NumOfKeys; x++)
					{
						//Sleep the main thread for 10 milliseconds in order to generate distinct keys.
						Thread.Sleep(10);
						//Add x to seed value to generate distinct keys
						//You can do your own operation on the seeded value here if you'd like.
						//just be sure that the GenerateKey function does not receive the same value more than once.
						//Otherwise, duplicate keys will be created.
						Console.WriteLine(NewKey.GenerateKey(SeedValue + x));	
					}			
				}
				catch(System.FormatException FE)
				{
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine("PROGRAM EXCEPTION: {0}", FE.Message);
					Console.ResetColor();

				}
			}
			else if(args[0] == "/seed" && args[2] == "/num")
			{
				try
				{	
					ProductKey NewKey = new ProductKey();
					Int32 NumOfKeys = Convert.ToInt32(args[3]);
					Int32 SeedValue = Convert.ToInt32(args[1]);

					if(NumOfKeys == 1)
						Console.WriteLine("Generating a product / license key from seed value: {0}",SeedValue);
					else
						Console.WriteLine("Generating {0} keys with seed value : {1}",NumOfKeys,SeedValue);

					for(int x=0; x < NumOfKeys; x++)
					{
						Thread.Sleep(10);
						//Add x to seed value to generate distinct keys	
						//You can do your own operation on the seeded value here if you'd like.
						//just be sure that the GenerateKey function does not receive the same value more than once.
						//Otherwise, duplicate keys will be created.
						Console.WriteLine(NewKey.GenerateKey(SeedValue + x));	
					}
				}
				catch(System.FormatException FE)
				{
					Console.ForegroundColor = ConsoleColor.DarkYellow;
					Console.WriteLine("PROGRAM EXCEPTION: {0}", FE.Message);
					Console.ResetColor();

				}	
			}
			else{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Unrecognized command arguments.");
				Console.ResetColor();
				Environment.Exit(-1);
			}
		}
		else{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Unrecognized command arguments.");
			Console.ResetColor();
			Environment.Exit(-1);
		}

	}
}
	
	
public class ProductKey{
	
	static string KeySeperator = "-";
	
	///<summary>
	///Generate a product key with the system time, instead of a seed value. 
	///</summary>
	public string GenerateKey()
	{
		string ProductKey = "";
		
		Random KeyDigit = new Random();
		
		for(int x = 0; x < 25; x++)
		{
			//Decide whether the next digit will be a number or an english character.
			int GenerateDigit = KeyDigit.Next(2);
							
			if(GenerateDigit == 0)
			{
				char KeyChar;
				KeyChar = Convert.ToChar(KeyDigit.Next(65,91));
				ProductKey += KeyChar;	
			}
			else if(GenerateDigit == 1)
			{
				byte KeyNum;
				KeyNum = (byte) KeyDigit.Next(0,10);
				ProductKey += KeyNum;
			}
		}
		
		//This probably isn't the best way to hypenate the key, but it works.
		return ProductKey.Insert(5,KeySeperator).Insert(11,KeySeperator).Insert(17,KeySeperator).Insert(23,KeySeperator);
	}
	
	///<summary>
	///Generate a product key using a random number generator with a seed value.
	///</summary>
	public string GenerateKey(Int32 SeedValue)
	{
		string ProductKey = "";
		Random KeyDigit = new Random(SeedValue);
		
		for(int x = 0; x < 25; x++)
		{
			//Decide whether the next digit will be a number or an english character.
			int GenerateDigit = KeyDigit.Next(2);
					
			if(GenerateDigit == 0)
			{
				char KeyChar;
				KeyChar = Convert.ToChar(KeyDigit.Next(65,91));
				ProductKey += KeyChar;	
			}
			else if(GenerateDigit == 1)
			{
				byte KeyNum;
				KeyNum = (byte) KeyDigit.Next(0,10);
				ProductKey += KeyNum;
			}
		}
		
		//This probably isn't the best way to hypenate the key, but it works.
		return ProductKey.Insert(5,KeySeperator).Insert(11,KeySeperator).Insert(17,KeySeperator).Insert(23,KeySeperator);

	}
	
}


