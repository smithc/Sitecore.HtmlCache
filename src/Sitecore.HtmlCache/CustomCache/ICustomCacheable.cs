using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.HtmlCache.CustomCache
{
	public interface ICustomCacheable
	{
		string GetCustomCacheKeyPart();
	}
}
