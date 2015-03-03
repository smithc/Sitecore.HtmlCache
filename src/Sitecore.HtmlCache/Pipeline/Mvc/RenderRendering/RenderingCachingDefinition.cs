using Sitecore.Mvc.Extensions;
using Sitecore.Mvc.Presentation;

namespace Sitecore.HtmlCache.Pipeline.Mvc.RenderRendering
{
	/// <summary>
	/// Extends Sitecore's RenderingCachingDefinition to include two more caching options
	/// </summary>
	public class RenderingCachingDefinition : Sitecore.Mvc.Presentation.RenderingCachingDefinition
	{
		public RenderingCachingDefinition(Rendering rendering) : base(rendering)
		{
		}

		public bool VaryByCustom
		{
			get { return Rendering.RenderingItem.InnerItem["VaryByCustom"].ToBool(); }
		}

		public string VaryByCustomImplementationType
		{
			get { return Rendering.RenderingItem.InnerItem["VaryByCustomType"]; }
		}

		public bool CacheGetOnly
		{
			get { return Rendering.RenderingItem.InnerItem["CacheGETOnly"].ToBool(); }
		}
	}
}
