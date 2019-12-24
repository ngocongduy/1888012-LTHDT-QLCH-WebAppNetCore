using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _1888012_LTHDT_QLCH_WebAppNetCore.Models
{
    //IdentityDbContext is bigger than DBContext
    public class AppDbContext : IdentityDbContext<IdentityUser> //Just plug in your customized IndentityUser if you want to change
    {             
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) //Pass options from DbContextOptions to DbContext
        {

        } 
        //public DbSet<Product> Products { get; set; } 
    }
}
