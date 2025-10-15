using System.Linq.Expressions;
using TheRememberer.Objects.DTOs;
using TheRememberer.Objects.Entities;

namespace TheRememberer.Objects.Interfaces.Bases
{
    public interface IBizBase<TEntity, TDto>
        where TDto : DtoBase, new()
        where TEntity : EntityBase, new()
    {
        IEnumerable<TDto> GetAll();
        TDto? Get(Guid id);
        void Upsert(TDto model, Expression<Func<TEntity, bool>>? matchPredicate = null);
        void Delete(TDto? model);
        void Delete(Guid id);
    }
}
