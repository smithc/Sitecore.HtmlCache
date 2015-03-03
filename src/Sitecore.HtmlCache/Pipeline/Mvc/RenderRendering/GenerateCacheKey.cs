using System;
using System.Web.Mvc;
using Sitecore.HtmlCache.CustomCache;
using Sitecore.Mvc.Pipelines.Response.RenderRendering;
using Sitecore.Mvc.Presentation;

namespace Sitecore.HtmlCache.Pipeline.Mvc.RenderRendering
{
    public class GenerateCacheKey : Sitecore.Mvc.Pipelines.Response.RenderRendering.GenerateCacheKey
    {
	    protected override string GenerateKey(Rendering rendering, RenderRenderingArgs args)
	    {
		    string key = base.GenerateKey(rendering, args);

		    if (!string.IsNullOrEmpty(key))
		    {
			    RenderingCachingDefinition definition = new RenderingCachingDefinition(rendering);
			    if (definition.VaryByCustom)
			    {
				    key = string.Format("{0}{1}", key, GetCustomPart(rendering, definition));
			    }
		    }

		    return key;
	    }

			protected virtual string GetCustomPart(Rendering rendering, RenderingCachingDefinition definition)
	    {
				object typeImplementation = GetInstanceOfType(definition.VaryByCustomImplementationType);

				if (typeImplementation == null) return string.Empty;

				ICustomCacheable keyProvider = typeImplementation as ICustomCacheable;

		    if (keyProvider == null) return string.Empty;

				return string.Format("_#custom:{0}", keyProvider.GetCustomCacheKeyPart());
	    }

	    protected virtual object GetInstanceOfType(string className)
	    {
		    Type type = Type.GetType(className);

		    if (type == null) return null;

		    object typeImplementation = DependencyResolver.Current.GetService(type);

		    if (typeImplementation == null)
		    {
			    typeImplementation = Activator.CreateInstance(type);
		    }

				return typeImplementation;
	    }
    }
}
