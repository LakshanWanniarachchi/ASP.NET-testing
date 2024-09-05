using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{

    [Route("api/Stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        private readonly ApplicationDbContext _context;

        public StockController(ApplicationDbContext context, IStockRepository stockRepository)
        {

            _stockRepository = stockRepository;

            _context = context;

        }

        [HttpGet]

        public async Task<List<Stock>> GetAllDataAsync()
        {
            return await _stockRepository.GetAllAsync();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetDataAsync([FromRoute] int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            var stockDto = stock.ToStockDto();
            return Ok(stockDto);
        }


        [HttpPost]
        public async Task<IActionResult> CreateStockAsync([FromBody] CreateStockRequestDto createStockRequestDto)
        {
            var stockModel = createStockRequestDto.ToCreateStockRequestDto();
            _context.Stocks.Add(stockModel);

            // Attempt to save the changes to the database
            int result = await _context.SaveChangesAsync();

            // Check if the data was successfully saved
            if (result > 0)
            {
                // Data saved successfully, return a CreatedAtAction response
                return Ok(stockModel.ToStockDto());
            }
            else
            {
                // Something went wrong, return a server error response
                return StatusCode(500, "A problem occurred while handling your request.");
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateStockAsync([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.id == id); //  await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            stock.Symbol = updateStockRequestDto.Symbol;
            stock.CompanyName = updateStockRequestDto.CompanyName;
            stock.Purchase = updateStockRequestDto.Purchase;
            stock.LastDiv = updateStockRequestDto.LastDiv;
            stock.Industry = updateStockRequestDto.Industry;
            stock.MarketCap = updateStockRequestDto.MarketCap;

            _context.Stocks.Update(stock);

            // Attempt to save the changes to the database
            int result = await _context.SaveChangesAsync();

            // Check if the data was successfully saved
            if (result > 0)
            {
                // Data saved successfully, return a CreatedAtAction response
                return Ok(stock.ToStockDto());
            }
            else
            {
                // Something went wrong, return a server error response
                return StatusCode(500, "A problem occurred while handling your request.");
            }
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteStockAsync([FromRoute] int id)
        {

            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.id == id); //  await _context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stock);

            // Attempt to save the changes to the database
            int result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500, "A problem occurred while handling your request.");
            }





        }
    }

}