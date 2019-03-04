using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Web;
using HijackMonitoringSystem.DataAccessLayer.Core.Context;
using HijackMonitoringSystem.DataAccessLayer.Core.Repositories.Interface;
using HijackMonitoringSystem.DataAccessLayer.Entities.GeneralData;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HijackMonitoringSystem.DataAccessLayer.Core.Repositories
{
    public class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : IPersistenceEntities
    {
        protected readonly MongoDbContext Context;

        private IMongoCollection<TEntity> _collection;
        public IMongoCollection<TEntity> Collection => _collection;

        public Repository(MongoDbContext context)
        {
            this.Context = context;
            _collection = Context.Database.GetCollection<TEntity>(typeof(TEntity).Name);
        }
        public bool CollectionExists(IMongoCollection<TEntity> collectionName)
        {
            var filter = new BsonDocument("name", collectionName.CollectionNamespace.CollectionName);
            //filter by collection name
            var collections = Context.Database.ListCollections(new ListCollectionsOptions { Filter = filter });
            //check for existence
            return collections.Any();
        }
        public TEntity AddOrEdit(TEntity entity)
        {
            if (!CollectionExists(_collection))
            {
                Context.Database.CreateCollection(_collection.CollectionNamespace.CollectionName);
            }
            if (string.IsNullOrEmpty(entity.Id))
            {
                //insert
                entity.CreateDate = DateTime.Now.ToLocalTime();
                entity.ModifyDate = DateTime.Now.ToLocalTime();
                _collection.InsertOne(entity);
            }
            else
            {
                //update
                entity.ModifyDate = DateTime.Now.ToLocalTime();
                _collection.FindOneAndReplace(x => x.Id == entity.Id, entity);// .ReplaceOne(filter, entity);
            }
            return entity;
        }
        public bool Remove(string id)
        {
            _collection.DeleteOne(x => x.Id == id);
            return true;
        }
        public IQueryable<TEntity> GetAll()
        {
            return _collection.AsQueryable<TEntity>();//.ToListAsync(); ;
        }
        public TEntity GetById(string id)
        {
            return _collection.Find(x => x.Id == id).First();//.ToListAsync(); ;
        }
        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> func)
        {
            return _collection.AsQueryable<TEntity>().Where(func);//.ToEnumerable<T>();//.AsQueryable<T>();
        }
        public IEnumerable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}