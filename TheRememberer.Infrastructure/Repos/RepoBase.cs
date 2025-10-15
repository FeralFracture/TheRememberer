using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using TheRememberer.Infrastructure.Data;
using TheRememberer.Objects.DTOs;
using TheRememberer.Objects.Entities;
using TheRememberer.Objects.Interfaces.Bases;

namespace TheRememberer.Infrastructure.Repos
{
    public abstract class RepoBase<TEntity, TDto> : IRepoBase<TEntity, TDto>
        where TEntity : EntityBase, new()
        where TDto : DtoBase, new()
    {
        protected readonly AppDbContext _context;
        protected readonly IMapper _mapper;
        protected readonly ILogger<IRepoBase<TEntity, TDto>> _logger;

        public RepoBase(AppDbContext context, IMapper mapper, ILogger<IRepoBase<TEntity, TDto>> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public virtual bool Contains(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().FirstOrDefault(expression) != null;
        }

        public virtual void Delete(TDto? dto)
        {
            if (dto != null)
            {
                var transaction = _context.Database.CurrentTransaction == null ? _context.Database.BeginTransaction() : null;
                try
                {
                    var entity = _mapper.Map<TEntity>(dto);
                    var primaryKey = GetPrimaryKey(entity);
                    var existingEntity = _context.Set<TEntity>().Find(primaryKey);
                    if (existingEntity != null)
                    {
                        _context.Set<TEntity>().Remove(existingEntity);
                        _context.SaveChanges();
                    }
                }
                catch
                {
                    transaction?.Rollback();
                    throw;
                }
            }
        }

        public virtual void Delete(Guid id)
        {
            var transaction = _context.Database.CurrentTransaction == null ? _context.Database.BeginTransaction() : null;
            try
            {
                var entity = _context.Set<TEntity>().Find(id);
                _context.Set<TEntity>().Remove(entity!);
                _context.SaveChanges();
                _context.Database.CommitTransaction();
            }
            catch
            {
                transaction?.Rollback();
                throw;
            }
        }

        public virtual IEnumerable<TDto> GetAll()
        {
            var entities = _context.Set<TEntity>();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual TDto? GetByGUID(Guid id)
        {
            var entity = _context.Set<TEntity>().Find(id);
            return entity == null ? null : _mapper.Map<TDto>(entity);
        }

        public virtual IEnumerable<TDto> SelectBy(Expression<Func<TEntity, bool>> expression)
        {
            var entities = _context.Set<TEntity>().Where(expression);
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual Guid? Upsert(TDto dto, Expression<Func<TEntity, bool>>? matchPredicate = null)
        {
            var transaction = _context.Database.CurrentTransaction == null ? _context.Database.BeginTransaction() : null;

            try
            {
                TEntity? entity = null;
                dto.UpdatedAt = DateTime.UtcNow;

                if (matchPredicate != null)
                {
                    entity = _context.Set<TEntity>().FirstOrDefault(matchPredicate);
                }

                if (entity == null)
                {
                    entity = _mapper.Map<TEntity>(dto);
                    entity.CreatedAt = DateTime.UtcNow;
                    _context.Set<TEntity>().Add(entity);
                }
                else
                {
                    _mapper.Map(dto, entity);
                }

                _context.SaveChanges();
                _context.Database.CommitTransaction();

                return entity.Id;
            }
            catch
            {
                transaction?.Rollback();
                throw;
            }
        }

        private object GetPrimaryKey(TEntity entity)
        {
            var keyProperties = _context.Model.FindEntityType(typeof(TEntity))!.FindPrimaryKey()!.Properties;
            var primaryKey = new object[keyProperties.Count];
            var i = 0;
            foreach (var keyProperty in keyProperties)
            {
                primaryKey[i++] = entity.GetType().GetProperty(keyProperty.Name)?.GetValue(entity)!;
            }
            return primaryKey[0]; // Assuming single-column primary key
        }
    }
}
