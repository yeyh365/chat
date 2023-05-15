﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Repositories
{
    public interface IEntitiy<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}
