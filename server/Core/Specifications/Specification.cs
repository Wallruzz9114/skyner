using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications
{
    public class Specification<T> : ISpecification<T>
    {
        public Specification() { }

        public Specification(Expression<Func<T, bool>> creteria)
        {
            Creteria = creteria;
        }

        public Expression<Func<T, bool>> Creteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}