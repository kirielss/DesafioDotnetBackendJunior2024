﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook;
using TesteBackendEnContact.Core.Interface.ContactBook;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ILogger<ContactController> _logger;

        public ContactController(ILogger<ContactController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IContact> Post(SaveContactRequest request, [FromServices] IContactRepository contactRepository)
        {
            var contact = await contactRepository.SaveAsync(request);

            return request.id == 0 ? Created(contact) : Ok(contact);
        }

        [HttpDelete]
        public async Task Delete(int id, [FromServices] IContactRepository contactRepository)
        {
            await contactRepository.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<IContact>> Get([FromServices] IContactRepository contactRepository)
        {
            return await contactRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<IContact> Get(int id, [FromServices] IContactRepository contactRepository)
        {
            return await contactRepository.GetAsync(id);
        }

    }
}
