using BaseBackend.Contexts;
using BaseBackend.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseBackend.Repositories
{
    public class TestRepository : GenericRepository<BaseContext, Test>
    {
        public TestRepository()
        {
        }

        public TestRepository(BaseContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
