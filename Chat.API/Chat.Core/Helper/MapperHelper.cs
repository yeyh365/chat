using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Core.Helper
{
    public class MapperHelper
    {
        public static D Mapper<D, S>(S s, D d)
        {
            try
            {
                var Types = s.GetType();//获得类型
                var Typed = typeof(D);
                foreach (PropertyInfo sp in Types.GetProperties())//获得类型的属性字段
                {
                    foreach (PropertyInfo dp in Typed.GetProperties())
                    {
                        if (dp.Name == sp.Name)//判断属性名是否相同
                        {
                            dp.SetValue(d, sp.GetValue(s, null), null);//获得s对象属性的值复制给d对象的属性
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return d;
        }
    }
}
