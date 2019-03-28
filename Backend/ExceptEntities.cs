using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend
{
    public class ExceptEntities:DbContext
    {
        public virtual DbSet<ExceptionLog> ExceptionLogs { get; set; }
     
    }
}
