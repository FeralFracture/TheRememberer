using System.Linq.Expressions;
using TheRememberer.Objects.DTOs;
using TheRememberer.Objects.Entities;

namespace TheRememberer.Objects.Interfaces.Bases
{
    public interface IRepoBase<TEntity, TDto>
        where TDto : DtoBase, new()
        where TEntity : EntityBase, new()
    {
        IEnumerable<TDto> GetAll();
        bool Contains(Expression<Func<TEntity, bool>> expression);
        TDto? GetByGUID(Guid id);
        IEnumerable<TDto> SelectBy(Expression<Func<TEntity, bool>> expression);
        void Upsert(TDto model, Expression<Func<TEntity, bool>>? matchPredicate = null);
        void Delete(TDto? model);
        void Delete(Guid id);
    }
}
