using Core.Entity;

namespace Core.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity // where T (generic object) is derived from base entity
{
    Task<T?> GetByIdAsync(int id); 
    
    Task<IReadOnlyList<T>> GetAllAsync();
    
    //specification pattern methods
    Task<T?> GetEntityWithSpec(ISpecification<T> spec);
    
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

    Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> spec);

    Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec);

    void Add(T entity);
    
    void Update(T entity);
    
    void Remove(T entity);
    
    Task<bool> SaveChangesAsync();
    
    bool Exists(int id);
}