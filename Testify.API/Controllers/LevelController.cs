﻿using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;
using Testify.DAL.Models;
using Testify.DAL.Reposiroties;

namespace Testify.API.Controllers
{
    [Route("Level")]
    [ApiController]
    public class LevelController : ControllerBase
    {
        LevelRepository _repo;
        public LevelController()
        {
            _repo = new LevelRepository();
        }

        [HttpGet("get-all-level-by-id")]
        public async Task<ActionResult<List<Level>>> GetAllById(int id)
        {
            var allLevel = await _repo.GetAllLevelId(id);
            return Ok(allLevel);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<Level>>> GetAll()
        {
            return await _repo.GetAllLevels();
        }

        [HttpGet("get-user-by-idlevel")]
        public async Task<ActionResult<List<User>>> GetUserById(int levelId, string? textSearch)
        {
            var allUser = await _repo.GetUserByIdLevel(levelId, textSearch);
            return Ok(allUser);
        }

        
    }
}
