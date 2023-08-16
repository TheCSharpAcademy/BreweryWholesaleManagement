using System.Net;
using BreweryWholesaleManagement.Data;
using BreweryWholesaleManagement.Data.DTOs;
using BreweryWholesaleManagement.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BreweryWholesaleManagement.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BreweryController : ControllerBase
{
    private readonly BreweryContext _context;
    public BreweryController(BreweryContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<List<Brewery>>> GetAllBeers(int breweryId)
    {
        var brewery = await  _context
            .Breweries
            .Include(b=>b.Beers)
            .FirstOrDefaultAsync(b=>b.Id==breweryId);

        if (brewery == null)
        {
            return NotFound("Brewery not found with breweryId");
        }

        var beers = brewery.Beers;
        return Ok(beers);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> AddBeer(int breweryId, Beer beer)
    {
        var brewery = await _context
            .Breweries
            .Include(b => b.Beers)
            .FirstOrDefaultAsync(b => b.Id == breweryId);

        if (brewery == null)
        {
            return NotFound("Brewery not found with breweryId");
        }

        brewery.Beers.Add(beer);
        await _context.SaveChangesAsync();
        var uri = $"brewery/{breweryId}/beer/{beer.Id}";
        return Created(uri,null);
    }

    [HttpDelete]
    [Route("{beerId:int}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> DeleteBeer(int breweryId, int beerId)
    {
        var brewery = await _context
            .Breweries
            .Include(b => b.Beers)
            .FirstOrDefaultAsync(b => b.Id == breweryId);

        if (brewery == null)
        {
            return NotFound("Brewery not found with breweryId");
        }

        var beer = brewery.Beers.FirstOrDefault(b => b.Id == beerId);

        if (beer == null)
        {
            return NotFound($"Beer doesn't exist with id {beerId}");
        }

        brewery.Beers.Remove(beer);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> AddWholeSalerOrder(Order order)
    {
        var brewery = await _context
            .Breweries
            .Include(b => b.Beers)
            .FirstOrDefaultAsync(b => b.Id == order.BreweryId);

        if (brewery == null)
        {
            return NotFound("Brewery not found with breweryId");
        }

        var wholesaler = await _context
            .Wholesalers
            .FirstOrDefaultAsync(b => b.Id == order.WholesalerId);

        if (wholesaler == null)
        {
            return NotFound("Wholesaler not found with wholesalerId");
        }

        _context.Orders.Add(order);

        await _context.SaveChangesAsync();
        var uri = "";//$"brewery/{breweryId}/beer/{beer.Id}";
        return Created(uri, null);
    }
}