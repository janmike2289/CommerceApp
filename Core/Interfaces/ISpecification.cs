﻿using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecification<T>
{
    //Contract for passing a "where" expression as the criteria 
    Expression<Func<T, bool>>? Criteria { get; }
    
    Expression<Func<T, object>>? OrderBy { get; }
    
    Expression<Func<T, object>>? OrderByDescending { get; }
    
    bool IsDistinct { get; }
    
    int Take { get; }
    
    int Skip { get; }
    
    bool IsPagingEnabled { get; }
    
    public IQueryable<T> ApplyCriteria(IQueryable<T> query) 
    {
        if(Criteria != null)
        {
            query = query.Where(Criteria);
        }

        return query;
    }
}


public interface ISpecification<T, TResult> : ISpecification<T>
{
    Expression<Func<T, TResult>>? Select { get; }
}