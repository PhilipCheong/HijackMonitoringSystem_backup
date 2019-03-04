using System;
using System.Linq;
using System.Reflection;

namespace Performance_Monitoring.BusinessLayer
{
    public class Mapping
    {
        public static void MapProp(object sourceObj, object targetObj)
        {
            Type T1 = sourceObj.GetType();
            Type T2 = targetObj.GetType();

            PropertyInfo[] sourceProprties = T1.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            PropertyInfo[] targetProprties = T2.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var sourceProp in sourceProprties)
            {
                object osourceVal = sourceProp.GetValue(sourceObj, null);
                if (targetProprties.Any(x => x.Name == sourceProp.Name))
                {
                    var targetProp = targetProprties.First(x => x.Name == sourceProp.Name);
                    if (targetProp != null)
                        targetProp.SetValue(targetObj, osourceVal, null);
                }
            }
        }

    }
}