using System;
using System.Linq;
using System.Reflection;
using Sitecore.Configuration;
using Sitecore.Events.Hooks;
using Sitecore.HtmlCache.CustomCache.Providers;
using Sitecore.Web;

namespace Sitecore.HtmlCache.Hooks
{
	public class InitializeHtmlCacheHook : IHook
	{
		private static readonly Type SiteInfoType = typeof (SiteInfo);
		private const string FieldName = "htmlCache";

		public void Initialize()
		{
			var siteInfos = Settings.GetSetting("Sitecore.HtmlCache.Sites", string.Empty)
				.Split('|')
				.Where(s => !string.IsNullOrEmpty(s))
				.Select(Factory.GetSiteInfo)
				.Where(info => info != null);

			foreach (var info in siteInfos)
			{
				ReplaceHtmlCache(info);
			}
		}

		private void ReplaceHtmlCache(SiteInfo info)
		{
			try
			{
				var field = SiteInfoType.GetField(FieldName, BindingFlags.GetField | BindingFlags.Instance | BindingFlags.NonPublic);
				if (field == null)
				{
					// TODO: log
					return;
				}

				var existingCache = info.HtmlCache;
				field.SetValue(info, new CustomHtmlCache(info, existingCache.InnerCache.MaxSize));
			}
			catch
			{
				// TODO: log
			}
		}
	}
}
