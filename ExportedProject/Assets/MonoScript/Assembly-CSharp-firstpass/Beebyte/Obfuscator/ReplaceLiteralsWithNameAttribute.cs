using System;

namespace Beebyte.Obfuscator
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event)]
	public class ReplaceLiteralsWithNameAttribute : Attribute
	{
	}
}
