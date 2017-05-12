using Bot_Application3.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Dao
{
    public class BrandDao
    {
        public List<t_brand> getAllData(bool VipFlag)
        {
            crm1Entities context = new crm1Entities();
            var q = from t in context.t_brand
                    select t;

            if (VipFlag)
                q = q.Where(p => p.vip_flag == 1);

            List<t_brand> list = q.ToList();
            return list;
        }

        public List<t_brand> getBrandByCondition(String[] brand, String floor, bool VipFlag)
        {
            crm1Entities context = new crm1Entities();
            var q = from t in context.t_brand
                    select t;

            if (null != brand && brand.Length > 0) 
                q = q.Where(p => brand.Contains(p.brand));

            if (!String.IsNullOrWhiteSpace(floor))
                q = q.Where(p => p.floor == floor);

            if (VipFlag)
                q = q.Where(p => p.vip_flag == 1);

            List<t_brand> list = q.ToList();
            return list;
        }

        public void add(t_brand po)
        {
            crm1Entities context = new crm1Entities();
            //3.将改对象放入EF容器中，默认会为该对象加一个封装类对象（代理类对象）
            //用户对对象的操作，实际上是对代理类的操作
            //DbEntityEntry保存着实体状态，当对象被加入时，EF默认为该对象设置State的属性为unchanged
            DbEntityEntry<t_brand> entityEntry = context.Entry<t_brand>(po);
            //4.设置对象的标志位Added
            entityEntry.State = EntityState.Added;
            //5.当调用SaveChanges()时，EF会遍历所有的代理类对象，并根据标志生成相应的sql语句
            context.SaveChanges();
            //Console.WriteLine("添加成功");
        }

    }
}