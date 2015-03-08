# Sitecore.HtmlCache

Extending functionality of Sitecore's HTML Cache

## 1. Cache Only GET Requests

I've run into a number of scenarios where I could not cache a component purely because it contained a form or handled a postback operation (Think of a header component containing a textbox for site search).  Sometimes this can be avoided by breaking the component into multiple components, where you minimize the amount of the page that can be cached.  Often, however, refactoring the component can be difficult or impossible.  I've added a checkbox to the 'Caching' section of the 'Rendering' template.  When this checkbox is selected, all cacheablity options will be respected for GET requests but ignored for all other request types.

![Cache Only GET Requests](http://optimizedquery.com/optimizedquery/wp-content/uploads/2015/03/cacheget-300x282.png)

## 2. Vary By Custom

Another caching dilemma arises when the existing 'Var By' options do not meet the needs of my component.  Thus, I'm left with a component I can't add to the HTML cache.  To solve this, I've added a new 'Vary By' option, called 'Vary By Custom'.  Selecting this option, coupled with your own custom cache definition, can cache whatever you'd like.

![Vary By Custom](http://optimizedquery.com/optimizedquery/wp-content/uploads/2015/03/varybycustom-300x282.png)

The value of the 'Vary By Custom Type' field is required when selecting 'Vary By Custom' and should follow the format: 'Namespace.ClassName, Assembly'.  The class definition provided will need to implement the `ICustomCacheable` interface.  The `ICustomCacheable` interface looks like this:

```
namespace Sitecore.HtmlCache.CustomCache
{
  public interface ICustomCacheable
  {
    string GetCustomCacheKeyPart();
  }
}
```

Just implement `GetCustomCacheKeyPart()` as needed and you are ready to go.  When the module tries to create an instance of your custom class, it first tries to use ASP.NET MVC's DependencyResolver.  So, if you are making use of an IoC container, you can make use of dependency injection in your custom class.