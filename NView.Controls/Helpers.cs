using System;

namespace NView.Controls
{
	internal static class Helpers
	{
		const string NotImplemented = "This functionality is not implemented in the portable version of this assembly.  You should reference the NView.Controls NuGet package from your application in order to reference the platform-specific implementation.";

		public static NotImplementedException ThrowNotImplementedException()
		{
			return new NotImplementedException (NotImplemented);
		}
	}
}

