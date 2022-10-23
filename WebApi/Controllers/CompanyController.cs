using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        // // GET api/<controller>
        public async Task<IHttpActionResult> GetAsync()
        {
            var items = await _companyService.GetAllCompanies();
            return Ok(_mapper.Map<IEnumerable<CompanyDto>>(items));
        }


        // GET api/<controller>/5
        /// <summary>
        /// Changed string companyCode to int id because it does not run unless it is int id
        /// Therefore, rest of the methods id represents companyCode
        /// It did not hit debug point when it was string companyCode
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> GetAsync(int id)
        {
            var companyCode = id.ToString();
            var item = await _companyService.GetCompanyByCode(companyCode);
            if (item == null)
                throw new Exception("Record not found");
            return Ok(_mapper.Map<CompanyDto>(item));
        }

        // POST api/<controller>
        /// <summary>
        /// Adds a new company
        /// </summary>
        /// <param name="companyDto"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> PostAsync([FromBody] CompanyDto companyDto)
        {
            if (companyDto == null)
                return BadRequest("Missing argument");

            return Ok(await _companyService.InsertCompany(_mapper.Map<CompanyInfo>(companyDto)));
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Updates company with id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companyDto"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> PutAsync(int id, [FromBody] CompanyDto companyDto)
        {
            if (companyDto == null)
                return BadRequest("Missing argument");

            return Ok(await _companyService.UpdateCompany(id, _mapper.Map<CompanyInfo>(companyDto)));
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes company with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Delete(int id)
        {
            return Ok(await _companyService.DeleteCompany(id));
        }
    }
}