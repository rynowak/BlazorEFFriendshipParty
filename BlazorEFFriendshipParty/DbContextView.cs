using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Components
{
    // Blazor ships this
    public class DbContextView<T> : ComponentBase, IDisposable where T : DbContext
    {
        [Inject]
        private IServiceProvider Services { get; set; }
        private OwningHandle<T> Handle { get; set; }
        protected T DbContext => Handle?.Item;

        protected override void OnInit()
        {
            Handle = Services.GetOwnedInstance<T>();
        }

        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
