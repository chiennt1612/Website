using EntityFramework.Web.Entities;
using EntityFramework.Web.Entities.Ordering;
using Microsoft.Extensions.Logging;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebClient.Repository.Interfaces;
using WebClient.Services.Interfaces;

namespace WebClient.Services
{
    public class OrderServices : IOrderServices
    {
        private IUnitOfWork unitOfWork;
        private ILogger<OrderServices> ilogger;

        public OrderServices(IUnitOfWork unitOfWork, ILogger<OrderServices> ilogger)
        {
            this.unitOfWork = unitOfWork;
            this.ilogger = ilogger;
        }

        public async Task<Order> GetByIdAsync(long Id)
        {
            try
            {
                var a = await unitOfWork.orderRepository.GetByIdAsync(Id);
                a.OrderItems = await unitOfWork.orderItemRepository.GetByOrderIdAsync(Id);
                try
                {
                    ilogger.LogInformation($"Get by id {Id.ToString()} ");
                }
                catch (Exception ex)
                {
                    ilogger.LogInformation($"Get by id {Id.ToString()} Is {ex.Message}");
                }
                return a;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Get by id {Id.ToString()} Is Fail {ex.Message}");
                return default;
            }
        }

        public async Task<BaseEntityList<Order>> GetListAsync(
            Expression<Func<Order, bool>> expression,
            Func<Order, object> sort, bool desc,
            int page, int pageSize)
        {
            try
            {
                var a = await unitOfWork.orderRepository.GetListAsync(expression, sort, desc, page, pageSize);
                ilogger.LogInformation($"GetListAsync expression, sort {desc} {page} {pageSize}");
                return a;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"GetListAsync expression, sort {desc} {page} {pageSize} Is Fail {ex.Message}");
                return default;
            }
        }

        public async Task<Order> Add(Order order)
        {
            try
            {
                await unitOfWork.orderRepository.AddAsync(order);
                await unitOfWork.orderItemRepository.AddManyAsync(order.OrderItems);
                await unitOfWork.SaveAsync();
                ilogger.LogInformation($"Add order is OK");
                return order;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Add order is error {ex.Message}");
                return default;
            }
        }

        public async Task<Order> Update(Order order)
        {
            try
            {
                unitOfWork.orderRepository.Update(order);
                await unitOfWork.SaveAsync();
                ilogger.LogInformation($"Update order is OK");
                return order;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Update order is error {ex.Message}");
                return default;
            }
        }
    }
}
