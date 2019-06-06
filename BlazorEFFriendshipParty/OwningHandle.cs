using System;

// DI ships this
namespace Microsoft.Extensions.DependencyInjection
{
    public static class OwningHandleServiceProviderExtensions
    {
        public static OwningHandle<T> GetOwnedInstance<T>(this IServiceProvider services) where T : class
        {
            var factory = services.GetService<IOwningHandleFactory<T>>();
            if (factory != null)
            {
                return factory.Create();
            }

            var instance = ActivatorUtilities.CreateInstance<T>(services);
            return new OwningHandle<T>(instance);
        }
    }

    public interface IOwningHandleFactory<T> where T : class
    {
        OwningHandle<T> Create();
    }

    public sealed class OwningHandle<T> : IDisposable where T : class
    {
        private T _item;

        public OwningHandle(T item)
        {
            if (item == null) // come at me bro
            {
                throw new ArgumentNullException(nameof(item));
            }

            _item = item;
        }


        public T Item => _item ?? throw new ObjectDisposedException("YOLO");

        public void Dispose()
        {
            if (_item is IDisposable disposable)
            {
                disposable.Dispose();
                _item = null;
            }
        }
    }
}
