﻿using Buisness.Abstract;
using Buisness.Concrete;
using DataAccess.Concrete.Entityframework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductService _productService;

        public ProductsController (IProductService productService)

        {
            _productService = productService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success) 
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);

        }
        [HttpPost("add")]

        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result.Success);
            }
            return BadRequest(result.Message);

        }


    }
}