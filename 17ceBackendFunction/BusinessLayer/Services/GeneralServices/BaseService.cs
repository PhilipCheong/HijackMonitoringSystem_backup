using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using _17ceBackendFunction.BusinessLayer.Dtos.GeneralData;
using _17ceBackendFunction.DataAccessLayer.Core.Repositories.Interface;
using _17ceBackendFunction.DataAccessLayer.Entities.GeneralData;

namespace _17ceBackendFunction.BusinessLayer.Services.GeneralServices
{
    public class BaseService<T, TDto> : IService<T, TDto> where T : IPersistenceEntities where TDto : IPersistenceDtos
    {
        public IRepository<T> _repository;
        public List<TDto> GetAll()
        {
            List<T> data = _repository.GetAll().ToList();

            List<TDto> returnData = new List<TDto>();// new List<Activator.CreateInstance<Dto>> ();
            foreach (var d in data)
            {
                TDto temp = Activator.CreateInstance<TDto>();
                Mapping.MapProp(d, temp);
                returnData.Add(temp);
            }
            return returnData;
        }

        public TDto GetById(string id)
        {
            T data = _repository.GetById(id);
            TDto returnData = Activator.CreateInstance<TDto>();
            Mapping.MapProp(data, returnData);
            return returnData;
        }
        public TDto AddOrEdit(TDto entity)
        {
            T sourceData = Activator.CreateInstance<T>();
            Mapping.MapProp(entity, sourceData);

            T data = _repository.AddOrEdit(sourceData);

            TDto returnData = entity;
            Mapping.MapProp(data, returnData);
            return returnData;
        }
        public List<TDto> Find(Expression<Func<T, bool>> predict)
        {
            List<T> data = _repository.Find(predict).ToList();

            List<TDto> returnData = new List<TDto>();// new List<Activator.CreateInstance<TDto>> ();
            foreach (var d in data)
            {
                TDto temp = Activator.CreateInstance<TDto>();
                Mapping.MapProp(d, temp);
                returnData.Add(temp);
            }
            return returnData;
        }
        public bool Remove(string id)
        {
            return _repository.Remove(id);
        }

    }
}