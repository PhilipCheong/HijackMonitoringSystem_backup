using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Performance_Monitoring.BusinessLayer.Dtos.GeneralData;
using Performance_Monitoring.DataAccessLayer.Entities.GeneralData;

namespace Performance_Monitoring.BusinessLayer.Services.GeneralServices
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