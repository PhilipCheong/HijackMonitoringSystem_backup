using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebAPI_PerformanceTest.Models.BusinessLayer.Dtos.GeneralData;
using WebAPI_PerformanceTest.Models.DataAccessLayer.Entities.GeneralData;

namespace WebAPI_PerformanceTest.Models.BusinessLayer.Services.GeneralServices
{
    public interface IService<T, TDto> where T : IPersistenceEntities where TDto : IPersistenceDtos
    {
        List<TDto> GetAll();
        TDto AddOrEdit(TDto entity);
        TDto GetById(string id);
        bool Remove(string id);
        //List<Dto> Find(Func<T,bool> predict);
        List<TDto> Find(Expression<Func<T, bool>> predict);

    }
}