﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApi.Interfaces;
using SchoolApi.Models;
using SchoolApi.Services;

namespace SchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService course)
        {
            _courseService = course;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCourse(Course course)
        {
            var createdCourse=await _courseService.Create(course);
            
            return createdCourse is null
                ? throw new Exception("Course creation failed")
                : CreatedAtAction(nameof(GetById),
                new { id = createdCourse.Id }, createdCourse);
        }
        [HttpGet/*("{id:length(24)}")*/]
        public async Task<ActionResult<Course>> GetById(string id)
        {
            var course = await _courseService.GetById(id);
            return course is null ? NotFound() : Ok(course);
        }
    }
}
