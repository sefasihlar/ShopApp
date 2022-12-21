﻿using ShopApp.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Abstract
{
    public interface ICategoryDal:IRepository<Category>
    {
        Category GetByIdWithProducuts(int id);
    }
}
