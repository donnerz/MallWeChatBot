using Bot_Application3.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Bot_Application3.test
{
    public class Class1
    {
        public void add()
        {
            //1.创建一个EF数据上下文对象
            crm1Entities context = new crm1Entities();
            //2.将要添加的数据，封装成对象
            test_table tt = new test_table() { name = "zhang3", date = DateTime.Now };
            //3.将改对象放入EF容器中，默认会为该对象加一个封装类对象（代理类对象）
            //用户对对象的操作，实际上是对代理类的操作
            //DbEntityEntry保存着实体状态，当对象被加入时，EF默认为该对象设置State的属性为unchanged
            DbEntityEntry<test_table> entityEntry = context.Entry<test_table>(tt);
            //4.设置对象的标志位Added
            entityEntry.State = EntityState.Added;
            //5.当调用SaveChanges()时，EF会遍历所有的代理类对象，并根据标志生成相应的sql语句
            context.SaveChanges();
            //Console.WriteLine("添加成功");
        }

        public void select()
        {
            crm1Entities context = new crm1Entities();
            //var s = context.test_table.Where(tt => tt.name == "张三").Select(tt => tt);
            //var tt = context.test_table.Find(4);

            var tt = context.test_table.Single(t => t.name == "张三");

            Debug.WriteLine(tt.id + tt.name + tt.date);

        }
    }
}   