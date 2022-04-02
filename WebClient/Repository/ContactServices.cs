using EntityFramework.Web.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WebClient.Repository.Interfaces;
using WebClient.Services.Interfaces;

namespace WebClient.Repository
{
    public class ContactServices : IContactServices
    {
        private IUnitOfWork unitOfWork;
        private ILogger<ContactServices> ilogger;
        public ContactServices(IUnitOfWork unitOfWork, ILogger<ContactServices> ilogger)
        {
            this.unitOfWork = unitOfWork;
            this.ilogger = ilogger;
        }
        public async Task<bool> AddAsync(Contact contact)
        {
            try
            {
                await unitOfWork.contactRepository.AddAsync(contact);
                await unitOfWork.SaveAsync();
                ilogger.LogInformation($"Save object {JsonConvert.SerializeObject(contact)} Is OK");//{JsonConvert.SerializeObject(article)}
                return true;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Save object {JsonConvert.SerializeObject(contact)} Is Fail {ex.Message}");//{JsonConvert.SerializeObject(article)}
                return false;
            }
        }

        public async Task<Contact> GetByIdAsync(long Id)
        {
            try
            {
                var a = await unitOfWork.contactRepository.GetByIdAsync(Id);
                try
                {
                    ilogger.LogInformation($"Get by id {Id.ToString()} Is {JsonConvert.SerializeObject(a)}");
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
    }
}
