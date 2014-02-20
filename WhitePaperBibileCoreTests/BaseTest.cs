using System;

namespace WhitePaperBibileCoreTests
{
	public abstract class BaseTest
	{
		protected string TestIntent {
			get {
				return NUnit.Framework.TestContext.CurrentContext.Test.Properties ["Intent"] as string;
			}
		}
	}
}

