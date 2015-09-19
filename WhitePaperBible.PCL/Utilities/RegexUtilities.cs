using System;
using System.Globalization;
using System.Text.RegularExpressions;

public static class RegexUtilities
{
	public static bool IsValidEmail(string email)
	{
		string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" 
			+ @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" 
			+ @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

		Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
		return regex.IsMatch (email);
	}
}