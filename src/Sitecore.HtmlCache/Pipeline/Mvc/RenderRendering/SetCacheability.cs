using Sitecore.Mvc.Pipelines.Response.RenderRendering;
using Sitecore.Mvc.Presentation;

namespace Sitecore.HtmlCache.Pipeline.Mvc.RenderRendering
{
	public class SetCacheability : Sitecore.Mvc.Pipelines.Response.RenderRendering.SetCacheability
	{
		/// <summary>
		/// Determines whether the specified rendering is cacheable.  Extending Sitecore functionality to check if 
		/// this shoudl be cached for GET requests.  If Request.HttpMethod != "GET, return false.
		/// </summary>
		/// <param name="rendering">The rendering.</param>
		/// <param name="args">The arguments.</param>
		/// <returns></returns>
		protected override bool IsCacheable(Rendering rendering, RenderRenderingArgs args)
		{
			var isCacheable = base.IsCacheable(rendering, args);
			if (isCacheable)
			{
				RenderingCachingDefinition definition = new RenderingCachingDefinition(rendering);
				if (definition.CacheGetOnly && args.PageContext.RequestContext.HttpContext.Request.HttpMethod != "GET")
				{
					return false;
				}
			}

			return isCacheable;
		}
	}
}
