using BaseBackend.Contexts;
using BaseBackend.Entities;
using BaseBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BaseBackend.Services
{
    public class TestService
    {
        public void Dispose()
        {
        }

        private BaseContext _dbContext;

        public TestService(BaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        TestRepository _repo => new TestRepository(_dbContext);

        public async Task<Test> GetTestById(int id)
        {
            return await _repo.FindAsync(x => x.TestId == id);
        }
    }
}
