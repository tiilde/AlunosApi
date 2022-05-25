using AlunosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AlunosApi.Context {

    // classe que faz a ponte entre a entidade e o banco de dados
    public class AppDbContext : DbContext{

        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) {

        }

        public DbSet<Aluno> Alunos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Aluno>().HasData(
                
                new Aluno {
                
                    Id = 1,
                    Name = "Nicole Lima",
                    Email = "nicolelima@email.com",
                    Idade = 20  
                },

                new Aluno {

                    Id=2,
                    Name= "Maria Bastos",
                    Email = "mariabastos@email.com",
                    Idade= 26
                }
                
            );         
        }
    }
}
