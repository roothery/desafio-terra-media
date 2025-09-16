﻿namespace Desafio.TerraMedia.Domain.Specifications;

public interface ISpecification<T>
{
    bool IsSatisfiedBy(T entity);
}
