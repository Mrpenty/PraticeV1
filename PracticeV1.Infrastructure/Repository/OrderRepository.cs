using PracticeV1.Application.DTO.Order;
using PracticeV1.Application.Repositories;
using PracticeV1.Domain.Entity;
using PracticeV1.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Infrastructure.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(PRDBContext context) : base(context) { }


    }
}
