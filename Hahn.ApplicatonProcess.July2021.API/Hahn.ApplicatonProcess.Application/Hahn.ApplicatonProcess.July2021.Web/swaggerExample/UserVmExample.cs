using Hahn.ApplicatonProcess.July2021.Domain.Entities;
using Hahn.ApplicatonProcess.July2021.Domain.VMs;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Web.swaggerExample
{
    public class UserVmExample : IExamplesProvider<UserVm>
    {
        /// <summary>
        /// gets the sample data for User model insert operation
        /// </summary>
        /// <returns></returns>
        public UserVm GetExamples()
        {
            return new UserVm
            {
                Id = 0,
                Address = "15/3, North Street",
                Age = 25,
                Email = "1@gmail.com",
                FirstName = "Lakshmanan",
                LastName = "Dhamotharan",
                Assets = new List<Domain.Entities.Asset>()
                {
                    new Asset()
                    {
                        AssetId = "bitcoin",
                        Name = "Bitcoin",
                        Symbol = "BTC"
                    },

                    new Asset()
                    {
                        AssetId = "ethereum",
                        Name = "Ethereum",
                        Symbol = "ETH"
                    }

                }
            };
        }
    }
}
