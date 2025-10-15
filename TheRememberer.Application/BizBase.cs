using System.Linq.Expressions;
using TheRememberer.Objects.DTOs;
using TheRememberer.Objects.Entities;
using TheRememberer.Objects.Interfaces.Bases;

namespace TheRememberer.Application
{
    public abstract class BizBase<TEntity, TDto, TRepo> : IBizBase<TEntity, TDto>
        where TEntity : EntityBase, new()
        where TDto : DtoBase, new()
        where TRepo : IRepoBase<TEntity, TDto>
        

    {
        protected readonly TRepo _repo;

        public BizBase(TRepo repo)
        {
            _repo = repo;
        }

        public void Delete(TDto? model) => _repo.Delete(model);

        public void Delete(Guid id) => _repo.Delete(id);

        public TDto? Get(Guid id)
        {
            return _repo.GetByGUID(id);
        }

        public IEnumerable<TDto> GetAll()
        {
            return _repo.GetAll();
        }

        public void Upsert(TDto model, Expression<Func<TEntity, bool>>? matchPredicate = null) => _repo.Upsert(model, matchPredicate);
    }
}
