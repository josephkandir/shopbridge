using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shopbridge.api.Models;
using shopbridge.api.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopbridge.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class InventoriesController : ControllerBase
    {
        private readonly IInventory _inventoryRepo;

        public InventoriesController(IInventory inventoryRepo)
        {
            _inventoryRepo = inventoryRepo;
        }

        /// <summary>
        /// Get Inventory List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetInventories()
        {
            try
            {
                var inventories = await _inventoryRepo.GetAsync();

                if (inventories.Count() == 0)
                    return NotFound("User hasn't added any data");

                return Ok(inventories);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Some error has occured!");
            }
        }

        /// <summary>
        /// Get Inventory Detail by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventories(int id)
        {
            try
            {
                var inventory = await _inventoryRepo.GetAsync(id);

                if (inventory == null)
                    return NotFound($"Data doesn't exist for the given Id {id}");

                return Ok(inventory);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Add product to inventory
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> PostInventories(Inventory inventory)
        {
            if (inventory == null)
                return BadRequest();

            try
            {
                var createdInventory = await _inventoryRepo.CreateAsync(inventory);
                Console.WriteLine($"Created: {createdInventory}");
                return Ok("Data added to Inventory.");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Update inventory by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inventory"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Inventory>> PutInventories(int id, Inventory inventory)
        {
            if (id != inventory.Id)
                return BadRequest();

            try
            {
                await _inventoryRepo.UpdateAsync(inventory);
                Console.WriteLine("Inventory updated");
                return Ok("Data updated to the Inventory.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Delete inventory by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Inventory>> DeleteInventories(int id)
        {
            try
            {
                var inventory = await _inventoryRepo.GetAsync(id);

                if (inventory == null)
                    return NotFound($"Data doesn't exist for the given Id {id}");

                return Ok(inventory);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
