using Autofac;
using Clean.Architecture.Core.Interfaces;
using Clean.Architecture.Core.OrderAggregate.Validators;
using Clean.Architecture.Core.Services;

namespace Clean.Architecture.Core;

public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<OrderValidator>()
           .As<IOrderValidator>()
           .InstancePerLifetimeScope();

    builder.RegisterType<ProductSearchService>()
           .As<IProductSearchService>()
           .InstancePerLifetimeScope();
  }
}
