﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.NoteDTOs;
using Service.Interfaces;

namespace NoteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        public readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService ?? throw new ArgumentNullException(nameof(noteService));
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> FindAll()
        {
            var response = await _noteService.FindAll();
            if (response.Code == 200) return Ok(response);
            return BadRequest(response);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult> FindById(int id)
        {
            var response = await _noteService.FindById(id);
            if(response.Code == 406) return Problem(statusCode: 406, title: "Caracter Not Acceptable");
            if (response.Code == 404) return NotFound(response);
            return Ok(response);
        }
        [Authorize]
        [HttpPost("register")]
        public async Task<ActionResult> Create([FromBody]NoteCreateDTO note)
        {
            var response = await _noteService.Create(note);
            if (response.Code == 400) return BadRequest(response);
            return StatusCode(201);
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult> Update([FromBody] NoteUpdateDTO note)
        {
            var response = await _noteService.Update(note);
            if (response.Code == 400) return BadRequest(response);
            if (response.Code == 404) return NotFound(response);
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _noteService.Delete(id);
            if (response.Code == 406) return Problem(statusCode: 406, title: "Caracter Not Acceptable");
            if (response.Code == 404) return NotFound(response);
            return Ok(response);
        }

    }

}
