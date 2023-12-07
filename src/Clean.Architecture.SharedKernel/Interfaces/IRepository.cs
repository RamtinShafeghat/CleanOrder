using Ardalis.Specification;

namespace Clean.Architecture.SharedKernel.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}
