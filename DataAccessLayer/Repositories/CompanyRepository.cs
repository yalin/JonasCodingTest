using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IDbWrapper<Company> _companyDbWrapper;

        public CompanyRepository(IDbWrapper<Company> companyDbWrapper)
        {
            _companyDbWrapper = companyDbWrapper;
        }

        public async Task<IEnumerable<Company>> GetAll()
        {
            return await _companyDbWrapper.FindAllAsync();
        }

        public async Task<Company> GetByCode(string companyCode)
        {
            var result = await _companyDbWrapper.FindAsync(t => t.CompanyCode.Equals(companyCode));
            return result?.FirstOrDefault();
        }

        public async Task<bool> SaveCompany(Company company)
        {
            var result = await _companyDbWrapper.FindAsync(t =>
                t.SiteId.Equals(company.SiteId) && t.CompanyCode.Equals(company.CompanyCode));

            var itemRepo = result?.FirstOrDefault();
            if (itemRepo != null)
            {
                itemRepo.CompanyName = company.CompanyName;
                itemRepo.AddressLine1 = company.AddressLine1;
                itemRepo.AddressLine2 = company.AddressLine2;
                itemRepo.AddressLine3 = company.AddressLine3;
                itemRepo.Country = company.Country;
                itemRepo.EquipmentCompanyCode = company.EquipmentCompanyCode;
                itemRepo.FaxNumber = company.FaxNumber;
                itemRepo.PhoneNumber = company.PhoneNumber;
                itemRepo.PostalZipCode = company.PostalZipCode;
                itemRepo.LastModified = company.LastModified;
                return await _companyDbWrapper.UpdateAsync(itemRepo);
            }

            return await _companyDbWrapper.InsertAsync(company);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        public Task<bool> DeleteCompany(string companyCode)
        {
            return _companyDbWrapper.DeleteAsync(x => x.CompanyCode.Equals((companyCode)));
        }
    }
}