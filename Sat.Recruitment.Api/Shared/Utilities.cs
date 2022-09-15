using System;
using System.IO;

namespace Sat.Recruitment.Api.Shared
{
	public static class Utilities
	{
		public static StreamReader GetFileStream(string path)
		{
			FileStream fileStream = new FileStream(path, FileMode.Open);
			StreamReader reader = new StreamReader(fileStream);
			return reader;
		}

		public static string NormalizeEmail(string emailToBeNormalized)
		{
			var aux = emailToBeNormalized.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

			var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

			aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

			return string.Join("@", new string[] { aux[0], aux[1] });
		}

	}
}
