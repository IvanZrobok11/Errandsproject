using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Errands.Data.Services;
using Errands.Domain.Models;
using MyTested.AspNetCore.Mvc;

namespace ErrandsTests.FakeDependencies
{
    public static class ErrandsTestData 
    {
        public static IEnumerable<Errand> GetErrands(int count,bool done = false, bool active = true ,bool sameUser = true)
        {
            var testUser = new User()
            {
                Id = TestUser.Identifier,
                UserName = TestUser.Username
            };
            var errands = Enumerable.Range(1, count)
                .Select(e => new Errand()
                {
                    Id = new Guid(),
                    Title = $"Errand {e}",
                    Description = $"Errand {e} description",
                    Done = done,
                    Active = active,
                    CreationDate = new DateTime(2021,1,1),
                    User = sameUser ? testUser : new User()
                    {
                        Id = $"User id {e}",
                        UserName = $"User name {e}"
                    }

                })
                .ToList();
            return errands;
        }
       
    }
}
