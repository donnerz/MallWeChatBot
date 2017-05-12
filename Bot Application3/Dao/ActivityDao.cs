using Bot_Application3.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Dao
{
    public class ActivityDao
    {
        public List<t_activity> getAllData()
        {
            crm1Entities context = new crm1Entities();
            var q = from t in context.t_activity
                    select t;


            List<t_activity> list = q.ToList();
            return list;
        }

        public List<t_activity> getDataByCondition(String basicInfoCode, String floor, DateTime? begin, DateTime? end)
        {
            crm1Entities context = new crm1Entities();
            var q = from t in context.t_activity
                    select t;


            if (!String.IsNullOrWhiteSpace(basicInfoCode))
                q = q.Where(p => p.basic_info == basicInfoCode);


            if (!String.IsNullOrWhiteSpace(floor))
                q = q.Where(p => p.floor == floor);

            if (begin.HasValue)
                q = q.Where(p => p.begin <= end.Value);

            if (end.HasValue)
                q = q.Where(p => p.end >= begin.Value);

            List<t_activity> list = q.ToList();

            Debug.WriteLine(begin);
            Debug.WriteLine(end);
            Debug.WriteLine(q.ToString());


            return list;
        }

        public void add(t_activity po)
        {
            crm1Entities context = new crm1Entities();
            //3.将改对象放入EF容器中，默认会为该对象加一个封装类对象（代理类对象）
            //用户对对象的操作，实际上是对代理类的操作
            //DbEntityEntry保存着实体状态，当对象被加入时，EF默认为该对象设置State的属性为unchanged
            DbEntityEntry<t_activity> entityEntry = context.Entry<t_activity>(po);
            //4.设置对象的标志位Added
            entityEntry.State = EntityState.Added;
            //5.当调用SaveChanges()时，EF会遍历所有的代理类对象，并根据标志生成相应的sql语句
            context.SaveChanges();
            //Console.WriteLine("添加成功");
        }
    }
}