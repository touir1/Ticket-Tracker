﻿using Corp.ERP.Common.Domain.Contract.Models;

namespace Corp.ERP.Common.Application.Repositories;

public interface IRepositoryService<T> where T : IEntity
{
    IList<T> GetAll();
    T GetById(Guid id);
    IList<T> GetAll(Predicate<T> predicate);
    T GetFirstOrDefault(Predicate<T> predicate, T defaultValue);
    void Update(T entity);
    void Delete(T entity);
    void Add(T entity);
}