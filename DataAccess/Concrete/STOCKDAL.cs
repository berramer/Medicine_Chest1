﻿using DATA.Absract;
using DataAcces.Concrete;
using ENTITIES;
using System;
using System.Collections.Generic;
using System.Text;

namespace DATA.Concrete
{
    public class STOCK : IRepositoryBase<STOCK, WebContext>, IRepository<STOCK>
    {
    }
}
