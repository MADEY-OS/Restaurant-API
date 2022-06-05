using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_API.Entities;
using Restaurant_API.Models;
using Restaurant_API.Services;

namespace Restaurant_API.Controllers;

[Route("api/restaurant")]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    //GET All Restaurant
    [HttpGet]
    public ActionResult<IEnumerable<RestaurantDto>> GetAll()
    {
        var restaurantsDtos = _restaurantService.GetAll();
        return Ok(restaurantsDtos);
    }

    //GET Single Restaurant
    [HttpGet("{id}")]
    public ActionResult<RestaurantDto> Get([FromRoute] int id)
    {
        var restaurant = _restaurantService.GerById(id);

        if (restaurant is null)
        {
            return NotFound();
        }

        return Ok(restaurant);
    }

    //POST Restaurant
    [HttpPost]
    public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var id = _restaurantService.Create(dto);


        return Created($"/api/restaurant/{id}", null);
    }

    //DELETE Restaurant
    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] int id)
    {
        var isDeleted = _restaurantService.Delete(id);

        if (!isDeleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    //PUT Restaurant
    [HttpPut("{id}")]
    public ActionResult Update([FromBody]UpdateRestaurantDto dto, [FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var isUpdated = _restaurantService.Update(id, dto);

        if (!isUpdated)
        {
            return NotFound();
        }

        return Ok();
    }
}