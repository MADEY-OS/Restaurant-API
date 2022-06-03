﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_API.Entities;
using Restaurant_API.Models;

namespace Restaurant_API.Controllers;

[Route("api/restaurant")]
public class RestaurantController : ControllerBase
{
    private readonly RestaurantDbContext _dbContext;
    private readonly IMapper _mapper;

    public RestaurantController(RestaurantDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    //GET All Restaurant
    [HttpGet]
    public ActionResult<IEnumerable<RestaurantDto>> GetAll()
    {
        var restaurants = _dbContext.Restaurants.Include(r => r.Address).Include(r => r.Dishes).ToList();

        var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(restaurants);
        return Ok(restaurantsDtos);
    }

    //GET Single Restaurant
    [HttpGet("{id}")]
    public ActionResult<RestaurantDto> Get([FromRoute] int id)
    {
        var restaurant = _dbContext.Restaurants.Include(r => r.Address).Include(r => r.Dishes).FirstOrDefault(r => r.Id == id);

        if (restaurant == null) return NotFound();

        var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
        return Ok(restaurantDto);
    }

    //POST Restaurant
    [HttpPost]
    public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
    {
        var restaurant = _mapper.Map<Restaurant>(dto);

        _dbContext.Restaurants.Add(restaurant);
        _dbContext.SaveChanges();

        return Created($"/api/restaurant/{restaurant.Id}", null);
    }
}