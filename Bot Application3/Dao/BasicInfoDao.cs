using Bot_Application3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dao
{
    public class BasicInfoDao
    {
        public List<t_basic_info> getAllData()
        {
            crm1Entities context = new crm1Entities();
            var q = from t in context.t_basic_info
                    select t;


            List<t_basic_info> list = q.ToList();
            return list;
        }

       
    }
}