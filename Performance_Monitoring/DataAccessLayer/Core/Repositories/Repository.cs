using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using Performance_Monitoring.DataAccessLayer.Core.Context;
using Performance_Monitoring.DataAccessLayer.Core.Repositories.Interface;
using Performance_Monitoring.DataAccessLayer.Entities.GeneralData;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Performance_Monitoring.DataAccessLayer.Core.Repositories
{
    public class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : IPersistenceEntities
    {
        protected readonly MongoDbContext Context;

        public Repository(MongoDbContext context)
        {
            this.Context = context;
        }
        private string GetFriendlyNameForCollection()
        {
            var collection = "";
            var displayName = typeof(TEntity).GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() as DisplayNameAttribute;
            if (displayName != null)
                collection = displayName.DisplayName;
            return collection;
        }

        public bool CollectionExists(string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            //filter by collection name
            var collections = Context.Database.ListCollections(new ListCollectionsOptions { Filter = filter });
            //check for existence
            return collections.Any();
        }
        public TEntity AddOrEdit(TEntity entity)
        {
            var collection = GetFriendlyNameForCollection();
            var existedCollection = Context.Database.GetCollection<TEntity>(collection);

            if (!CollectionExists(collection))
            {
                Context.Database.CreateCollection(collection);
            }
            if (string.IsNullOrEmpty(entity.Id))
            {
                //insert
                entity.CreateDate = DateTime.Now.ToLocalTime();
                entity.ModifyDate = DateTime.Now.ToLocalTime();
                existedCollection.InsertOne(entity);
            }
            else
            {
                //update
                entity.ModifyDate = DateTime.Now.ToLocalTime();
                existedCollection.FindOneAndReplace(x => x.Id == entity.Id, entity);// .ReplaceOne(filter, entity);
            }
            return entity;
        }
        public bool Remove(string id)
        {
            var collection = GetFriendlyNameForCollection();
            var d = Context.Database.GetCollection<TEntity>(collection);
            d.FindOneAndDelete(x => x.Id == id);// .ReplaceOne(filter, entity);
            return true;
        }
        public IQueryable<TEntity> GetAll()
        {
            var collection = GetFriendlyNameForCollection();
            var existedCollection = Context.Database.GetCollection<TEntity>(collection);
            //var filter = new BsonDocument();
            //var filter = Builders<T>.Filter;
            return existedCollection.AsQueryable<TEntity>();//.ToListAsync(); ;
            //throw new NotImplementedException();
        }
        public TEntity GetById(string id)
        {
            var collection = GetFriendlyNameForCollection();
            var existedCollection = Context.Database.GetCollection<TEntity>(collection);
            return existedCollection.Find(x => x.Id == id).First();//.ToListAsync(); ;
        }
        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> func)
        {
            var collection = GetFriendlyNameForCollection();
            var existedCollection = Context.Database.GetCollection<TEntity>(collection);
            return existedCollection.AsQueryable<TEntity>().Where(func);//.ToEnumerable<T>();//.AsQueryable<T>();
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