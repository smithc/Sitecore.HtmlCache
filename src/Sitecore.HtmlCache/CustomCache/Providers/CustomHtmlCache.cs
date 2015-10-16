using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Web;

namespace Sitecore.HtmlCache.CustomCache.Providers
{
	public class CustomHtmlCache : Caching.HtmlCache, IStaleCacheProvider
	{
		public CustomHtmlCache(SiteInfo site, long maxSize) : base(site, maxSize)
		{
		}
	}
}
