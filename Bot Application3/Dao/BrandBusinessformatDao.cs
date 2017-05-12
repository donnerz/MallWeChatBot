using Bot_Application3.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Dao
{
    public class BrandBusinessformatDao
    {
        public List<t_brand_businessformat> getBusinessformatByBrandAndBf(String brandCode, String businessformatCode)
        {
            crm1Entities context = new crm1Entities();
            var q = from t in context.t_brand_businessformat
                    select t;

            if (!String.IsNullOrWhiteSpace(brandCode))
                q = q.Where(p => p.brand_code == brandCode);

            if (!String.IsNullOrWhiteSpace(businessformatCode))
                q = q.Where(p => p.businessformat_word_code == businessformatCode);

            List<t_brand_businessformat> list = q.ToList();
            return list;
        }

        public void add(t_brand_businessformat po)
        {
            crm1Entities context = new crm1Entities();
            //3.将改对象放入EF容器中，默认会为该对象加一个封装类对象（代理类对象）
            //用户对对象的操作，实际上是对代理类的操作
            //DbEntityEntry保存着实体状态，当对象被加入时，EF默认为该对象设置State的属性为unchanged
            DbEntityEntry<t_brand_businessformat> entityEntry = context.Entry<t_brand_businessformat>(po);
            //4.设置对象的标志位Added
            entityEntry.State = EntityState.Added;
            //5.当调用SaveChanges()时，EF会遍历所有的代理类对象，并根据标志生成相应的sql语句
            context.SaveChanges();
            //Console.WriteLine("添加成功");
        }
    }
}