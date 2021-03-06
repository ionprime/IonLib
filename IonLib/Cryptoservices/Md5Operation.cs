﻿using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace IonLib.cryptoservices
{
	public static class Md5Operation
	{
		public static string GetMD5(string input, bool isFile = false)
		{
			if (!isFile)
			{
				var encoding = new UTF8Encoding();
				byte[] bytes = encoding.GetBytes(input);
				var md5 = new MD5CryptoServiceProvider();
				byte[] hashBytes = md5.ComputeHash(bytes);
				string hashString = hashBytes.Aggregate(string.Empty, (current, t) => current + Convert.ToString(t, 16).PadLeft(2, '0'));

				return hashString.PadLeft(32, '0');
			}

			try
			{
				var file = new FileStream(input, FileMode.Open);
				byte[] retVal = new MD5CryptoServiceProvider().ComputeHash(file);
				file.Close();
				var sb = new StringBuilder();

				for (int i = 0; i < retVal.Length; i++)
				{
					sb.Append(i.ToString("x2"));
				}

				return sb.ToString();
			}
			catch
			{
				return null;
			}
		}

		public static string GetMD5OfBytesArray(byte[] array)
		{
			byte[] hashBytes = new MD5CryptoServiceProvider().ComputeHash(array);
			string hashString = hashBytes.Aggregate(string.Empty, (current, t) => current + Convert.ToString(t, 16).PadLeft(2, '0'));

			return hashString.PadLeft(32, '0');
		}
	}
}