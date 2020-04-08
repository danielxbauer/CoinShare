using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CoinShare.Api.Dtos;
using CoinShare.Api.Shared.Dtos;
using CoinShare.Core.Logic;
using CoinShare.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoinShare.Api.Controllers
{
    [ApiController]
    [Route("api/group")]
    public class GroupApiController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IGroupLogic groupLogic;
        private readonly IPersonLogic personLogic;

        public GroupApiController(
            IMapper mapper,
            IGroupLogic groupLogic,
            IPersonLogic personLogic)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.groupLogic = groupLogic ?? throw new ArgumentNullException(nameof(groupLogic));
            this.personLogic = personLogic ?? throw new ArgumentNullException(nameof(personLogic));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult<GroupDto>> GetById(Guid id)
        {
            Group group = await this.groupLogic.GetGroupByIdAsync(id);
            if (group == null) return NotFound();

            IEnumerable<Person> persons = await this.personLogic.GetPersonsByGroupIdAsync(id);

            GroupDto groupDto = this.mapper.Map<GroupDto>(group);
            groupDto.Persons = this.mapper.Map<IList<PersonDto>>(persons);
            return Ok(groupDto);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] GroupDto groupDto)
        {
            Group group = this.mapper.Map<Group>(groupDto);
            IEnumerable<Person> persons = this.mapper.Map<IEnumerable<Person>>(groupDto.Persons);

            await this.groupLogic.SaveGroupAsync(group);

            foreach (var person in persons)
            {
                person.GroupId = group.Id;
                await this.personLogic.SavePersonAsync(person);
            }

            return Ok(group.Id);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] GroupDto groupDto)
        {
            Group group = this.mapper.Map<Group>(groupDto);
            IEnumerable<Person> persons = this.mapper.Map<IEnumerable<Person>>(groupDto.Persons);

            await this.groupLogic.SaveGroupAsync(group);

            foreach (var person in persons)
            {
                person.GroupId = group.Id;
                await this.personLogic.SavePersonAsync(person);
            }

            return Ok();
        }
    }
}
