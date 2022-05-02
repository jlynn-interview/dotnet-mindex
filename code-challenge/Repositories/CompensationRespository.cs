using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using challenge.Data;

namespace challenge.Repositories
{
    public class CompensationRespository : ICompensationRepository
    {
        private readonly CompensationContext _compensationContext;

        public CompensationRespository(CompensationContext compensationContext)
        {
            _compensationContext = compensationContext;
        }

        public Compensation Add(Compensation compensation)
        {
            _compensationContext.Compensations.Add(compensation);
            return compensation;
        }

        public Compensation GetById(string id)
        {
            Compensation[] returnable = _compensationContext.Compensations.ToArray();
            Compensation returnablet = Array.Find(returnable, findCompensation);
            return returnablet;

            bool findCompensation(Compensation p)
            {
                if (p.Employee != null){
                    return p.Employee.EmployeeId == id;
                } else
                {
                    return false;
                }
            }
        }

        public Task SaveAsync()
        {
            return _compensationContext.SaveChangesAsync();
        }

        public Compensation Remove(Compensation compensation)
        {
            return _compensationContext.Remove(compensation).Entity;
        }
    }
}
