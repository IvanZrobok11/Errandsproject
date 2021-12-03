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
        public static FileModel GetFile()
        {
            return new FileModel()
            {
                Id = GenerateGuid(5),
                Name = "File",
                Path = "Path",
                Type = TypeFile.File,
            };
        }
        public static IEnumerable<Errand> GetErrands(int count,  bool sameUser = true, bool done = false, bool active = true)
        {
            var testUser = new User()
            {
                Id = TestUser.Identifier,
                UserName = TestUser.Username,
                Email = "testuser123@gmail.com",
            };
            var errands = Enumerable.Range(1, count)
                .Select(e => new Errand()
                {
                    Id = GenerateGuid(byte.Parse(e.ToString())),
                    Title = $"Title{e}",
                    Description = $"Description{e}",
                    Done = done,
                    Active = active,
                    CreationDate = new DateTime(2021, 1, 1),
                    User = sameUser ? testUser : 
                        new User
                        {
                            UserName = $"TestUser{count}",
                            Id = $"TestId"
                        },
                    HelperUserId = "TestHelperUserId"
                })
                .ToList();
            return errands;
        }

        public static IEnumerable<Errand> GetErrands()
        {
            var users = Enumerable.Range(1, 10).Select(n =>
                new User()
                {
                    UserName = "name" + n,
                    Email = $"name{n}@gmail.com",
                    Id = n.ToString()
                }).ToArray();
            var errands = Enumerable.Range(1, 10).Select(n =>
                new Errand()
                {
                    Id = GenerateGuid(byte.Parse(n.ToString())), 
                    Cost = n , 
                    Title = "title" + n,
                    Description = "Description" + n,
                    UserId = users[n - 1].Id
                }).ToArray();
            return errands.ToList();
        }

        public static Guid GenerateGuid(byte withDigit)
        {
            byte[] guid = new byte[16];
            for (int i = 0; i < guid.Length -1; i++)
            {
                guid[i] = withDigit;
            }

            return new Guid(guid);
        }
    }


}
