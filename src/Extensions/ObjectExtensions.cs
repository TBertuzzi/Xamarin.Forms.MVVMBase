using System;
namespace Xamarin.Forms.MVVMBase.Extensions
{
    public static class ObjectExtensions
    {
        public static T Cast<T>(this object data)
        {
            var ret = (T)Activator.CreateInstance(typeof(T));

            foreach (var p in ret.GetType().GetProperties())
            {
                if (data.GetType().GetProperty(p.Name) != null)
                    p.SetValue(ret, data.GetType().GetProperty(p.Name)?.GetValue(data));
            }

            return ret;
        }
    }

}
