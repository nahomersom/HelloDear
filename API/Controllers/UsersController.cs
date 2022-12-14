using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
     // in order to access this controllers from the client the user will need to go api/users(which is the name of our controller)
   

    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        
       //in order to get data from our db we need to injecet our dbcontext 
        public UsersController(DataContext context)
        {
            _context = context;
            
        }
        // IEnumerable is one of out of many type of list in c#(list in js)
        // AppUser is our entity that we create
        // [HttpGet] decoratore is like app.get('/api/users') in mongodb
        [HttpGet]
        public async  Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){
            // we are parsing the the db data to list (below we use async version) so that it become IEnumerable type
               return await _context.Users.ToListAsync();

        }
        // it forces the user to send authorizations header e.g token
        [Authorize]//we need to authorize first
        // get specific user
        // api/users/3
        [HttpGet("{id}")]
        
        public async Task<ActionResult <AppUser>> GetUser(int id){
            // find is the method allow us to find from our db by using foreign key
               return await _context.Users.FindAsync(id);
        }
    }
}