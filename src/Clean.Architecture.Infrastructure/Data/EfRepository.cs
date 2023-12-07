﻿using Ardalis.Specification.EntityFrameworkCore;
using Clean.Architecture.SharedKernel.Interfaces;

namespace Clean.Architecture.Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
  public EfRepository(AppDbContext dbContext) : base(dbContext)
  {
  }
}
