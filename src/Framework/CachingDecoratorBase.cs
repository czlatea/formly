using System;
using Microsoft.Extensions.Caching.Memory;

namespace Framework
{
  public abstract class CachingDecoratorBase<T>
    where T : class
  {
    private readonly IMemoryCache mCache;

    protected CachingDecoratorBase(IMemoryCache cache, T inner)
    {
      Check.Assigned(cache, nameof(cache));
      Check.Assigned(inner, nameof(inner));

      mCache = cache;
      Inner = inner;
    }

    public T Inner { get; }

    protected TValueType GetOrCreate<TValueType>(object key, Func<TValueType> factory)
    {
      return mCache.GetOrCreate(key, cacheEntry =>
      {
        cacheEntry.AbsoluteExpiration = DateTimeOffset.MaxValue;
        return factory();
      });
    }
  }
}