<Query Kind="Statements" />

// https://paragonie.com/blog/2016/05/how-generate-secure-random-numbers-in-various-programming-languages#dotnet-csprng

var csprng = new System.Security.Cryptography.RNGCryptoServiceProvider();
byte[] rawByteArray = new byte[32];
csprng.GetBytes(rawByteArray);
rawByteArray.Dump();
System.BitConverter.ToInt64(rawByteArray, 0).Dump();